using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class CustomCoroutine : MonoBehaviour
{
    private CustomCoroutineInternalPool _pool;
    private CustomCoroutineSettings settings;
    private readonly List<CustomCoroutineInternal> activeCoroutines;
    private List<int> availableIndices;
    private Queue<int> freeIndices;
    private Dictionary<string, List<int>> usingIndices;

    private void Start()
    {
        usingIndices = new Dictionary<string, List<int>>(settings.MaxCoroutines);
        _pool = new CustomCoroutineInternalPool();
        Resize(settings.MaxCoroutines);
    }

    private void InitIndices(int start ,int count)
    {
        for (int i = start; i < count; i++)
        {
            availableIndices[i] = i;
            freeIndices.Enqueue(i);
        }
    }
    
    public void Update()
    {
        if (settings.OptimizeMode) return;
        if (activeCoroutines.Count == 0) return;
            
        for (var i = 0; i < activeCoroutines?.Count; i++)
        {
            CustomCoroutineInternal behaviour = activeCoroutines[i];
            ProcessCoroutine(ref behaviour);    
        }
    }
    
    private void FixedUpdate()
    {
        if (!settings.OptimizeMode) return;
        if (activeCoroutines.Count == 0) return;
            
        for (var i = 0; i < activeCoroutines?.Count; i++)
        {
            CustomCoroutineInternal behaviour = activeCoroutines[i];
            ProcessCoroutine(ref behaviour);    
        }
    }
    public async Task UpdateAsync()
    {
        for (var index = 0; index < activeCoroutines.Count; index++)
        {
            var behaviour = activeCoroutines[index];
            await Task.Yield();
            ProcessCoroutine(ref behaviour);
        }
    }
    
    public Task UpdateInParallel()
    {
        void ParallelBehaviours(CustomCoroutineInternal behaviour)
        {
            ProcessCoroutine(ref behaviour);
        }

        Parallel.ForEach(activeCoroutines, ParallelBehaviours);
        return Task.CompletedTask;
    }
    
    #region Accessors
    public void Resize(int capacity)
    {
        // buffer
        if(availableIndices is null)
            availableIndices = new List<int>(capacity);
        else
            availableIndices.AddRange(Enumerable.Range(start: availableIndices.Count, count: capacity));
        
        if(freeIndices is null)
        {
            freeIndices = new Queue<int>(capacity);
            InitIndices(start: 0, count: settings.MaxCoroutines);
        }
        // if already using previous all indices, then add new indices range.
        else
            InitIndices(start: availableIndices.Count, count: capacity);
    }
    public int StartRoutine(IEnumerator routine)
    {
        if (freeIndices.Count == 0)
        {
            InitIndices(0, availableIndices.Count);
        }
        
        int index = freeIndices.Dequeue();
        activeCoroutines.Add(_pool.Get().Init(index, routine, new CustomCoroutineToken(index, true, false,false,true)));
        return index;
    }
    public int StartRoutineWithTag(string tag, IEnumerator routine)
    {
        if (freeIndices.Count == 0)
        {
            InitIndices(0, availableIndices.Count);
        }
        
        int index = freeIndices.Dequeue();
        CustomCoroutineInternal internalRoutine = _pool.Get().Init(index, routine, new CustomCoroutineToken(index, true, false,false,true));
        
        if(usingIndices.ContainsKey(tag))
            usingIndices[tag].Add(index);
        else
            usingIndices.Add(tag, new List<int>(settings.MaxCoroutines){index});
        
        activeCoroutines.Add(internalRoutine);
        return index;
    }
    public void ResumeRoutine(CustomCoroutineToken token)
    {
        if (activeCoroutines.Count <= token.Index) return;
        activeCoroutines[token.Index].OnStart();
    }
    public void PauseRoutine(CustomCoroutineToken token)
    {
        if (activeCoroutines.Count <= token.Index) return;
        activeCoroutines[token.Index].OnPause();
    }
    public void ConvertToAsync(CustomCoroutineToken token)
    {
        if (activeCoroutines.Count <= token.Index) return;
        activeCoroutines[token.Index].OnAsync();
    }
    public void ConvertToSync(CustomCoroutineToken token)
    {
        if (activeCoroutines.Count <= token.Index) return;
        activeCoroutines[token.Index].OnSync();
    }
    
    public void StopRoutine(IEnumerator routine)
    {
        int GetEqualRoutineIndex()
        {
            for (var i = 0; i < activeCoroutines.Count; i++)
            {
                if (activeCoroutines[i].Routine == routine)
                {
                    _pool.Return(activeCoroutines[i]);
                    freeIndices.Enqueue(activeCoroutines[i].Index);
                    return i;
                }
            }

            return -1;
        }

        int index = GetEqualRoutineIndex();

        if (index is -1) return;

        activeCoroutines.RemoveAt(index);
    } 
    public void StopRoutine(CustomCoroutineToken token)
    {
        activeCoroutines[token.Index].OnStop();
        
        _pool.Return(activeCoroutines[token.Index]);
        freeIndices.Enqueue(token.Index);
        activeCoroutines.RemoveAt(token.Index);
    }

    public void StopAllRoutines()
    {
        foreach (CustomCoroutineInternal activeCoroutine in activeCoroutines)
        {
            _pool.Return(activeCoroutine);
        }
        activeCoroutines.Clear();
        
        // clear and reset. 
        availableIndices.Clear();
        freeIndices.Clear();
        Resize(settings.MaxCoroutines);
    }
    #endregion

    #region Queries
    public bool AnyExist(string tag)
    {
        return usingIndices.TryGetValue(tag, out List<int> indices) && indices.Count != 0;
    }
    public bool HasRoutine(string tag, CustomCoroutineToken token)
    {
        return usingIndices.ContainsKey(tag) && usingIndices[tag].Contains(token.Index);
    }
    #endregion

    #region Processor
    bool ProcessCoroutine(ref CustomCoroutineInternal behaviour)
    {
        do
        {
            if (behaviour.Routine is null)
                break;

            if (behaviour.Token is { Pause: true, Start: false ,Stop: false})
                return false;
            
            if(behaviour.Token is {Stop: true})
                break;

            object current = behaviour.Routine.Current;
                
            switch (current)
            {
                case CustomYieldInstruction { keepWaiting: true }:
                    return false;
                case CustomYieldInstruction:
                    continue;
                case WaitForEndOfFrame or WaitForFixedUpdate or null when behaviour.Token.SyncOrAsync:
                    return false;
                case WaitForEndOfFrame or WaitForFixedUpdate or null:
                {
                    if (behaviour.KeepWaiting(true)) return false;
                    continue;
                }
                case IEnumerator subRoutine when behaviour.Token.SyncOrAsync:
                {
                    if (!ProcessCoroutine(subRoutine, behaviour.Token)) continue;
                    return true;
                }
                case IEnumerator subRoutine when ProcessCoroutine(subRoutine, behaviour.Token):
                    continue;
                case IEnumerator:
                    return true;
            }

            // Task Area.
            if (current is not Task task) return false;
            if (!behaviour.Token.SyncOrAsync) continue;
            if (task.IsCanceled || task.IsCompleted || task.IsCompletedSuccessfully || task.IsFaulted)
                continue;

            return false;

        } while (behaviour.Routine.MoveNext());

        return true;
    }

    private bool ProcessCoroutine(in IEnumerator routine, CustomCoroutineToken token)
    {
        do
        {
            if (routine is null)
                break;

            object current = routine.Current;

            if (token is { Pause: true, Start: false, Stop: false })
                return false;
            if(token is {Stop: true})
                break;

            switch (current)
            {
                case WaitWhile{keepWaiting: true}:
                    return false;
                case WaitWhile{keepWaiting: false}:
                    continue;
                case WaitUntil{keepWaiting: true}:
                    return false;
                case WaitUntil{keepWaiting: false}:
                    continue;
                case CustomYieldInstruction { keepWaiting: true }:
                    return false;
                case CustomYieldInstruction { keepWaiting: false }:
                    continue;
                case WaitForEndOfFrame :
                case WaitForFixedUpdate :
                case null:
                    if (token.KeepWaiting(true)) return false;
                    continue;
            }

            if (current is Task task)
            {
                if (task.IsCanceled || task.IsCompleted || task.IsCompletedSuccessfully || task.IsFaulted)
                    continue;

                return false;
            }

            if (current is not IEnumerator other) continue;

            if (token.SyncOrAsync)
            {
                if (!ProcessCoroutine(other, token)) continue;

                return true;
            }

            if (ProcessCoroutine(other, token)) continue;

            return true;

        } while (routine.MoveNext());

        return true;
    }
    #endregion
}
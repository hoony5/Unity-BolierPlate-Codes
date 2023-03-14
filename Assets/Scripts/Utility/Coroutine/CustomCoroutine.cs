using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class CustomCoroutine : MonoBehaviour
{
    public CustomCoroutineSettings settings;
    private CustomCoroutineInternalPool _coroutinePool;
    private List<CustomCoroutineInternal> _activeCoroutines;
    private List<int> _availableIndicesList;
    private Queue<int> _freeIndicesQueue;
    private Dictionary<string, List<int>> _usingIndicesDictionary;

    private bool _isStart;
    private bool _isPause;
    private bool _isStop;
    
    private void Start()
    {
        _usingIndicesDictionary = new Dictionary<string, List<int>>(settings.MaxCoroutines);
        _coroutinePool = new CustomCoroutineInternalPool();
        Resize(settings.MaxCoroutines);
    }

    private void InitializeAvailableIndices(int start ,int count)
    {
        for (int i = start; i < count; i++)
        {
            _availableIndicesList[i] = i;
            _freeIndicesQueue.Enqueue(i);
        }
    }
    
    public void Update()
    {
        if (settings.OptimizeMode) return;
        if (_activeCoroutines.Count == 0) return;
            
        if(_isStart && !_isPause && !_isStop)
            OnUpdateRoutines();
        
        if(_isStop)
            RemoveAllRoutines();
    }
    
    private void FixedUpdate()
    {
        if (!settings.OptimizeMode) return;
        if (_activeCoroutines.Count == 0) return;

        if(_isStart && !_isPause && !_isStop)
            OnUpdateRoutines();
        
        if(_isStop)
            RemoveAllRoutines();
    }

    public void ResetRoutines()
    {
        _activeCoroutines.Clear();
        _freeIndicesQueue.Clear();
        _usingIndicesDictionary.Clear();
        InitializeAvailableIndices(0, settings.MaxCoroutines);
    }

    private void OnUpdateRoutines()
    {
        for (var i = 0; i < _activeCoroutines?.Count; i++)
        {
            CustomCoroutineInternal behaviour = _activeCoroutines[i];
            if(settings.DebugMode)
                ProcessDebugCoroutine(ref behaviour);
            else
                ProcessCoroutine(ref behaviour);    
        }
    }
    private async Task OnUpdateRoutinesAsync()
    {
        for (var index = 0; index < _activeCoroutines.Count; index++)
        {
            CustomCoroutineInternal behaviour = _activeCoroutines[index];
            await Task.Yield();
            if(settings.DebugMode)
                ProcessDebugCoroutine(ref behaviour);
            else
                ProcessCoroutine(ref behaviour);    
        }
    }
    
    private Task OnUpdateRoutinesInParallel()
    {
        void ParallelBehaviours(CustomCoroutineInternal behaviour)
        {
            if(settings.DebugMode)
                ProcessDebugCoroutine(ref behaviour);
            else
                ProcessCoroutine(ref behaviour);    
        }

        Parallel.ForEach(_activeCoroutines, ParallelBehaviours);
        return Task.CompletedTask;
    }
    
    #region Accessors

    public void RunRoutines()
    {
        _isStart = true;
        _isStop = false;
        _isPause = false;
    }

    public void PauseRoutines()
    {
        _isStart = false;
        _isStop = false;
        _isPause = true;
    }
    public void ClearRoutines()
    {
        _isStart = false;
        _isStop = true;
        _isPause = false;
    }
    public void Resize(int capacity)
    {
        if(_availableIndicesList is null)
            _availableIndicesList = new List<int>(capacity);
        else
            _availableIndicesList.AddRange(Enumerable.Range(start: _availableIndicesList.Count, count: capacity));
        
        if(_freeIndicesQueue is null)
        {
            _freeIndicesQueue = new Queue<int>(capacity);
            InitializeAvailableIndices(start: 0, count: settings.MaxCoroutines);
        }
        else
            InitializeAvailableIndices(start: _availableIndicesList.Count, count: capacity);
        
        _activeCoroutines ??= new List<CustomCoroutineInternal>(settings.MaxCoroutines);
    }
    public int AddRoutine(IEnumerator routine)
    {
        if (_freeIndicesQueue.Count == 0)
        {
            InitializeAvailableIndices(0, _availableIndicesList.Count);
        }
        
        int index = _freeIndicesQueue.Dequeue();
        _activeCoroutines.Add(_coroutinePool.Get().Init(index, routine, new CustomCoroutineToken(index, true, false,false,true)));
        return index;
    }
    public int AddRoutineWithTag(string tag, IEnumerator routine)
    {
        if (_freeIndicesQueue.Count == 0)
        {
            InitializeAvailableIndices(0, _availableIndicesList.Count);
        }
        
        int index = _freeIndicesQueue.Dequeue();
        CustomCoroutineInternal internalRoutine = _coroutinePool.Get().Init(index, routine, new CustomCoroutineToken(index, true, false,false,true));
        
        if(_usingIndicesDictionary.ContainsKey(tag))
            _usingIndicesDictionary[tag].Add(index);
        else
            _usingIndicesDictionary.Add(tag, new List<int>(settings.MaxCoroutines){index});
        
        _activeCoroutines.Add(internalRoutine);
        return index;
    }
    public void ResumeRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnStart();
    }
    public void PauseRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnPause();
    }
    public void ConvertToAsync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnAsync();
    }
    public void ConvertToSync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Count <= token.Index) return;
        _activeCoroutines[token.Index].OnSync();
    }
    
    public void RemoveRoutine(IEnumerator routine)
    {
        int GetEqualRoutineIndex()
        {
            for (var i = 0; i < _activeCoroutines.Count; i++)
            {
                if (_activeCoroutines[i].Routine == routine)
                {
                    _coroutinePool.Return(_activeCoroutines[i]);
                    _freeIndicesQueue.Enqueue(_activeCoroutines[i].Index);
                    return i;
                }
            }

            return -1;
        }

        int index = GetEqualRoutineIndex();

        if (index is -1) return;

        _activeCoroutines.RemoveAt(index);
    } 
    public void RemoveRoutine(CustomCoroutineToken token)
    {
        _activeCoroutines[token.Index].OnStop();
        
        _coroutinePool.Return(_activeCoroutines[token.Index]);
        _freeIndicesQueue.Enqueue(token.Index);
        _activeCoroutines.RemoveAt(token.Index);
    }

    public void RemoveAllRoutines()
    {
        foreach (CustomCoroutineInternal activeCoroutine in _activeCoroutines)
        {
            _coroutinePool.Return(activeCoroutine);
        }
        _activeCoroutines.Clear();
        
        _availableIndicesList.Clear();
        _freeIndicesQueue.Clear();
        Resize(settings.MaxCoroutines);
    }
    #endregion

    #region Queries
    public bool AnyExist(string tag)
    {
        return _usingIndicesDictionary.TryGetValue(tag, out List<int> indices) && indices.Count != 0;
    }
    public bool HasRoutine(string tag, CustomCoroutineToken token)
    {
        return _usingIndicesDictionary.ContainsKey(tag) && _usingIndicesDictionary[tag].Contains(token.Index);
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

            if (current is not Task task) return false;
            if (!behaviour.Token.SyncOrAsync) continue;
            if (task.IsCanceled || task.IsCompleted || task.IsCompletedSuccessfully || task.IsFaulted)
                continue;

            return false;

        } while (behaviour.Routine.MoveNext());

        return true;
    }
    bool ProcessDebugCoroutine(ref CustomCoroutineInternal behaviour)
    {
        try
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

                if (current is not Task task) return false;
                if (!behaviour.Token.SyncOrAsync) continue;
                if (task.IsCanceled || task.IsCompleted || task.IsCompletedSuccessfully || task.IsFaulted)
                    continue;

                return false;

            } while (behaviour.Routine.MoveNext());

            return true;
        }
        catch (Exception e)
        {
            Debug.Log(@$"Coroutine Index : {behaviour.Index} / Token Index : {behaviour.token.Index} |
 Exception: {e}");
            throw;
        }
    }

    private bool ProcessCoroutine(in IEnumerator routine, CustomCoroutineToken token)
    {
        do
        {
            if (routine is null)
                break;
            if (token is { Pause: true, Start: false, Stop: false })
                return false;
            if(token is {Stop: true})
                break;

            object current = routine.Current;
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
  
    private bool ProcessDebugCoroutine(in IEnumerator routine, CustomCoroutineToken token)
    {
        try
        {
            do
            {
                if (routine is null)
                    break;
                if (token is { Pause: true, Start: false, Stop: false })
                    return false;
                if (token is { Stop: true })
                    break;

                object current = routine.Current;
                switch (current)
                {
                    case WaitWhile { keepWaiting: true }:
                        return false;
                    case WaitWhile { keepWaiting: false }:
                        continue;
                    case WaitUntil { keepWaiting: true }:
                        return false;
                    case WaitUntil { keepWaiting: false }:
                        continue;
                    case CustomYieldInstruction { keepWaiting: true }:
                        return false;
                    case CustomYieldInstruction { keepWaiting: false }:
                        continue;
                    case WaitForEndOfFrame:
                    case WaitForFixedUpdate:
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
        catch (Exception e)
        {
            Debug.Log($@"Sub Coroutine Index : {token.Index} |
 Exception: {e}");
            throw;
        }
    }

    #endregion
}
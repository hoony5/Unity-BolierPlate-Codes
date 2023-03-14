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
    private CustomCoroutineInternal[] _activeCoroutines;
    private List<int> _availableIndicesList;
    private Queue<int> _freeIndicesQueue;
    private Dictionary<string, List<int>> _usingIndicesDictionary;

    private bool _isStart;
    private bool _isPause;
    private bool _isStop;

    void Start()
    {
        _coroutinePool = new CustomCoroutineInternalPool();
        _usingIndicesDictionary = new Dictionary<string, List<int>>(settings.MaxCoroutines);
        Resize(settings.MaxCoroutines);
    }

    public void Resize(int capacity)
    {
        if (_availableIndicesList is null)
            _availableIndicesList = new List<int>(capacity);
        else
            _availableIndicesList.AddRange(Enumerable.Range(start: _availableIndicesList.Count, count: capacity));

        if (_freeIndicesQueue is null)
        {
            _freeIndicesQueue = new Queue<int>(capacity);
            InitializeAvailableIndices(start: 0, count: settings.MaxCoroutines);
        }
        else
            InitializeAvailableIndices(start: _availableIndicesList.Count, count: capacity);

        if(_activeCoroutines is null)
        {
            _activeCoroutines = new CustomCoroutineInternal[settings.MaxCoroutines];
            for (int i = 0; i < _activeCoroutines.Length; i++)
            {
                CustomCoroutineInternal temp = _activeCoroutines[i];
                temp.index = -1;
                _activeCoroutines[i] = temp;
            }
        }
        else
            Array.Resize(ref _activeCoroutines, settings.MaxCoroutines * 2);
    }

    private void InitializeAvailableIndices(int start, int count)
    {
        for (int i = start; i < count; i++)
        {
            _availableIndicesList.Add(i);
            _freeIndicesQueue.Enqueue(i);
        }
    }

    private void OnUpdateRoutines()
    {
        for (int i = 0; i < _activeCoroutines.Length; i++)
        {
            if(_activeCoroutines[i].index != -1)
                ProcessCoroutine(_activeCoroutines[i]);
        }
    }
    private async Task OnUpdateRoutinesAsync()
    {
        await Task.Yield();
        
        for (int i = 0; i < _activeCoroutines.Length; i++)
        {
            if(_activeCoroutines[i].index != -1)
                ProcessCoroutine(_activeCoroutines[i]);
        }
    }

    private void RemoveAllRoutines()
    {
        for (int i = _activeCoroutines.Length - 1; i >= 0; i--)
        {
            CustomCoroutineInternal temp = _activeCoroutines[i];
            temp.index = -1;
            _activeCoroutines[i] = temp;
            _coroutinePool.Return(_activeCoroutines[i]);
        }
    }

    public void Update()
    {
        if (settings.OptimizeMode) return;
        if (_activeCoroutines.Length == 0) return;

        if (_isStart && !_isPause && !_isStop)
            OnUpdateRoutines();

        if (_isStop)
            RemoveAllRoutines();
    }

    private void FixedUpdate()
    {
        if (!settings.OptimizeMode) return;
        if (_activeCoroutines.Length == 0) return;

        if (_isStart && !_isPause && !_isStop)
            OnUpdateRoutines();

        if (_isStop)
            RemoveAllRoutines();
    }

    private bool ProcessCoroutine(CustomCoroutineInternal behaviour)
    {

        bool ProcessNestedCoroutine(IEnumerator coroutine)
        {
            int nestedIndex;
            if (_freeIndicesQueue.Count > 0)
            {
                nestedIndex = _freeIndicesQueue.Dequeue();
            }
            else
            {
                Resize(settings.MaxCoroutines);
                nestedIndex = _activeCoroutines.Length;
            }

            CustomCoroutineInternal nestedBehaviour =
                new CustomCoroutineInternal(nestedIndex, coroutine, new CustomCoroutineToken(), true);

            do
            {
                object currentYieldInstruction = nestedBehaviour.routine.Current;

                if (behaviour.routine is null)
                    break;

                if (currentYieldInstruction is null or WaitForFixedUpdate or WaitForEndOfFrame)
                {
                    behaviour.UpdateWaiting();
                    if (behaviour.KeepWaiting())
                        return false;
                }
                else if (currentYieldInstruction is WaitForTime waitForTime)
                {
                    if (waitForTime.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitUntil waitUntil)
                {
                    if (waitUntil.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitWhile waitWhile)
                {
                    if (waitWhile.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitUpdate waitUpdate)
                {
                    if (waitUpdate.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitEvent waitEvent)
                {
                    if (waitEvent.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is Task task)
                {
                    if (!task.IsCompleted && behaviour.token.SyncOrAsync)
                    {
                        return false;   
                    }
                }
                else if (currentYieldInstruction is CustomCoroutineInternal nestedCoroutine)
                {
                    if (!ProcessNestedCoroutine(nestedCoroutine.routine))
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is IEnumerator nestedRoutine)
                {
                    if (!ProcessNestedCoroutine(nestedRoutine))
                    {
                        return false;
                    }
                }
            } while (coroutine.MoveNext());

            _freeIndicesQueue.Enqueue(nestedIndex);
            return !nestedBehaviour.KeepWaiting();
        }

        if (settings.DebugMode)
        {
            try
            {
                do
                {
                    if (behaviour.routine is null)
                        break;
                    if (behaviour.token.Stop)
                        break;

                    if (behaviour.token.Pause)
                        return false;

                    // Handle the current yield instruction
                    object currentYieldInstruction = behaviour.routine.Current;

                    if (currentYieldInstruction is null or WaitForFixedUpdate or WaitForEndOfFrame)
                    {
                        behaviour.UpdateWaiting();
                        if (behaviour.KeepWaiting())
                            return false;
                    }
                    else if (currentYieldInstruction is WaitForTime waitForTime)
                    {
                        if (waitForTime.keepWaiting)
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is WaitUntil waitUntil)
                    {
                        if (waitUntil.keepWaiting)
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is WaitWhile waitWhile)
                    {
                        if (waitWhile.keepWaiting)
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is WaitUpdate waitUpdate)
                    {
                        // WaitUpdate will automatically alternate between waiting and not waiting
                        // No additional handling is needed here
                        if (waitUpdate.keepWaiting)
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is WaitEvent waitEvent)
                    {
                        if (waitEvent.keepWaiting)
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is Task task)
                    {
                        // Wait for the Task to complete
                        if (!task.IsCompleted && behaviour.token.SyncOrAsync)
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is CustomCoroutineInternal nestedCoroutine)
                    {
                        // Process the nested CustomCoroutineInternal
                        if (!ProcessNestedCoroutine(nestedCoroutine.routine))
                        {
                            return false;
                        }
                    }
                    else if (currentYieldInstruction is IEnumerator nestedRoutine)
                    {
                        // Process the nested IEnumerator
                        if (!ProcessNestedCoroutine(nestedRoutine))
                        {
                            return false;
                        }
                    }
                } while (behaviour.routine.MoveNext());

                _activeCoroutines[behaviour.index] = default;
                _coroutinePool.Return(behaviour);
                _freeIndicesQueue.Enqueue(behaviour.index);
                return true;
            }
            catch (Exception e)
            {
                string tag = "";
                foreach (var kvp in _usingIndicesDictionary)
                {
                    if (kvp.Value.Contains(behaviour.index))
                        tag = kvp.Key;
                }

                if (string.IsNullOrEmpty(tag))
                    Debug.Log($" Index : {behaviour.index} | IsNested : {behaviour.isNested}|");
                else
                    Debug.Log($" Tag : {tag} | Index : {behaviour.index} | IsNested : {behaviour.isNested}");
                throw;
            }
        }

        else
        {
            do
            {
                if (behaviour.routine is null)
                    break;
                if (behaviour.token.Stop)
                    break;
                if (behaviour.token.Pause)
                    return false;
                // Handle the current yield instruction
                object currentYieldInstruction = behaviour.routine.Current;

                if (currentYieldInstruction is null or WaitForFixedUpdate or WaitForEndOfFrame)
                {
                    behaviour.UpdateWaiting();
                    if (behaviour.KeepWaiting())
                        return false;
                }
                else if (currentYieldInstruction is WaitForTime waitForTime)
                {
                    if (waitForTime.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitUntil waitUntil)
                {
                    if (waitUntil.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitWhile waitWhile)
                {
                    if (waitWhile.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitUpdate waitUpdate)
                {
                    // WaitUpdate will automatically alternate between waiting and not waiting
                    // No additional handling is needed here
                    if (waitUpdate.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is WaitEvent waitEvent)
                {
                    if (waitEvent.keepWaiting)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is Task task)
                {
                    // Wait for the Task to complete
                    if (!task.IsCompleted && behaviour.token.SyncOrAsync)
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is CustomCoroutineInternal nestedCoroutine)
                {
                    // Process the nested CustomCoroutineInternal
                    if (!ProcessNestedCoroutine(nestedCoroutine.routine))
                    {
                        return false;
                    }
                }
                else if (currentYieldInstruction is IEnumerator nestedRoutine)
                {
                    // Process the nested IEnumerator
                    if (!ProcessNestedCoroutine(nestedRoutine))
                    {
                        return false;
                    }
                }
            } while (behaviour.routine.MoveNext());
        }

        _activeCoroutines[behaviour.index] = default;
        _coroutinePool.Return(behaviour);
        _freeIndicesQueue.Enqueue(behaviour.index);
        return true;
    }

   public void StartRoutines()
   {
       _isStart = true;
       _isPause = false;
       _isStop = false;
   }
   public void PauseRoutines()
   {
       _isStart = false;
       _isPause = true;
       _isStop = false;
   }
   public void StopRoutines()
   {
       _isStart = false;
       _isPause = false;
       _isStop = true;
   }

    public CustomCoroutineToken AddRoutine(IEnumerator routine)
    {
        if (_freeIndicesQueue.Count == 0)
        {
            Resize(settings.MaxCoroutines);
        }

        int index = _freeIndicesQueue.Dequeue();
        CustomCoroutineToken token = new CustomCoroutineToken(index, start: true, pause: false, stop: false, syncOrAsync: true);
        CustomCoroutineInternal newBehaviour = _coroutinePool.Get().Init(index, routine, token ,false);
        _activeCoroutines[index] = newBehaviour;

        return token;
    }
    public CustomCoroutineToken AddRoutineWithTag(string tag, IEnumerator routine)
    {
        if (_freeIndicesQueue.Count == 0)
        {
            Resize(settings.MaxCoroutines);
        }

        int index = _freeIndicesQueue.Dequeue();

        // Update the dictionary with the new index
        if (_usingIndicesDictionary.TryGetValue(tag, out List<int> indicesList))
        {
            indicesList.Add(index);
        }
        else
        {
            _usingIndicesDictionary[tag] = new List<int> { index };
        }

        CustomCoroutineToken token = new CustomCoroutineToken(index, start: true, pause: false, stop: false, syncOrAsync: true);
        CustomCoroutineInternal newBehaviour = _coroutinePool.Get().Init(index, routine, token, false);
        _activeCoroutines[index] = newBehaviour;

        return token;
    }

    public void ResumeRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnStart();
    }

    public void PauseRoutine(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnPause();
    }

    public void ConvertToAsync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnAsync();
    }

    public void ConvertToSync(CustomCoroutineToken token)
    {
        if (_activeCoroutines.Length <= token.Index) return;
        _activeCoroutines[token.Index].OnSync();
    }
    public bool AnyExist(string tag)
    {
        return _usingIndicesDictionary.ContainsKey(tag) && _usingIndicesDictionary[tag].Count > 0;
    }

    public bool HasRoutine(string tag, CustomCoroutineToken token)
    {
        if (_usingIndicesDictionary.TryGetValue(tag, out var indicesList))
        {
            return indicesList.Contains(token.Index);
        }
        return false;
    }

    public bool HasRoutine(CustomCoroutineToken token)
    {
        foreach (CustomCoroutineInternal activeCoroutine in _activeCoroutines)
        {
            if (activeCoroutine.index == token.Index) return true;
        }

        return false;
    }
    public bool HasRoutine(int index)
    {
        foreach (CustomCoroutineInternal activeCoroutine in _activeCoroutines)
        {
            if (activeCoroutine.index == index) return true;
        }

        return false;
    }
}
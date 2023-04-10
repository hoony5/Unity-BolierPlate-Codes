using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

[System.Serializable]
public class Timer
{
    public bool IsDebugOn { get; set; }
    public Object Owner { get; set; }
    public bool IsLoop { get; set; }
    public bool IsPause { get; set; }
    public ushort Index { get; set; }
    
    private float _runningTime;
    private float _chasingTime;
    private float _threshold;
    /// <summary>
    /// limited time, when effect time is over
    /// </summary>
    public float MaxTime { get; private set; }
    
    public float CurrentTime => _chasingTime;
    /// <summary>
    /// effect time
    /// </summary>
    public float Duration { get; private set; }

    [SerializeField] private UnityEvent<Object> onTime;
    
    public Timer(ushort index, float maxTime, float duration, bool isLoop)
    {
        IsDebugOn = false;
        MaxTime = maxTime;
        Duration = duration;
        _runningTime = _chasingTime =  0;
        _threshold = 1;
        IsLoop = isLoop;
        IsPause = false;
        Index = index;
    }

    private bool checkLimitedTimers()
    {
        if(_runningTime >= MaxTime)
        {
            _runningTime = _chasingTime = MaxTime;
            if (IsLoop)
                _runningTime = _chasingTime = 0;
            else
            {
                _runningTime = _chasingTime = MaxTime;
                return false;
            }
        }
        if(_runningTime >= Duration)
        {
            if (IsLoop)
                _runningTime = _chasingTime = 0;
            else
            {
                _runningTime = _chasingTime = Duration;
                return false;
            }
        }

        return true;
    }

    private bool keepCountingTime()
    {
        if (Owner is null) return false;
        
        if(IsPause)
        {
            _chasingTime = _runningTime;
            return false;
        }

        return checkLimitedTimers();
    }
    public bool CheckCountPerSecond(float deltaTime)
    {
        if (!keepCountingTime())
            return false;
        
        _runningTime += deltaTime;
        // once per second
        if (_runningTime < (int)(_chasingTime + _threshold)) return false;
        
        _chasingTime = (int)_runningTime;
        if(IsDebugOn)
            Debug.Log($"currentTime =  {_chasingTime}");
        
        onTime?.Invoke(Owner);
        return true;
    }
    public void RegisterOnTime(UnityAction<Object> action)
    {
        onTime.AddListener(action);
    }
    public void UnRegisterOnTime(UnityAction<Object> action)
    {
        onTime.RemoveListener(action);
    }
    public void SetOwner(Object owner)
    {
        Owner = owner;
    }
    public void AddMaxTime(float maxTime)
    {
        MaxTime += maxTime;
    }
    public void SetMaxTime(float maxTime)
    {
        MaxTime = maxTime;
    }
    public void Pause()
    {
        IsPause = true;
    }
    public void Start()
    {
        IsPause = false;
    }
    public void AddDuration(float duration)
    {
        Duration += duration;
    }
    public void SetDuration(float duration)
    {
        Duration = duration;
    }
    public void SetThreshold(float threshold)
    {
        _threshold = threshold;
    }
    public void SetLoop(bool isLoop)
    {
        IsLoop = isLoop;
    }
    public void Reset()
    {
        _runningTime = _chasingTime = 0;
        onTime.RemoveAllListeners();
    }
}
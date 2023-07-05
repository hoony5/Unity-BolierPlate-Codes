using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class Timer
{
    [field:SerializeField]public bool IsDebugOn { get; set; }
    [field:SerializeField]public Object Owner { get; set; }
    [field:SerializeField]public bool IsLoop { get; set; }
    [field:SerializeField]public bool IsPause { get; set; }
    [field:SerializeField]public ushort Index { get; set; }
    
    [SerializeField]private float _runningTime;
    [SerializeField]private float _chasingTime;
    [SerializeField]private float _threshold;
    /// <summary>
    /// limited time, when effect time is over
    /// </summary>
    [field:SerializeField] public float MaxTime { get; private set; }
    
    public float CurrentTime => _chasingTime;
    /// <summary>
    /// effect time
    /// </summary>
    [field:SerializeField] public float Duration { get; private set; }

    private List<Action<Object>> onTime;
    
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
        onTime = new List<Action<Object>>(32);
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

        if (!IsPause) return checkLimitedTimers();
        
        _chasingTime = _runningTime;
        return false;
    }

    private void executeEvents()
    {
        if(Owner is null || onTime is null || onTime.Count == 0) return;

        for(var i = 0 ; i < onTime?.Count; i++)
            onTime[i]?.Invoke(Owner);
    }
    public void AddOnTime(Action<Object> action)
    {
        onTime.Add(action);
    }
    public void RemoveOnTime(Action<Object> action)
    {
        onTime.Remove(action);
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
        
        executeEvents();
        return true;
    }

    public bool IsEnd()
    {
        return ((int)_chasingTime == (int)Duration || (int)_chasingTime == (int)MaxTime) && !IsLoop;
    }
    public void SetOwner(Object owner)
    {
        Owner = owner;
    }
    public void ReleaseOwner()
    {
        Owner = null;
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
        IsPause = true;
        
        if(onTime is null) 
            onTime = new List<Action<Object>>(32);
        else
            onTime.Clear();
    }
}
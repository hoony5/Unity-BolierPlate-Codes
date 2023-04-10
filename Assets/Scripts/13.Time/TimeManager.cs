using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool isRunning;
    [SerializeField] private bool creatingOnAwake;
    [SerializeField] private List<Timer> timers = new List<Timer>(64);
    [SerializeField] private ushort maxCount;
    private void Start()
    {
        if(creatingOnAwake)
            createTimers(0, maxCount);
    }

    private void Update()
    {
        if (!isRunning) return;
        
        for (var index = 0; index < timers.Count; index++)
        {
            Timer timer = timers[index];
            bool canContinued = !timer.CheckCountPerSecond(Time.deltaTime);
            if(canContinued) continue;
            if(!timer.IsEnd()) continue;
            releaseTimer(timer);
        }
    }

    public void Reset()
    {
        foreach (Timer timer in timers)
        {
            timer.Reset();
            timer.ReleaseOwner();
        }
    }
    private void createTimers(int startCount, int maxCount)
    {
        for(var i = startCount ; i < startCount + maxCount ; i++)
            timers.Add(new Timer((ushort)i, 60, 3, false));        
    }
    
    public Timer GetFreeTimer()
    {
        for (var index = 0; index < timers.Count; index++)
        {
            if (timers[index].Owner is null) return timers[index];
        }

        int startCount = timers.Count;
        createTimers(startCount, maxCount);
        return timers[startCount];
    }

    private void releaseTimer(Timer timer)
    {
        timer.Reset();
        timer.ReleaseOwner();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Clock : Singleton<Clock>
{
    private const int InitialTimersCapacity = 64;

    [SerializeField] private bool isRunning;
    [SerializeField] private bool creatingOnAwake;
    [SerializeField] private List<Timer> timers = new List<Timer>(InitialTimersCapacity);
    [SerializeField] private ushort maxCount;

    public async Task DispathcerTest()
    {
        Debug.Log($"before");
        await Task.Yield();
        Debug.Log($"after");
    }

    private void Start()
    {
        if(creatingOnAwake)
            createTimers(0, maxCount);
    }

    private void Update()
    {   
        if (!isRunning || timers.Count == 0) return;

        foreach (Timer timer in timers)
        {
            bool canContinued = !timer.CheckCountPerSecond(Time.deltaTime);
            if(canContinued) continue;
            if(!timer.IsEnd()) continue;
            releaseTimer(timer);
        }
    }

    public void Reset()
    {
        foreach (var timer in timers)
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
        foreach (var timer in timers)
        {
            if (timer.Owner is null) return timer;
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
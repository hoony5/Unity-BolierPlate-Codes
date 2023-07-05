using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[ToDo("싱글톤으로 해야하나?")]
public class Clock : MonoBehaviour
{
    private static Clock _instance;
    public static Clock Instance => _instance;
    
    [SerializeField] private bool isRunning;
    [SerializeField] private bool creatingOnAwake;
    [SerializeField] private List<Timer> timers = new List<Timer>(64);
    [SerializeField] private ushort maxCount;

    public async Task DispathcerTest()
    {
        Debug.Log($"before");
        await Task.Yield();
        Debug.Log($"after");
    }
    [ToDo("싱글톤에 대한 고민이 필요하다.")]
    private void Awake()
    {
        if (_instance is null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        DestroyImmediate(this);
    }

    private void Start()
    {
        if(creatingOnAwake)
            createTimers(0, maxCount);
    }

    private void Update()
    {   
        if (!isRunning || timers.Count == 0) return;

        int timersCount = timers.Count;
        for (var index = 0; index < timersCount; index++)
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
        int timersCount = timers.Count;
        for (var index = 0; index < timersCount; index++)
        {
            Timer timer = timers[index];
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
        var timersCount = timers.Count;
        for (var index = 0; index < timersCount; index++)
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
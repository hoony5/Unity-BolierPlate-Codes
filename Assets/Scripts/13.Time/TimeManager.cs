using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private List<Timer> timers = new List<Timer>(64);
    [SerializeField] private bool createAwake;
    [SerializeField] private ushort maxCount;
    private void Start()
    {
        if (!createAwake) return;
        createTimers();
    }

    private void createTimers()
    {
        for(var i = 0 ; i < maxCount ; i++)
            timers.Add(new Timer((ushort)i, 60, 3, false));        
    }

    private void Update()
    {
        foreach (Timer timer in timers)
        {
            bool canContinued = !timer.CheckCountPerSecond(Time.deltaTime);
            if(canContinued) continue;
        }
    }
}
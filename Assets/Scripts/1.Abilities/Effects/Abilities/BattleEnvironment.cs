using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEnvironment
{
    public int areaMask;
    public float hitRate;
    public float duration;
    public float threshold;
    public Collider[] effectTargets;
    private Dictionary<string, Timer> effectTimers;

    public BattleEnvironment()
    {
        effectTargets = Array.Empty<Collider>();
        effectTimers = new Dictionary<string, Timer>(24);
    }

    public void Reset()
    {
        areaMask = -1;
        hitRate = 0;
        duration = 0;
        threshold = 0;
        effectTargets = Array.Empty<Collider>();
        if(effectTimers is null) 
            effectTimers = new Dictionary<string, Timer>();
        else
            effectTimers.Clear();
    }
    
    public void SetEffectTimer(string effectName, Timer timer)
    {
        if(effectTimers.ContainsKey(effectName))
            effectTimers[effectName] = timer;
        else
            effectTimers.Add(effectName, timer);
    }
}
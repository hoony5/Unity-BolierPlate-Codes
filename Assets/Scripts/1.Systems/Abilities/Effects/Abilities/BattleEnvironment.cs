using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable]
public class BattleEnvironment
{
    [field:SerializeField] public int AreaMask { get; private set; }
    [field:SerializeField] public float HitRate { get; private set; }
    [field:SerializeField] public float Duration { get; private set; }
    [field:SerializeField] public float Threshold { get; private set; }
    [field:SerializeField] public Collider[] EffectTargets { get; private set; }
    [field: SerializeField] private SerializedDictionary<string, Timer> EffectTimers { get; set; }

    public BattleEnvironment()
    {
        EffectTargets = Array.Empty<Collider>();
        EffectTimers = new SerializedDictionary<string, Timer>(24);
    }

    public void Reset()
    {
        AreaMask = -1;
        HitRate = 0;
        Duration = 0;
        Threshold = 0;
        EffectTargets = Array.Empty<Collider>();
        if(EffectTimers is null) 
            EffectTimers = new SerializedDictionary<string, Timer>();
        else
            EffectTimers.Clear();
    }
    
    public void SetEffectTimer(string effectName, Timer timer)
    {
        EffectTimers[effectName] = timer;
    }
}
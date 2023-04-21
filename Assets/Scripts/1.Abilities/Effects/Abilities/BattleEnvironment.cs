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
        effectTimers = new Dictionary<string, Timer>(24);
    }

    public void Rest()
    {
        areaMask = -1;
        hitRate = 0;
        duration = 0;
        
    }
}
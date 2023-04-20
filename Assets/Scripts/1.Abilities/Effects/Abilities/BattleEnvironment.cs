using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEnvironment
{
    public int areaMask;
    public float duration;
    public float threshold;
    public Collider[] effectTargets;
}
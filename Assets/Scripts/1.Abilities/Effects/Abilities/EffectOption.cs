using UnityEngine;

[System.Serializable]
public class EffectOption
{
    public int areaMask;
    public float duration;
    public float threshold;
    public Collider[] effectTargets;
}
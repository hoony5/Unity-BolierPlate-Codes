using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CastAbility : Effect, IThresholdAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    public bool HasThresholdPassed(float threshold)
    {
        return  threshold >= Threshold;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
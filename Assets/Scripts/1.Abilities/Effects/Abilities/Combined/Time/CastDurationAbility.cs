using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CastDurationAbility : Effect, ICastDurationAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public bool HasTimePassed(float currentDuration)
    {
       return currentDuration >= Duration;
    }
    public bool HasThresholdPassed(float threshold, bool isHit)
    {
        return  threshold >= Threshold && !isHit;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
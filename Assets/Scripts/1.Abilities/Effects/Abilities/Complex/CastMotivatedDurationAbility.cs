using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CastMotivatedDurationAbility : Effect, ICastMotivatedDurationAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public string MotivationStat { get; set; }
    [field: SerializeField] public float Motivation { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field: SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public bool HasTimePassed(float currentDuration)
    {
        return currentDuration >= Duration;
    }
    public bool HasThresholdPassed(float threshold)
    {
        return threshold >= Threshold;
    }
    public bool IsMotivatedWhenGreater(float motivation)
    {
        return motivation > Motivation;
    }
    public bool IsMotivatedWhenLess(float motivation)
    {
        return motivation < Motivation;
    }
    public bool IsMotivatedWhenApproximately(float motivation, float threshold = 0.01f)
    {
        return motivation - Motivation <= threshold;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
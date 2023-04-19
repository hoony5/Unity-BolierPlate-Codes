
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DurationMotivatedAbility : Effect, IDurationMotivatedAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Motivation { get; set; }
    [field:SerializeField] public float Chance { get; set; }

    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public bool HasTimePassed(float currentDuration)
    {
        return currentDuration >= Duration;
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
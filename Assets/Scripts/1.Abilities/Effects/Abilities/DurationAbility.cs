using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DurationAbility : Effect, IDurationAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    public bool HasTimePassed(float currentDuration)
    {
        return currentDuration >= Duration;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
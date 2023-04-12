using System.Collections.Generic;
using UnityEngine;

public class DurationAbility : Effect, IDurationAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
}
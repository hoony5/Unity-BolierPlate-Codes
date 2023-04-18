using System.Collections.Generic;
using UnityEngine;

public class CastDurationAbility : Effect, ICastDurationAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}
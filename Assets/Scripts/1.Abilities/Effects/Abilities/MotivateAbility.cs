using System.Collections.Generic;
using UnityEngine;

public class MotivateAbility : Effect, IMotivatedAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
}
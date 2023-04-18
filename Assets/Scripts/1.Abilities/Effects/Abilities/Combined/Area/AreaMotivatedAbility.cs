using System.Collections.Generic;
using UnityEngine;

public class AreaMotivatedAbility : Effect, IAreaMotivatedAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
}
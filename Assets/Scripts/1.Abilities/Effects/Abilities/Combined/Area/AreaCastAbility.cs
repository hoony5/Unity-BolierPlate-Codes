using System.Collections.Generic;
using UnityEngine;

public class AreaCastAbility : Effect, IAreaCastAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
   
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}
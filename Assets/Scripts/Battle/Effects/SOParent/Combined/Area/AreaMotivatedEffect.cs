using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaMotivatedEffect", menuName = "ScriptableObject/Battle/Combined/Area/AreaMotivatedEffect", order = 0)]
public class AreaMotivatedEffect : EffectInfoBase, IAreaEffect, IMotivatedEffect
{
    [field:SerializeField] public float Range { get; set; }

    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    
    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
}
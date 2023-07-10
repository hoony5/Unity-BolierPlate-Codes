using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectAbility
{
    [field:SerializeField]public string EffectName { get; set; }
    [field:SerializeField]public List<EffectAbilityInfo> AbilityInfos { get; set; }

    public EffectAbility(string effectName, List<EffectAbilityInfo> abilityInfos)
    {
        this.EffectName = effectName;
        this.AbilityInfos = abilityInfos;
    }
}
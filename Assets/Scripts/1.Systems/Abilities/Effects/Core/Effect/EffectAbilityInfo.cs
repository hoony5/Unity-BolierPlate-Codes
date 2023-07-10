using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectAbilityInfo
{
    [field:SerializeField] public string AbilityName { get; set; }
    [field:SerializeField] public List<EffectAbilityStat> AbtilityStats { get; set; }

    public EffectAbilityInfo(string abilityName)
    {
        this.AbilityName = abilityName;
        if (abilityName == "Empty") return;
        AbtilityStats = new List<EffectAbilityStat>(16);
    }
}
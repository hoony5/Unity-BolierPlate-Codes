
using System.Collections.Generic;

[System.Serializable]
public class EffectAbility
{
    public string effectName;
    public List<EffectAbilityInfo> abilityInfos;

    public EffectAbility(string effectName, List<EffectAbilityInfo> abilityInfos)
    {
        this.effectName = effectName;
        this.abilityInfos = abilityInfos;
    }
}

using System.Collections.Generic;

[System.Serializable]
public class EffectAbilityInfo
{
    public string abilityName;
    public List<EffectAbilityStat> abtilityStats;

    public EffectAbilityInfo(string abilityName)
    {
        this.abilityName = abilityName;
        if (abilityName == "Empty") return;
        abtilityStats = new List<EffectAbilityStat>(16);
    }
}
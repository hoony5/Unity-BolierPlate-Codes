using System.Collections.Generic;
using UnityEngine;

public class EffectAbilityManager : MonoBehaviour
{
    public AbilityResourceInfo[] abilityResourceInfos;

    /*public List<EffectAbility> LoadAllAbilityInfos()
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            List<string[]> infos = info.GetInfo();
            var loadedData = LoadEffectAbilityInfo(infos);
        }
        return 
    }*/
    private List<EffectAbility> LoadEffectAbilityInfo(List<string[]> values)
    {
        List<EffectAbility> effectAbilities = new List<EffectAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility effectAbility = new EffectAbility(
                effectName: rowDatas[0],
                abilityName: rowDatas[1],
                statRawName: rowDatas[2],
                applyTargetType:rowDatas[3],
                calculationType:rowDatas[4],
                value:float.TryParse(rowDatas[5], out float Value) ? Value : 0,
                min:float.TryParse(rowDatas[6], out float Min) ? Min : 0,
                max:float.TryParse(rowDatas[7], out float Max) ? Max : 0
            );
            effectAbilities.Add(effectAbility);
        }

        return effectAbilities;
    }

}
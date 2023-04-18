using System.Collections.Generic;
using UnityEngine;

public class EffectAbilityManager : MonoBehaviour
{
    [SerializeField] private AllEffectAbilities allEffectAbilities;
    public AbilityResourceInfo[] abilityResourceInfos;
    
    // Load Start or Button Attributes
    public void LoadAllAbilityInfos()
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            List<string[]> infos = info.GetAbilityDatas();
            (List<EffectAbility> ability, List<EffectAbilityInfo> abilityInfo) loadedData = LoadEffectAbilityInfo(infos);
            allEffectAbilities.SetEffectInfomations(loadedData.ability.ToArray(),loadedData.abilityInfo.ToArray());
        }
    }
    private (List<EffectAbility> ability, List<EffectAbilityInfo> abilityInfo) LoadEffectAbilityInfo(List<string[]> values)
    {
        List<EffectAbility> result = new List<EffectAbility>(32);
        List<EffectAbilityInfo> result2 = new List<EffectAbilityInfo>(32);
        List<EffectAbilityInfo> resultAbilityInfos = new List<EffectAbilityInfo>(32);
        List<EffectAbilityStat> effectAbilityStats = null;
        
        string currentEffectName = string.Empty;
        string nextEffectName = string.Empty;
        string currentAbilityName = string.Empty;
        string nextAbilityName = string.Empty;

        for (int index = 0; index < values.Count - 1; index++)
        {
            string[] rowDatas = values[index];

            // Is First?
            if (index == 0)
            {
                currentEffectName = rowDatas[0];
                currentAbilityName = rowDatas[1];
                effectAbilityStats = new List<EffectAbilityStat>(values.Count);
            }
            
            nextEffectName = values[index + 1][0];
            nextAbilityName = values[index + 1][1];
            // Add Status 
            EffectAbilityStat effectAbilityStat = new EffectAbilityStat(
                statRawName: rowDatas[2],
                applyTargetType: rowDatas[3],
                calculationType: rowDatas[4],
                value: float.TryParse(rowDatas[5], out float Value) ? Value : 0,
                min: int.TryParse(rowDatas[6], out int Min) ? Min : 0,
                max: int.TryParse(rowDatas[7], out int Max) ? Max : 0
            );
            
            effectAbilityStats?.Add(effectAbilityStat);

            // Is Next AbilityInfo

            if (string.IsNullOrEmpty(nextAbilityName) ||
                currentAbilityName == nextAbilityName) continue;
            
            // Add AbilityInfo
            EffectAbilityInfo abilityInfo = new EffectAbilityInfo(currentAbilityName)
            {
                abtilityStats = effectAbilityStats
            };
            if(!resultAbilityInfos.Exists(effect => effect.abilityName == currentAbilityName))
            {
                resultAbilityInfos.Add(abilityInfo);
                result2.Add(abilityInfo);
            }
            
            // Init
            effectAbilityStats = new List<EffectAbilityStat>(values.Count);
            currentAbilityName = nextAbilityName;
            
            // Is Next EffectAbility
            if (string.IsNullOrEmpty(nextEffectName) ||
                currentEffectName == nextEffectName) continue;
            
            // Add EffectAbility
            EffectAbility ability = new EffectAbility(currentAbilityName, resultAbilityInfos);
            if(!result.Exists(i => i.effectName == ability.effectName))
                result.Add(ability);
            
            // Init
            resultAbilityInfos = new List<EffectAbilityInfo>(32);
            currentEffectName = nextEffectName;
        }

        return (result, result2);
    }

}   
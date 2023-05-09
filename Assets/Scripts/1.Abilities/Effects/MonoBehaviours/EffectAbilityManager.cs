using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectAbilityManager : MonoBehaviour
{
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get;private set; }
 
    [field: Header("All EffectAbility Types")]
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; private set; }
    private Dictionary<string, EffectAbility> EffectAbilityMap { get; set; }
    private EffectAbility EmptyAbility = new EffectAbility("Empty", null);
    
    [field: Header("All AbilityInfo Types")]
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilityInfos { get; private set; }
    private Dictionary<string, EffectAbilityInfo> EffectAbilityInfoMap { get; set; }
    private EffectAbilityInfo EmptyInfo = new EffectAbilityInfo("Empty");
    private void OnEnable()
    {
        Reset();
    }
    public EffectAbilityInfo GetEffectAbilityInfo(string abilityName)
    {
        if (EffectAbilityInfoMap is null || EffectAbilityInfoMap.Count == 0) return EmptyInfo;
        if (string.IsNullOrEmpty(abilityName) || !EffectAbilityInfoMap.ContainsKey(abilityName)) return EmptyInfo;

        return EffectAbilityInfoMap[abilityName];
    }
    public EffectAbility GetEffectAbility(string effectName)
    {
        
        if (EffectAbilityMap is null || EffectAbilityMap.Count == 0) return EmptyAbility;
        if (string.IsNullOrEmpty(effectName) || !EffectAbilityMap.ContainsKey(effectName)) return EmptyAbility;

        return EffectAbilityMap[effectName];
    }

    public void Reset()
    {
        // Effect Map
        EffectAbilityMap.Clear();
        EffectAbilityMap = EffectAbilities.ToDictionary(keyIs => keyIs.effectName, valueIs => valueIs);
        
        // Ability Map
        EffectAbilityInfoMap.Clear();
        EffectAbilityInfoMap = EffectAbilityInfos.ToDictionary(keyIs => keyIs.abilityName, valueIs => valueIs);
    }
    // Load Start or Button Attributes
    public void LoadAllAbilityInfos()
    {
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            List<string[]> infos = info.GetDataList();
            (List<EffectAbility> ability, List<EffectAbilityInfo> abilityInfo) loadedData = LoadEffectAbilityInfo(infos);

            EffectAbilities.AddRange(loadedData.ability.ToArray());
            EffectAbilityInfos.AddRange(loadedData.abilityInfo.ToArray());
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

        for (int index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];

            // Is First?
            currentEffectName = string.IsNullOrEmpty(rowDatas[0]) ? currentEffectName : rowDatas[0];
            currentAbilityName = string.IsNullOrEmpty(rowDatas[1]) ? currentAbilityName : rowDatas[1];
            effectAbilityStats ??= new List<EffectAbilityStat>(values.Count);
            
            nextEffectName = index <= values.Count - 1 ? values[index + 1][0] : currentEffectName;
            nextAbilityName = index <= values.Count - 1 ? values[index + 1][1] : currentEffectName;
            
            // Add Status 
            EffectAbilityStat effectAbilityStat = new EffectAbilityStat(
                statRawName: rowDatas[2],
                applyTargetType: rowDatas[3],
                calculationType: rowDatas[4],
                dataUnitType: rowDatas[5],
                value: float.TryParse(rowDatas[6], out float Value) ? Value : 0,
                min: int.TryParse(rowDatas[7], out int Min) ? Min : 0,
                max: int.TryParse(rowDatas[8], out int Max) ? Max : 0
            );
            
            effectAbilityStats?.Add(effectAbilityStat);

            // Is Next AbilityInfo

            if (string.IsNullOrEmpty(nextAbilityName) ||
                (currentAbilityName == string.Empty && nextAbilityName == string.Empty)) continue;
            
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
                (currentAbilityName == string.Empty && nextAbilityName == string.Empty)) continue;
            
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
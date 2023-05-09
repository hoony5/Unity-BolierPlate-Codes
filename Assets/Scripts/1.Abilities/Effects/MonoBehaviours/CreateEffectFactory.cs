using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateEffectFactory : MonoBehaviour
{
    [field:SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    
    private Dictionary<string, Effect> allOneAbilities = new Dictionary<string, Effect>();
    [field:SerializeField] public OneAbilityResourcesManager OneAbilityResourcesManager {get; private set;}
    [field:SerializeField] public EffectAbilityManager  EffectAbilityManager {get; private set;}
    [field:SerializeField] public EffectSearchStatInfoManager  EffectSearchSatInfoManager {get; private set;}
    [field:SerializeField] public EffectMotivationStatInfoManager  EffectMotivationManager {get; private set;}

    public void CreateEffectList()
    {
        LoadDocuments(OneAbilityResourcesManager.AbilityResourceInfos);
        LoadDocuments(EffectAbilityManager.AbilityResourceInfos);
        LoadDocuments(EffectSearchSatInfoManager.AbilityResourceInfos);
        LoadDocuments(EffectMotivationManager.AbilityResourceInfos);
        
        allOneAbilities = OneAbilityResourcesManager.LoadAllOneAbilities().ToDictionary(key => key.EffectName, value => value);
        // first
        EffectAbilityManager.LoadAllAbilityInfos();
        EffectSearchSatInfoManager.LoadAllSearchStatusItemInfo();
        EffectMotivationManager.LoadAllMotivationStatusInfoDatas();
    }
    
    public bool TryGetValue(string keyWhichIsEffectName, out Effect value)
    {
        return allOneAbilities.TryGetValue(keyWhichIsEffectName, out value);
    }

    private void LoadDocuments(AbilityResourceInfo[] abilityResourceInfos)
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            info.LoadExcelDocument(CsvReader);
        }    
    }
}
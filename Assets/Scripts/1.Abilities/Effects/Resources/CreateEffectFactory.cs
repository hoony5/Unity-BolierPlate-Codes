using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateEffectFactory : MonoBehaviour
{
    private Dictionary<string, Effect> allOneAbilities = new Dictionary<string, Effect>();
    [field:SerializeField] public OneAbilityResourcesManager OneAbilityResourcesManager {get; private set;}
    [field:SerializeField] public EffectAbilityManager  EffectAbilityManager {get; private set;}
    [field:SerializeField] public EffectSearchStatInfoManager  EffectSearchSatInfoManager {get; private set;}
    [field:SerializeField] public EffectMotivationStatInfoManager  EffectMotivationManager {get; private set;}

    public void CreateEffectList()
    {
        allOneAbilities = OneAbilityResourcesManager.LoadAllOneAbilities().ToDictionary(key => key.EffectName, value => value);
        // first
        EffectAbilityManager.LoadAllAbilityInfos();
        EffectSearchSatInfoManager.LoadAllSearchStatusItemInfo();
        EffectMotivationManager.LoadAllMotivationStatusInfoDatas();
    }
    
    public bool TryGetValue(string key, out Effect value)
    {
        return allOneAbilities.TryGetValue(key, out value);
    }
}
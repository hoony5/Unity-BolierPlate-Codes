using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "newEffectInfos", menuName = "ScriptableObject/Effect/EffectInfos")]
public class AllEffectAbilities : ScriptableObject
{
    [field: Header("All EffectAbility Types")]
    [field:SerializeField] private List<EffectAbility> EffectAbilities { get; set; }
    private Dictionary<string, EffectAbility> EffectAbilityMap { get; set; }
    private EffectAbility EmptyAbility = new EffectAbility("Empty", null);
    
    [field: Header("All AbilityInfo Types")]
    [field:SerializeField] private List<EffectAbilityInfo> EffectAbilityInfos { get; set; }
    private Dictionary<string, EffectAbilityInfo> EffectAbilityInfoMap { get; set; }
    private EffectAbilityInfo EmptyInfo = new EffectAbilityInfo("Empty");
    private void OnEnable()
    {
        Reset();
    }
    public void SetEffectInfomations(EffectAbility[] effectAbilities, EffectAbilityInfo[] effectAbilityInfos)
    {
        EffectAbilities.AddRange(effectAbilities);
        EffectAbilityInfos.AddRange(effectAbilityInfos);
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
}
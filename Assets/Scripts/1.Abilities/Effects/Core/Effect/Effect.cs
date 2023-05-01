using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Effect
{
    public string _effectName;
    [field: SerializeField] public AllStatusInfos AllStatusInfos { get; set; }
    [field:SerializeField] public AllEffectInfos AllEffectInfos{get;set;}
    [field:SerializeField] public ExpenseAbilityInfo ExpenseAbilityInfo{get;set;}
    [field:SerializeField] public EffectReferenceInfo ReferenceInfo{get;set;}
    [field:SerializeField] public EffectValueInfo ValueInfo{get;set;}
    public void SetAllEffectInfo(string path)
    {
        AllEffectInfos = (AllEffectInfos)Resources.Load(path);
    }
    public void LoadEffectData(string effectName)
    {
        if (!AllEffectInfos) return;
        
        _effectName = effectName;
        (ReferenceInfo, ValueInfo) = AllEffectInfos.GetEffectDataInformation(effectName);
    }
}
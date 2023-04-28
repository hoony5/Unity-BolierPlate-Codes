using UnityEngine;

[System.Serializable]
public class Effect
{
    public string _effectName;
    [SerializeField] protected AllStatusInfos _allStatusInfos;
    [SerializeField] protected AllEffectInfos _allEffectInfos;
    [SerializeField] protected ExpenseAbilityInfo _expenseAbilityInfo;
    [SerializeField] protected EffectReferenceInfo _referenceInfo;
    [SerializeField] protected EffectValueInfo _valueInfo;
    public void SetAllEffectInfo(string path)
    {
        _allEffectInfos = (AllEffectInfos)Resources.Load(path);
    }
    public void LoadEffectData(string effectName)
    {
        if (!_allEffectInfos) return;
        
        _effectName = effectName;
        (_referenceInfo, _valueInfo) = _allEffectInfos.GetEffectDataInformation(effectName);
    }
}
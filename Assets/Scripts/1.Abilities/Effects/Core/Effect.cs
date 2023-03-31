using UnityEngine;

[System.Serializable]
public class Effect
{
    [SerializeField] private string _effectName;
    [SerializeField] private AllEffectInfos _allEffectInfos;
    [SerializeField] private EffectReferenceInfo _referenceInfo;
    [SerializeField] private EffectValueInfo _valueInfo;

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
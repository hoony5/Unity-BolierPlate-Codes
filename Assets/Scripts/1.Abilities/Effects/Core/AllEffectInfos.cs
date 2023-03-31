using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "newEffectsNames", menuName = "ScriptableObject/Battle/EffectNames")]
public class AllEffectInfos : ScriptableObject
{
    private Dictionary<string, EffectReferenceInfo> _effectInfos = new Dictionary<string, EffectReferenceInfo>(128);
    private Dictionary<string, EffectValueInfo> _effectValueInfos = new Dictionary<string, EffectValueInfo>(128);

    public (EffectReferenceInfo referenceInfo, EffectValueInfo valueInfo)
        GetEffectDataInformation(string effectName)
    {
        return (_effectInfos[effectName], _effectValueInfos[effectName]);
    }

    public void AddEffectValueInfo(string effectName, EffectValueInfo effectValueInfo)
    {
        _effectValueInfos.TryAdd(effectName, effectValueInfo);
    }
}
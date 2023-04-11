using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "newEffectsNames", menuName = "ScriptableObject/Battle/EffectNames")]
public class AllEffectInfos : ScriptableObject
{
    private Dictionary<string, EffectReferenceInfo> _effectInfos = new Dictionary<string, EffectReferenceInfo>(128);
    private Dictionary<string, EffectValueInfo> _effectValueInfos = new Dictionary<string, EffectValueInfo>(128);
    [SerializeField] private EffectReferenceInfo[] _effectReferenceInfoArray;
    [SerializeField] private EffectValueInfo[] _effectValueInfoArray;

    private void OnEnable()
    {
        CopyToDictionary();
    }

    private void CopyToDictionary()
    {
        _effectInfos.Clear();
        _effectValueInfos.Clear();
        for (int index = 0; index < _effectReferenceInfoArray.Length; index++)
        {
            EffectReferenceInfo effectReferenceInfo = _effectReferenceInfoArray[index];
            _effectInfos.Add(effectReferenceInfo.EffectName, effectReferenceInfo);
        }

        for (int index = 0; index < _effectValueInfoArray.Length; index++)
        {
            EffectValueInfo effectValueInfo = _effectValueInfoArray[index];
            _effectValueInfos.Add(effectValueInfo.EffectName, effectValueInfo);
        }
    }

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
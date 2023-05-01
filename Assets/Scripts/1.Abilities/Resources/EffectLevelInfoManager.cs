using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectLevelInfoManager : MonoBehaviour
{
    public AbilityResourceInfo[] effectLevelResourceInfos;
    private Dictionary<string, EffectValueInfo> _effectValueInfosMap = new Dictionary<string, EffectValueInfo>();

    public void SetEffectLevelInfo(ref List<Effect> input)
    {
        Init();

        foreach (Effect data in input)
        {
            if (!_effectValueInfosMap.ContainsKey(data._effectName)) continue;
            data.ValueInfo = new EffectValueInfo();
            data.ValueInfo = _effectValueInfosMap[data._effectName];
        }
    }
    private void Init()
    {
        foreach (AbilityResourceInfo info in effectLevelResourceInfos)
        {
            List<string[]> data = info.GetAbilityDatas();
            List<EffectValueInfo> valueInfos = GetEffectLevelInfo(data);
            foreach (EffectValueInfo valueInfo in valueInfos)
            {
                if (_effectValueInfosMap.ContainsKey(valueInfo.EffectName)) continue;
                _effectValueInfosMap.Add(valueInfo.EffectName, valueInfo);
            }
        }
    }

    public List<EffectValueInfo> GetEffectLevelInfo(List<string[]> values)
    {
        List<EffectValueInfo> valueInfos = new List<EffectValueInfo>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectValueInfo valueInfo = new EffectValueInfo
            {
                EffectName = rowDatas[0],
                Level = 1, // rowDatas[1] is level
                MaxLevel = int.TryParse(rowDatas[2], out int maxLevel) ? maxLevel : 1,
                BaseValue = float.TryParse(rowDatas[3], out float baseValue) ? baseValue : 0,
                ValuePerLevel = float.TryParse(rowDatas[4], out float valuePerLevel) ? valuePerLevel : 0,
                CoolTime = float.TryParse(rowDatas[5], out float coolTime) ? coolTime : 0,
            };
            if (valueInfos.Contains(valueInfo)) continue;
            valueInfos.Add(valueInfo);
        }
        return valueInfos;
    }
}
﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BehaviourInfoManager : MonoBehaviour
{
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get; private set; }
    private Dictionary<string, BehaviourValueInfo> behaviourValueInfosMap = new Dictionary<string, BehaviourValueInfo>();

    public void SetBehaviourLevelInfo(ref List<BattleBehaviour> input)
    {
        Init();

        foreach (BattleBehaviour data in input)
        {
            if (!behaviourValueInfosMap.ContainsKey(data.BehaviourName)) continue;
            data.BehaviourValueInfo = new BehaviourValueInfo();
            data.BehaviourValueInfo = behaviourValueInfosMap[data.BehaviourName];
        }
    }
    private void Init()
    {
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            List<string[]> data = info.GetAbilityDatas();
            List<BehaviourValueInfo> valueInfos = GetEffectLevelInfo(data);
            foreach (BehaviourValueInfo valueInfo in valueInfos)
            {
                if (behaviourValueInfosMap.ContainsKey(valueInfo.BehaviourName)) continue;
                behaviourValueInfosMap.Add(valueInfo.BehaviourName, valueInfo);
            }
        }
    }

    public List<BehaviourValueInfo> GetEffectLevelInfo(List<string[]> values)
    {
        List<BehaviourValueInfo> valueInfos = new List<BehaviourValueInfo>(values.Count);
        List<float> baseValues = new List<float>();
        List<float> coolTimes = new List<float>();
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];

            string currentBehaviour = rowDatas[0];
            string nextBehaviour = index < values.Count - 1 ? values[index + 1][0] : currentBehaviour;

            if (!string.IsNullOrEmpty(nextBehaviour))
            {
                if (index != values.Count - 1 && string.IsNullOrEmpty(nextBehaviour)) continue;
                BehaviourValueInfo valueInfo = new BehaviourValueInfo
                {
                    BehaviourName = rowDatas[0],
                    Level = 1, // rowDatas[1] is level
                    MaxLevel = int.TryParse(rowDatas[2], out int maxLevel) ? maxLevel : 1,
                    BaseValues = baseValues.ToArray(),
                    ValuePerLevel = float.TryParse(rowDatas[4], out float valuePerLevel) ? valuePerLevel : 0,
                    CoolTimes = coolTimes.ToArray(),
                };
                valueInfos.Add(valueInfo);
            }
            else
            {
                float BaseValue = float.TryParse(rowDatas[3], out float baseValue) ? baseValue : 0;
                float CoolTime = float.TryParse(rowDatas[5], out float coolTime) ? coolTime : 0;
                baseValues.Add(BaseValue);
                coolTimes.Add(CoolTime);
            }
        }

        return valueInfos;
    }
}
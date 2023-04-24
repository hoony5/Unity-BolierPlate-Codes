
using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectMotivationStatInfoManager : MonoBehaviour
{
    public AllMotivationStatusInfos allMotivationStatusInfos;
    public AbilityResourceInfo[] abilityResourceInfos;

    public void LoadAllMotivationStatusInfoDatas()
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            MotivationInfo[] loadedData = LoadMotivationStatusInfoData(info.GetAbilityDatas()).ToArray();
            allMotivationStatusInfos.SetEffectInfomations(loadedData);
        }
    }
    private List<MotivationInfo> LoadMotivationStatusInfoData(List<string[]> values)
    {
        List<MotivationInfo> result = new List<MotivationInfo>(64);
        List<MotivationStatusInfo> statusInfos = new List<MotivationStatusInfo>(64);
        
        string currentEffectName = string.Empty;
        string nextEffectName = string.Empty;
        for (int i = 0; i < values.Count; i++)
        {
            string[] rowDatas = values[i];
            
            if(i == 0)
            {
                currentEffectName = rowDatas[0];
            }

            nextEffectName = i <= values.Count - 1 ? values[i + 1][0] : currentEffectName;
            
            MotivationStatusInfo statusItem = new MotivationStatusInfo()
            {
                HasReflectMyStatus = bool.TryParse(rowDatas[1], out bool isMyStatus) && isMyStatus,
                CurrentStatName = rowDatas[2],
                MaxStatName = rowDatas[3],
                ReflectValue = float.TryParse(rowDatas[4], out float value) ? value : 0,
                ValueUnitType = Enum.TryParse(rowDatas[5], out DataUnitType unitType) ? unitType : DataUnitType.None,
                MotivationComparerType = Enum.TryParse(rowDatas[6], out ComparerType comparerType) ? comparerType : ComparerType.None,
            };
            
            if (statusInfos.Contains(statusItem)) continue;
            statusInfos.Add(statusItem);
            
            if(string.IsNullOrEmpty(nextEffectName) || (currentEffectName == string.Empty && nextEffectName == string.Empty) ) continue;

            MotivationInfo motivationStatusInfo = new MotivationInfo(currentEffectName, statusInfos);
         
            if(!result.Exists(i => i.EffectName == motivationStatusInfo.EffectName))
                result.Add(motivationStatusInfo);
            
            statusInfos.Clear();
            currentEffectName = nextEffectName;
        }

        return result;
    }
}
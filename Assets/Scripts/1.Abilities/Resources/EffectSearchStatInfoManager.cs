using System.Collections.Generic;
using UnityEngine;

public class EffectSearchStatInfoManager : MonoBehaviour
{
    public AbilityResourceInfo[] abilityResourceInfos;

    public void LoadAllSearchStatusItemInfo()
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            
        }
    }
    private List<SearchStatusItem> LoadSearchStatusItemInfo(List<string[]> values)
    {
        List<SearchStatusItem> result = new List<SearchStatusItem>(5);
        List<StatusItemInfo> statusItemInfos = new List<StatusItemInfo>(values.Count);
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
            
            StatusItemInfo statusItem = new StatusItemInfo()
            {
                RawName = rowDatas[1],
                DisplayName = rowDatas[2],
                Value = float.TryParse(rowDatas[3], out float value) ? value : 0,
                Index = int.TryParse(rowDatas[5], out int index) ? index : 0
            };
            if (statusItemInfos.Contains(statusItem)) continue;
            statusItemInfos.Add(statusItem);
            
            if(string.IsNullOrEmpty(nextEffectName) || (currentEffectName == string.Empty && nextEffectName == string.Empty) ) continue;

            SearchStatusItem searchStatusItem = new SearchStatusItem(currentEffectName, statusItemInfos);
         
            if(!result.Exists(i => i.effectName == searchStatusItem.effectName))
                result.Add(searchStatusItem);
            
            statusItemInfos.Clear();
            currentEffectName = nextEffectName;
        }

        return result;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectSearchStatInfoManager : MonoBehaviour
{
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get; private set; }
    [field:SerializeField] public List<SearchStatusInfo> SearchStatuses { get; private set; }
    private Dictionary<string, SearchStatusInfo> SearchStatusItemsMap { get; set; }

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        SearchStatusItemsMap.Clear();
        SearchStatusItemsMap = SearchStatuses.ToDictionary(key => key.effectName, value => value);
    }

    public void LoadAllSearchStatusItemInfo()
    {
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            SearchStatuses.AddRange(LoadSearchStatusItemInfo(info.GetAbilityDatas()).ToArray());
        }
    }
    private List<SearchStatusInfo> LoadSearchStatusItemInfo(List<string[]> values)
    {
        List<SearchStatusInfo> result = new List<SearchStatusInfo>(5);
        List<SearchStatusItem> statusItemInfos = new List<SearchStatusItem>(values.Count);
        string currentEffectName = string.Empty;
        string nextEffectName = string.Empty;
        for (int i = 0; i < values.Count; i++)
        {
            string[] rowDatas = values[i];
            
            currentEffectName = string.IsNullOrEmpty(rowDatas[0]) ? currentEffectName : rowDatas[0];

            nextEffectName = i <= values.Count - 1 ? values[i + 1][0] : currentEffectName;
            
            StatusItemInfo statusItem = new StatusItemInfo()
            {
                RawName = rowDatas[1],
                DisplayName = rowDatas[2],
                Index = int.TryParse(rowDatas[7], out int index) ? index : 0
            };
            float Value = float.TryParse(rowDatas[3], out float value) ? value : 0;
            int Min = int.TryParse(rowDatas[4], out int min) ? min : 0;
            int Max = int.TryParse(rowDatas[5], out int max) ? max : 0;
            statusItem.SetValue(Value);
            statusItem.SetRange(Min, Max);
            
            bool exist = Enum.TryParse(rowDatas[6], out DataUnitType unitType);
            SearchStatusItem searchStatusItem = new SearchStatusItem(statusItem,exist ? DataUnitType.None : unitType);
            if (statusItemInfos.Contains(searchStatusItem)) continue;
            statusItemInfos.Add(searchStatusItem);
            
            if(string.IsNullOrEmpty(nextEffectName) || (currentEffectName == string.Empty && nextEffectName == string.Empty) ) continue;

            SearchStatusInfo searchStatusInfo = new SearchStatusInfo(currentEffectName, statusItemInfos);
         
            if(!result.Exists(i => i.effectName == searchStatusInfo.effectName))
                result.Add(searchStatusInfo);
            
            statusItemInfos.Clear();
            currentEffectName = nextEffectName;
        }

        return result;
    }
}
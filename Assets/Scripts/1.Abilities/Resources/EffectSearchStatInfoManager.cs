using System.Collections.Generic;
using UnityEngine;

public class EffectSearchStatInfoManager : MonoBehaviour
{
    public List<StatusItemInfo> LoadStatusItemInfo(List<string[]> values)
    {
        List<StatusItemInfo> statusItemInfos = new List<StatusItemInfo>(values.Count);
        foreach (string[] rowDatas in values)
        {
            StatusItemInfo statusItem = new StatusItemInfo()
            {
                RawName = rowDatas[2],
                DisplayName = rowDatas[3],
                Value = float.TryParse(rowDatas[4], out float value) ? value : 0,
                Index = int.TryParse(rowDatas[5], out int index) ? index : 0
            };
            if (statusItemInfos.Contains(statusItem)) continue;
            statusItemInfos.Add(statusItem);
        }

        return statusItemInfos;
    }
}
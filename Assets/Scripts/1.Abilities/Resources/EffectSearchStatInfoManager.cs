using System.Collections.Generic;
using UnityEngine;

public class EffectSearchStatInfoManager : MonoBehaviour
{
    [ToDo("모든 데이터를 설정함에 있어서, 이미 로드 되어있는 기존의 Effect 데이터에 적확하게 개입되어야한다. 인수로 Effect 리스트를 받는편이 좋을 것 같다.")]
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
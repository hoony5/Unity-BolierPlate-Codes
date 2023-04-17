using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class AbilityResourceInfo
{
    public string typeName;
    public string path;
    // Row Data - adapted from RowData.cs
    [FormerlySerializedAs("baseInfos")] public List<AbilityDataInfo> infos = new List<AbilityDataInfo>(32);

    public void SetAbilityDataInfo(string firstColumnValue , string[] columnHeaders, string[] columnValues)
    {
        if (infos.Exists(i => i.firstColumnValue == firstColumnValue)) return;
        
        AbilityDataInfo newData = new AbilityDataInfo()
        {
            firstColumnValue = firstColumnValue,
            columnHeaders = columnHeaders,
            columnValues = columnValues
        };
        
        infos.Add(newData);
    }

    public List<string[]> GetInfo()
    {
        List<string[]> result = new List<string[]>(32);
        
        foreach (AbilityDataInfo info in infos)
        {
            result.Add(info.columnValues);
        }

        return result;
    }
}
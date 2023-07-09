using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.ExcelReader;
// ReSharper disable All

[System.Serializable]
public class AbilityResourceInfo
{
    public string sheetName;
    public string path;
    // Row Data - adapted from RowData.cs
    [SerializeField] private List<RowData> infos = new List<RowData>(32);

    private void SetAbilityDataInfo(string firstColumnValue , string[] columnHeaders, string[] columnValues)
    {
        if (infos.Exists(i => i.FirstColumnValue == firstColumnValue)) return;
        
        RowData newData = new RowData()
        {
            FirstColumnValue = firstColumnValue,
            Headers = columnHeaders.ToList(),
            Values = columnValues.ToList()
        };
        
        infos.Add(newData);
    }

    public List<string[]> GetDataList()
    {
        List<string[]> result = new List<string[]>(32);
        
        foreach (RowData info in infos)
        {
            result.Add(info.Values.ToArray());
        }

        return result;
    }
}
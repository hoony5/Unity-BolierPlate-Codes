using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityResourceInfo
{
    public string sheetName;
    public string path;
    // Row Data - adapted from RowData.cs
    [SerializeField] private List<AbilityDataInfo> infos = new List<AbilityDataInfo>(32);

    public void LoadExcelDocument(ExcelCsvReader reader)
    {
        reader.LoadDocument(path, sheetName);
        foreach (RowData rowData in reader.RowDataDictValues)
        {
            SetAbilityDataInfo(rowData.FirstColumnValue, rowData.columnHeaders.ToArray(), rowData.columnValues.ToArray());
        }
    }
    private void SetAbilityDataInfo(string firstColumnValue , string[] columnHeaders, string[] columnValues)
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

    public List<string[]> GetAbilityDatas()
    {
        List<string[]> result = new List<string[]>(32);
        
        foreach (AbilityDataInfo info in infos)
        {
            result.Add(info.columnValues);
        }

        return result;
    }
}
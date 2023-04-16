using System.Collections.Generic;

[System.Serializable]
public class AbilityResourceInfo
{
    public string typeName;
    public string path;
    public string description;
    // Row Data - adapted from RowData.cs
    public List<AbilityDataInfo> abilityDataInfos = new List<AbilityDataInfo>(32);

    public void SetAbilityDataInfo(string firstColumnValue , string[] columnHeaders, string[] columnValues)
    {
        if (abilityDataInfos.Exists(i => i.firstColumnValue == firstColumnValue)) return;
        
        AbilityDataInfo newData = new AbilityDataInfo()
        {
            firstColumnValue = firstColumnValue,
            columnHeaders = columnHeaders,
            columnValues = columnValues
        };
        
        abilityDataInfos.Add(newData);
    }

    public List<string[]> GetAbilityValues()
    {
        List<string[]> result = new List<string[]>(32);
        
        foreach (AbilityDataInfo info in abilityDataInfos)
        {
            result.Add(info.columnValues);
        }

        return result;
    }
}
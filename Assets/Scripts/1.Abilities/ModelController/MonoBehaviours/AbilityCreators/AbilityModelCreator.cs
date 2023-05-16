using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityModelCreator : MonoBehaviour
{
    [field: SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field:SerializeField] public AbilityResourceInfo[] AllAbilityResourceInfos { get; private set; }
    protected string GrowableSheetName => "Growable";
    protected string EnhancableSheetName => "Enhancable";
    protected string CombinableSheetName => "Combinable";
    protected string StatusTypesSheetName => "StatusTypes";
    protected string StatusesBaseSheetName => "StatusesBase";
    
    protected List<StatusBaseAbility> LoadStatusTypesByModels(string model, List<string[]> values)
    {
        List<StatusBaseAbility> result = new List<StatusBaseAbility>(values.Count());
        int modelIndex = -1;
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];
            if (index == 0)
            {
                modelIndex = Array.FindIndex(rowDatas, i => i.Equals(model));
            }
            else
            {
                if (modelIndex != -1)
                {
                    result.Add(new StatusBaseAbility(rowDatas[modelIndex]));
                }   
            }
        }

        return result;
    }

    protected List<StatusItemInfo> LoadStatusBasicNames(List<string[]> values)
    {
        List<StatusItemInfo> result = new List<StatusItemInfo>(values.Count);
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            StatusItemInfo itemInfo = new StatusItemInfo
            (
                rawName: rowData[0],
                displayName: rowData[1],
                index: int.TryParse(rowData[2], out int Index) ? Index : 0
            );
            
            if(!result.Exists(i => i.RawName == itemInfo.RawName))
                result.Add(itemInfo);
        }
        return result;
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Utility.ExcelReader;

public static class RowDataToObject
{
    public static Dictionary<string, T> ConvertAll<T>(this ExcelSheetInfo sheetInfo, bool usingUnityJsonFormat = false)
    {
        Type getType = Type.GetType(sheetInfo.TypeName);
        
        if (getType is null)
        {
            Debug.LogError($"[{sheetInfo.TypeName}] is no exist type.");
            return null;
        }
        if (typeof(T) != getType)
        {
            Debug.LogError($"[{sheetInfo.TypeName}] is not equal with [T:{typeof(T)}].");
            return null;
        }

        Dictionary<string, T> result = new Dictionary<string, T>(sheetInfo.RowDataDict.Values.Count);
        foreach (RowData rowData in sheetInfo.RowDataDict.Values)
        {
            T instance = JsonConvert.DeserializeObject<T>(CreateJsonFormat_Internal(rowData, usingUnityJsonFormat));
            result.Add(rowData.FirstColumnValue, instance);
        }

        return result;
    }
    public static T Convert<T>(this ExcelSheetInfo sheetInfo, string index, bool usingUnityJsonFormat = false)
    {
        Type getType = Type.GetType(sheetInfo.TypeName);
        
        if (getType is null)
        {
            Debug.LogError($"[{sheetInfo.TypeName}] is no exist type.");
            return default;
        }
        if (typeof(T) != getType)
        {
            Debug.LogError($"[{sheetInfo.TypeName}] is not equal with [T:{typeof(T)}].");
            return default;
        }
        RowData rowData = sheetInfo.RowDataDict[index];
        T instance = JsonConvert.DeserializeObject<T>(CreateJsonFormat_Internal(rowData, usingUnityJsonFormat));

        return instance;
    }

    private static string CreateJsonFormat_Internal(RowData rowData, bool usingUnityJsonFormat = false)
    {
        return new string($"{{{CreateJsonFormatContent_Internal(rowData, usingUnityJsonFormat)}}}");
    }

    private static string CreateJsonFormatContent_Internal(RowData rowData, bool usingUnityJsonFormat = false)
    {
        if (rowData.Values.Count == 0) return string.Empty;
        if (rowData.Values.Count != rowData.Headers.Count) return string.Empty;
        string content = string.Empty;
        for (int i = 0; i < rowData.Headers.Count; i++)
        {
            // $"<{memberName}>k__BackingField" : $"{memberName}"
            string memberName = $"\"{rowData.Headers[i]}\"";
            string memberType = rowData.Types[i];
            string memberValue = memberType switch
            {
                "string" => $"\"{rowData.Values[i]}\"",
                _ => rowData.Values[i]
            };
            content += $@"{(usingUnityJsonFormat ? $"<{memberName}>k__BackingField" : $"{memberName}")}:{memberValue}";
            if (i != rowData.Headers.Count - 1) content += ',';
        }

        return content;
    }

    private static RowData ToRowData<T>(this T instance, string typeName)
    {
        if (!DataManager.Instance.DataSo.ContainsKey(typeName))
        {
            Debug.LogError($"There is no typeName : {typeName}");
            return null;
        }

        if (!DataManager.Instance.DataSo.TryGetValue(typeName, out ExcelSheetInfo result))
        {
            result = new ExcelSheetInfo();
        }

        // A : 0
        string[] rawValue = JsonConvert.SerializeObject(instance)
            .Replace("{", "")
            .Replace("}", "")
            .Replace("\"","")
            .Split(",", StringSplitOptions.RemoveEmptyEntries);
        
        RowData rowData = new RowData()
        {
            Headers = new List<string>(),
            Values = new List<string>(),
            FirstColumnValue = string.Empty
        };
        for(int i = 0 ; i < rawValue.Length; i ++)
        {
            string[] pair = rawValue[i].Split(":",StringSplitOptions.RemoveEmptyEntries);
            rowData.Headers.Add(pair[0]);
        
            rowData.Values.Add(pair[1].Contains('[') ? 
                    pair[1].Replace("[","").Replace("]","") :
                    pair[1]);
        }

        rowData.FirstColumnValue = rawValue[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[0];
        rowData.Types = result.RowDataDict[rowData.FirstColumnValue].Types;

        return rowData;
    }
}
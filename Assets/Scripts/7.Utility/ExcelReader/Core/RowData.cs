using System.Collections.Generic;
using UnityEngine;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public class RowData
    {
        [field:SerializeField] public string FirstColumnValue {get; set;} 
        [field:SerializeField] public List<string> ColumnHeaders {get; set;}
        [field:SerializeField] public List<string> ColumnValues {get; set;}
    }
}
using System.Collections.Generic;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public class RowData
    {
        public string FirstColumnValue {get; set;} 
        public List<string> ColumnHeaders {get; set;}
        public List<string> ColumnValues {get; set;}
    }
}
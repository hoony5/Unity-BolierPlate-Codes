using System.Collections.Generic;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public class ColumnData
    {
        public string Header {get; set;}
        public List<string> Values {get; set;}
    }
}
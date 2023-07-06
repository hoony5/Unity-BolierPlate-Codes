using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    [Serializable]
    public class ExcelSheetInfo
    {
        private const int FixedCapacity = 32;
        
        [field:SerializeField] public string TypeName { get; set; }
        
        [field: SerializeField, SerializedDictionary("Name", "Excel Column Data")]
        public SerializedDictionary<string, ColumnData> ColumnDataDict { get; private set; } = new SerializedDictionary<string, ColumnData>(FixedCapacity);

        [field: SerializeField, SerializedDictionary("Name", "Excel Row Data")]
        public SerializedDictionary<string, RowData> RowDataDict { get; private set; } = new SerializedDictionary<string, RowData>(FixedCapacity);

    }
}
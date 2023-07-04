using System.Collections.Concurrent;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    [CreateAssetMenu(fileName = "newExcelSOData", menuName = "ScriptableObject/ExcelDatabase", order = 0)]
    public class ExcelDataSO : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Database Category" , "Sheet Info")]
        private SerializedDictionary<string, ExcelSheetInfo> Database = new SerializedDictionary<string, ExcelSheetInfo>();

        [SerializeField]
        private ConcurrentDictionary<string, ExcelSheetInfo> DatabaseAsync =
            new ConcurrentDictionary<string, ExcelSheetInfo>();

        public void Init()
        {
            DatabaseAsync.Clear();
            foreach (KeyValuePair<string, ExcelSheetInfo> item in Database)
            {
                DatabaseAsync.TryAdd(item.Key, item.Value);
            }
        }
        private void OnEnable()
        {
            Init();
        }

        public bool TryGetValue(string key, out ExcelSheetInfo result)
        {
            return Database.TryGetValue(key, out result);
        }
        public bool TryGetValueAsync(string key, out ExcelSheetInfo result)
        {
            return DatabaseAsync.TryGetValue(key, out result);
        }
    }
}
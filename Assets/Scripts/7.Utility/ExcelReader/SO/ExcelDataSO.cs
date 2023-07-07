using System.Collections.Concurrent;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    [CreateAssetMenu(fileName = "newExcelSOData", menuName = "ScriptableObject/ExcelDatabase", order = 0)]
    public class ExcelDataSO : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Database TypeName" , "Sheet Infos")]
        private SerializedDictionary<string, ExcelSheetInfo> Database = new SerializedDictionary<string, ExcelSheetInfo>();

        [SerializeField]
        private ConcurrentDictionary<string, ExcelSheetInfo> DatabaseAsync =
            new ConcurrentDictionary<string, ExcelSheetInfo>();

#if UNITY_EDITOR
        public void Set(IDictionary<string, ExcelSheetInfo> database)
        {
            foreach (KeyValuePair<string, ExcelSheetInfo> item in database)
            {
                Database[item.Key] = item.Value;
            }
        }
        public void Set(IList<string> keys, IList<ExcelSheetInfo> values)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                Database[keys[i]] = values[i];
            }
        }
#endif
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

        public bool ContainsKey(string key) => Database.ContainsKey(key);
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
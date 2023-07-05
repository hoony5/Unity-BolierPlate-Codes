using System.Collections.Concurrent;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    [CreateAssetMenu(fileName = "newObejctData", menuName = "ScriptableObject/ObjectData", order = 0)]
    public class ObjectDataSO<T> : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Database TypeName" , "Object Infos")]
        private SerializedDictionary<string, SerializedDictionary<string,T>> database 
            = new SerializedDictionary<string, SerializedDictionary<string,T>>();

        [SerializeField]
        private ConcurrentDictionary<string, SerializedDictionary<string,T>> databaseAsync =
            new ConcurrentDictionary<string, SerializedDictionary<string,T>>();

        [SerializeField]
        private SerializedDictionary<string, ObjectDataSO<T>> referenceDictionary =
            new SerializedDictionary<string, ObjectDataSO<T>>();
        public void Init()
        {
            databaseAsync.Clear();
            foreach (KeyValuePair<string, SerializedDictionary<string,T>> item in database)
            {
                databaseAsync.TryAdd(item.Key, item.Value);
            }
        }
        private void OnEnable()
        {
            Init();
        }

        public bool ContainsKey(string key) => database.ContainsKey(key);
        public bool TryGetValue(string key, out SerializedDictionary<string,T> result)
        {
            return database.TryGetValue(key, out result);
        }
        public bool TryGetValueAsync(string key, out SerializedDictionary<string,T> result)
        {
            return databaseAsync.TryGetValue(key, out result);
        }
        public bool ContainsRefKey(string key) => referenceDictionary.ContainsKey(key);
        public bool TryGetRefValue(string key, out ObjectDataSO<T> result)
        {
            return referenceDictionary.TryGetValue(key, out result);
        }
    }
}
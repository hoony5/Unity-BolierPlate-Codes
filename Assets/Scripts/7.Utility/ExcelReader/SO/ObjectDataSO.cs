using System.Collections.Concurrent;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    public abstract class ObjectDataSO<T> : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Database TypeName" , "Object Infos")]
        protected SerializedDictionary<string, SerializedDictionary<string,T>> database 
            = new SerializedDictionary<string, SerializedDictionary<string,T>>();

        [SerializeField]
        protected ConcurrentDictionary<string, SerializedDictionary<string,T>> databaseAsync =
            new ConcurrentDictionary<string, SerializedDictionary<string,T>>();

        [SerializeField]
        protected SerializedDictionary<string, ObjectDataSO<T>> referenceDictionary =
            new SerializedDictionary<string, ObjectDataSO<T>>();
        protected virtual void Init()
        {
            databaseAsync.Clear();
            foreach (KeyValuePair<string, SerializedDictionary<string,T>> item in database)
            {
                databaseAsync.TryAdd(item.Key, item.Value);
            }
        }
        protected virtual void OnEnable()
        {
            Init();
        }

        public virtual bool ContainsKey(string key) => database.ContainsKey(key);
        public virtual bool TryGetValue(string key, out SerializedDictionary<string,T> result)
        {
            return database.TryGetValue(key, out result);
        }
        public virtual bool TryGetValueAsync(string key, out SerializedDictionary<string,T> result)
        {
            return databaseAsync.TryGetValue(key, out result);
        }
        public virtual bool ContainsRefKey(string key) => referenceDictionary.ContainsKey(key);
        public virtual bool TryGetRefValue(string key, out ObjectDataSO<T> result)
        {
            return referenceDictionary.TryGetValue(key, out result);
        }
    }
}
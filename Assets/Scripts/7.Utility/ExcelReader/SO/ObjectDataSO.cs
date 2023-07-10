using System.Collections.Concurrent;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Utility.ExcelReader
{
    [System.Serializable]
    public abstract class ObjectDataSO<T> : ScriptableObject
    {
        [SerializeField, SerializedDictionary("Database TypeName" , "Object Infos")]
        protected SerializedDictionary<string, SerializedDictionary<string,T>> database 
            = new SerializedDictionary<string, SerializedDictionary<string,T>>();

        [SerializeField]
        protected ConcurrentDictionary<string, SerializedDictionary<string,T>> databaseAsync =
            new ConcurrentDictionary<string, SerializedDictionary<string,T>>();
        
        protected SerializedDictionary<string, T> referenceDatabase = new SerializedDictionary<string, T>(); 
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

        protected virtual bool ContainsKey(string key) => database.ContainsKey(key);
        protected virtual bool TryGetValue(string key, out SerializedDictionary<string,T> result)
        {
            return database.TryGetValue(key, out result);
        }
        protected virtual bool TryGetValueAsync(string key, out SerializedDictionary<string,T> result)
        {
            return databaseAsync.TryGetValue(key, out result);
        }

        protected virtual bool ContainsRefKey(string key) => referenceDatabase.ContainsKey(key);
        protected virtual bool TryGetRefValue(string key, out T result)
        {
            return referenceDatabase.TryGetValue(key, out result);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SerializeDictionary<TKey,TValue> : IDictionary<TKey,TValue> , ISerializationCallbackReceiver
{
    [field:SerializeField] private List<TKey> TKeys { get; set; }
    [field:SerializeField] private List<TValue> TValues { get; set; }
    private Dictionary<TKey, TValue> _dictionary;
    public SerializeDictionary(IList<TKey> keys, IList<TValue> values)
    {
        TKeys = keys is null || keys.Count == 0 ? new List<TKey>() : new List<TKey>(keys);
        TValues = values is null || values.Count == 0 ? new List<TValue>() : new List<TValue>(values);
        _dictionary = new Dictionary<TKey, TValue>();
    }

    public SerializeDictionary() : this(null, null) { }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_dictionary).GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        if(_dictionary is null) _dictionary = new Dictionary<TKey, TValue>();
        _dictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _dictionary.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _dictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        ((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return _dictionary.Remove(item.Key);
    }

    public int Count { get => _dictionary.Count; }
    public bool IsReadOnly { get => false; }
    public void Add(TKey key, TValue value)
    {
        if(_dictionary is null) _dictionary = new Dictionary<TKey, TValue>();
        _dictionary.Add(key, value);
    }

    public bool ContainsKey(TKey key)
    {
        return _dictionary is not null && _dictionary.ContainsKey(key);
    }

    public bool Remove(TKey key)
    {
        return _dictionary is not null && _dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_dictionary is not null) return _dictionary.TryGetValue(key, out value);
        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }

    public ICollection<TKey> Keys { get; }
    public ICollection<TValue> Values { get; }
    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }
}

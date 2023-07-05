using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CustomEnum<T>
{
    [field: SerializeField] private string PreviousKey {get; set;} 
    [field: SerializeField] private string CurrentKey {get; set;}
    [SerializeField] private EnumItem<T>[] _itemsBuffer;
    private Dictionary<string, EnumItem<T>> _itemsDictionary;

    public event UnityAction<EnumItem<T>> OnEnterValue;
    public event UnityAction<EnumItem<T>> OnStayValue;
    public event UnityAction<EnumItem<T>> OnExitValue;

    private EnumItem<T> Empty;

    public CustomEnum() { }
    
    public void Init()
    {
        PreviousKey = string.Empty;
        CurrentKey = string.Empty;

        if (_itemsDictionary is null)
            _itemsDictionary = new Dictionary<string, EnumItem<T>>();
        else
        {
            _itemsDictionary.Clear();
            _itemsBuffer = _itemsBuffer.OrderBy(item => item.Order).ToArray();
        }

        foreach (EnumItem<T> item in _itemsBuffer)
        {
            _itemsDictionary.TryAdd(item.Key, item);
        }
    }

    public void Clear()
    {
        _itemsDictionary.Clear();
    }

    public void SetPrevious()
    {
        ChangeItem(PreviousKey);
    }

    public void Set(string key)
    {
        if(!_itemsDictionary.ContainsKey(key))
        {
            Debug.LogWarning($"Key: {key} does not exist in the dictionary.");
            return;
        }

        ChangeItem(key);
    }

    public void Set(T value)
    {
        string key = _itemsDictionary.FirstOrDefault(x => x.Value.Value.Equals(value)).Key;

        if(string.IsNullOrEmpty(key))
        {
            Debug.LogWarning($"Value: {value} does not exist in the dictionary.");
            return;
        }

        ChangeItem(key);
    }

    public void Set(int order) 
    {
        if (order >= _itemsBuffer.Length)
        {
            Debug.LogWarning("Order is out of range");
            return;
        }

        string key = _itemsBuffer[order].Key;

        ChangeItem(key);
    }

    public void SetFlag(string key, bool setFlag)
    {
        if(!_itemsDictionary.ContainsKey(key))
        {
            Debug.LogWarning($"Key: {key} does not exist in the dictionary.");
            return;
        }

        _itemsDictionary[key].HasFlag = setFlag;

        ChangeItem(key);
    }

    public void SetFlag(T value, bool setFlag)
    {
        string key = _itemsDictionary.FirstOrDefault(x => x.Value.Value.Equals(value)).Key;

        if(string.IsNullOrEmpty(key))
        {
            Debug.LogWarning($"Value: {value} does not exist in the dictionary.");
            return;
        }

        _itemsDictionary[key].HasFlag = setFlag;

        ChangeItem(key);
    }

    public void SetFlag(int order, bool setFlag)
    {
        if (order >= _itemsBuffer.Length)
        {
            Debug.LogWarning("Order is out of range");
            return;
        }

        string key = _itemsBuffer[order].Key;

        _itemsDictionary[key].HasFlag = setFlag;

        ChangeItem(key);
    }

    private void ChangeItem(string key)
    {
        EnumItem<T> previousItem;

        if (!string.IsNullOrEmpty(PreviousKey) && _itemsDictionary.TryGetValue(PreviousKey, out previousItem))
        {
            OnExitValue?.Invoke(previousItem);
        }

        (CurrentKey, PreviousKey) = (key, CurrentKey);

        EnumItem<T> currentItem = _itemsDictionary[CurrentKey];

        OnEnterValue?.Invoke(currentItem);
        OnStayValue?.Invoke(currentItem);
    }
}

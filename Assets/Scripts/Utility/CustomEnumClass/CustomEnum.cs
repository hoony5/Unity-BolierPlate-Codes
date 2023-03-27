using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CustomEnum<T>
{
    [SerializeField] private List<EnumItem<T>> _items = new List<EnumItem<T>>(32);
    
    public void Clear()
    {
        _items.Clear();
    }

    public void Add(string key, T value, int order, bool hasFlag)
    {
        _items.Add(new EnumItem<T>(key, value, order, hasFlag));
    }

    public void Add(EnumItem<T> item)
    {
        _items.Add(item);
    }

    public EnumItem<T> GetItemByKey(string key)
    {
        return _items.FirstOrDefault(item => item.Key == key);
    }

    public EnumItem<T> GetItemByValue(T value)
    {
        return _items.FirstOrDefault(item => Equals(item.Value, value));
    }

    public EnumItem<T> GetItemByOrder(int order)
    {
        return _items.FirstOrDefault(item => item.Order == order);
    }
    public void SetFlag(string key, bool setFlag)
    {
        foreach (EnumItem<T> item in _items)
        {
            if (item.Key.Equals(key, StringComparison.Ordinal))
                item.HasFlag = setFlag;
        }
    }

    public void SetFlag(T value, bool setFlag)
    {
        foreach (EnumItem<T> item in _items)
        {
            if (Equals(item.Value,value))
                item.HasFlag = setFlag;
        }
    }

    public void SetFlag(int order, bool setFlag)
    {
        if (order >= _items.Count)
        {
            Debug.Assert(order >= _items.Count, "order is out of range");
            return;
        }

        _items[order].HasFlag = setFlag;
    }

    public IEnumerable<EnumItem<T>> GetItemsByOrder()
    {
        return _items.OrderBy(item => item.Order);
    }
}
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

    public void Add(string key, T value, int order)
    {
        _items.Add(new EnumItem<T>(key, value, order));
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

    public IEnumerable<EnumItem<T>> GetItemsByOrder()
    {
        return _items.OrderBy(item => item.Order);
    }
}
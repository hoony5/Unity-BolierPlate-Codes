using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnumItem<T>
{
    public EnumItem(EnumItem<T> item)
    {
        Key = item.Key;
        Value = item.Value;
        Order = item.Order;       
        HasFlag = item.HasFlag;       
    }
    public EnumItem(string key, T value, int order, bool hasFlag)
    {
        Key = key;
        Value = value;
        Order = order;       
        HasFlag = hasFlag;       
    }
    public EnumItem() : this("None", default, -1, false) { }
    [field: SerializeField] public string Key { get; set; }
    [field: SerializeField] public T Value { get; set; }
    [field: SerializeField] public int Order { get; set; }        
    [field: SerializeField] public bool HasFlag { get; set; }
}
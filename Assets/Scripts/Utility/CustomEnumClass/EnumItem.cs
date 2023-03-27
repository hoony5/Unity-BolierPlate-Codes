using UnityEngine;

[System.Serializable]
public class EnumItem<T>
{
    public EnumItem(string key, T value, int order, bool hasFlag)
    {
        Key = key;
        Value = value;
        Order = order;       
        HasFlag = hasFlag;       
    }

    [field: SerializeField] public string Key { get; set; }
    [field: SerializeField] public T Value { get; set; }
    [field: SerializeField] public int Order { get; set; }        
    [field: SerializeField] public bool HasFlag { get; set; }        
}
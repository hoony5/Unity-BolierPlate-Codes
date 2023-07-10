using System;
using UnityEngine;

[Serializable]
public class Item : ModuleController
{
    [field: SerializeField] public string Name { get;  set; }
    [field: SerializeField] public Transform Transform { get; set; }
    [field: SerializeField] public int Count { get;  set; }
    [field: SerializeField] public ItemAttributes ItemAttributes { get;  set; }

    public Item(string name)
    {
        Name = name;
        Count = 0;
    }
    public void SetAttributes(ItemAttributes attributes)
    {
        ItemAttributes = attributes;
    }
    protected bool TryAddCount(int count)
    {
        Count += count;
        if (Count > ItemAttributes.MaxCount)
        {
            Count = ItemAttributes.MaxCount;
            return false;
        }

        if (Count >= 0) return true;
        Count = 0;
        return false;
    }
}
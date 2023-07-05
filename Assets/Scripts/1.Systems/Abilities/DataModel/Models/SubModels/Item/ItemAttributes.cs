using System;
using UnityEngine;

[Serializable]
public class ItemAttributes
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public ElementalType ElementalType { get; private set; }
    [field:SerializeField] public string Category { get; private set; }
    [field:SerializeField] public Grade Grade { get; private set; }
    [field: SerializeField] public bool IsQuestItem { get; private set; }
    [field: SerializeField] public int MaxCount { get; private set; }
    [field:SerializeField] public string Description { get; private set; }
    
    public ItemAttributes(string name, ElementalType elementalType, string category, Grade grade, int maxCount,bool isQuestItem, string description)
    {
        Name = name;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        IsQuestItem = isQuestItem;
        MaxCount = maxCount;
        Description = description;
    }
}
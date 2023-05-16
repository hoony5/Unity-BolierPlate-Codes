﻿using UnityEngine;

[System.Serializable]
public class EquipmentAttributes
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string Type { get; private set; }
    [field:SerializeField] public ElementalType ElementalType { get; private set; }
    [field:SerializeField] public string Category { get; private set; }
    [field:SerializeField] public Grade Grade { get; private set; }
    [field: SerializeField] public bool IsQuestItem { get; private set; }
    [field: SerializeField] public int MaxCount { get; private set; }
    [field:SerializeField] public string[] AttackSkills { get; private set; }
    [field:SerializeField] public string[] DefenseSkills { get; private set; }
    [field:SerializeField] public string[] UtilitySkills { get; private set; }
    [field:SerializeField] public string[] PassiveSkills { get; private set; }
    [field:SerializeField] public string[] Places { get; private set; }
    [field:SerializeField] public string Description { get; private set; }
    
    public EquipmentAttributes(string name ,string type , ElementalType elementalType, string category, Grade grade,
        int maxCount, bool isQuestItem,
        string[] attackSkills, 
        string[] defenseSkills,
        string[] utilitySkills,
        string[] passiveSkills, 
        string[] places,
        string description)
    {
        Name = name;
        Type = type;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        MaxCount = maxCount;
        IsQuestItem = isQuestItem;
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
        PassiveSkills = passiveSkills;
        Places = places;
        Description = description;
    }
}
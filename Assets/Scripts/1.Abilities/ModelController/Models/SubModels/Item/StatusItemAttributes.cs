using UnityEngine;

[System.Serializable]
public class StatusItemAttributes
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public ElementalType ElementalType { get; private set; }
    [field:SerializeField] public string Category { get; private set; }
    [field:SerializeField] public Grade Grade { get; private set; }
    [field:SerializeField] public bool IsQuestItem{ get; private set; }
    [field: SerializeField] public int MaxCount { get; private set; }
    [field:SerializeField] public string[] PassiveSkills { get; private set; }
    [field:SerializeField] public string[] Places { get; private set; }
    [field:SerializeField] public string Description { get; private set; }

    public StatusItemAttributes(string name, ElementalType elementalType, string category, Grade grade,
        int maxCount, bool isQuestItem,
        string[] passiveSkills, string[] places,
        string description)
    {
        Name = name;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        MaxCount = maxCount;
        IsQuestItem = isQuestItem;
        PassiveSkills = passiveSkills;
        Places = places;
        Description = description;
    }
}
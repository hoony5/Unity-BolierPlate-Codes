using UnityEngine;

[System.Serializable]
public class ItemAttributes
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public ElementalType ElementalType { get; private set; }
    [field:SerializeField] public string Category { get; private set; }
    [field:SerializeField] public Grade Grade { get; private set; }
    [field:SerializeField] public string[] PassiveSkills { get; private set; }
    [field:SerializeField] public string[] Places { get; private set; }
    [field:SerializeField] public string Description { get; private set; }

    public ItemAttributes(string name, ElementalType elementalType, string category, Grade grade,
        string[] passiveSkills, string[] places, string description)
    {
        Name = name;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        PassiveSkills = passiveSkills;
        Places = places;
        Description = description;
    }
}
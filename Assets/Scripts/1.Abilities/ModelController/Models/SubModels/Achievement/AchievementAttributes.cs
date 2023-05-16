using UnityEngine;

[System.Serializable]
public class AchievementAttributes
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string Type { get; private set; }
    [field:SerializeField] public string Category { get; private set; }
    [field:SerializeField] public Grade Grade { get; private set; }
    [field:SerializeField] public string[] PassiveSkills { get; private set; }
    [field:SerializeField] public string[] MotivationSkills { get; private set; }
    [field:SerializeField] public string[] Conditions { get; private set; }
    [field:SerializeField] public string Description { get; private set; }

    public AchievementAttributes(string name,string type, string category, Grade grade,
        string[] passiveSkills, string[] motivationSkills, string[] conditions,
        string description)
    {
        Name = name;
        Type = type;
        Category = category;
        Grade = grade;
        PassiveSkills = passiveSkills;
        MotivationSkills = motivationSkills;
        Conditions = conditions;
        Description = description;
    }
}
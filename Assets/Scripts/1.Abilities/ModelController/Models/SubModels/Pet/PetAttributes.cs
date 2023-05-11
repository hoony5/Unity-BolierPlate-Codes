using UnityEngine;

[System.Serializable]
public class PetAttributes
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public ElementalType ElementalType { get; private set; }
    [field:SerializeField] public string Race { get; private set; }
    [field:SerializeField] public Grade Grade { get; private set; }
    [field:SerializeField] public string[] AttackSkills { get; private set; }
    [field:SerializeField] public string[] DefenseSkills { get; private set; }
    [field:SerializeField] public string[] UtilitySkills { get; private set; }
    [field:SerializeField] public string[] PassiveSkills { get; private set; }
    [field:SerializeField] public string[] MotivationSkills { get; private set; }
    [field:SerializeField] public string Description { get; private set; }

    public PetAttributes(string name, ElementalType elementalType, string race,Grade grade, string[] attackSkills,
        string[] defenseSkills, string[] utilitySkills, string[] passiveSkills, string[] motivationSkills,
        string description)
    {
        Name = name;
        ElementalType = elementalType;
        Race = race;
        Grade = grade;
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
        PassiveSkills = passiveSkills;
        MotivationSkills = motivationSkills;
        Description = description;
    }

}
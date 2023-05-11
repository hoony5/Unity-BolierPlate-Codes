using UnityEngine;

[System.Serializable]
public class EquipmentAttributes : ItemAttributes
{
    [field:SerializeField] public string[] AttackSkills { get; private set; }
    [field:SerializeField] public string[] DefenseSkills { get; private set; }
    [field:SerializeField] public string[] UtilitySkills { get; private set; }
    
    public EquipmentAttributes(string name, ElementalType elementalType, string category, Grade grade,
        string[] passiveSkills, string[] places, string description, string[] attackSkills, string[] defenseSkills,
        string[] utilitySkills) : base(name, elementalType, category, grade, passiveSkills, places, description)
    {
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
    }
}
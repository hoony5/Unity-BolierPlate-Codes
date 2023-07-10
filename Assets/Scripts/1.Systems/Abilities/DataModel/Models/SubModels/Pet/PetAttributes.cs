using UnityEngine;

[System.Serializable]
public class PetAttributes
{
    [field:SerializeField] public string Name { get; set; }
    [field:SerializeField] public string Type { get; set; }
    [field:SerializeField] public ElementalType ElementalType { get; set; }
    [field:SerializeField] public string Race { get; set; }
    [field:SerializeField] public Grade Grade { get; set; }
    [field:SerializeField] public string[] AttackSkills { get; set; }
    [field:SerializeField] public string[] DefenseSkills { get; set; }
    [field:SerializeField] public string[] UtilitySkills { get; set; }
    [field:SerializeField] public string[] PassiveSkills { get; set; }
    [field:SerializeField] public string[] MotivationSkills { get; set; }
    [field:SerializeField] public string Description { get; set; }
}
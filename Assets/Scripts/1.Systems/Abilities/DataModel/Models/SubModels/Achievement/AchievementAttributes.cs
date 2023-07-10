using UnityEngine;

[System.Serializable]
public class AchievementAttributes
{
    [field:SerializeField] public string Name { get; set; }
    [field:SerializeField] public string Type { get; set; }
    [field:SerializeField] public string Category { get; set; }
    [field:SerializeField] public Grade Grade { get; set; }
    [field:SerializeField] public string[] PassiveSkills { get; set; }
    [field:SerializeField] public string[] MotivationSkills { get; set; }
    [field:SerializeField] public string[] Conditions { get; set; }
    [field:SerializeField] public string Description { get; set; }
}
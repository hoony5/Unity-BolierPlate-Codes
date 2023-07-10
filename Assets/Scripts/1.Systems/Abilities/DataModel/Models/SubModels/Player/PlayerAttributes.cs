using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    [field:SerializeField] public bool HasBeenConvertedTodayExerciseData { get; set; }
    [field:SerializeField] public bool HasBeenTakenTodayRewards { get; set; }
    [field:SerializeField] public string Job { get; set; }
    [field:SerializeField] public int MaxEnergy { get; set; }
    [field:SerializeField] public int Money { get; set; }
    [field:SerializeField] public int GoldToken { get; set; }
    
    [field:SerializeField] public string[] Achievements { get; set; }
    [field:SerializeField] public string[] AttackSkills { get; set; }
    [field:SerializeField] public string[] DefenseSkills { get; set; }
    [field:SerializeField] public string[] UtilitySkills { get; set; }
    [field:SerializeField] public string[] PassiveSkills { get; set; }
    [field:SerializeField] public string[] MotivationSkills { get; set; }
    [field:SerializeField] public string[] Pets { get; set; }
}
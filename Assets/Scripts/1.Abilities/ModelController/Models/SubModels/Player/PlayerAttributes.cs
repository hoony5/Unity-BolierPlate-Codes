using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    [field:SerializeField] public bool HasBeenTakenTodayRewards { get; private set; }
    [field:SerializeField] public string Job { get; private set; }
    [field:SerializeField] public int Money { get; private set; }
    [field:SerializeField] public int GoldToken { get; private set; }
    [field:SerializeField] public string[] Achievements { get; private set; }
    [field:SerializeField] public string[] AttackSkills  { get; private set; }
    [field:SerializeField] public string[] DefenseSkills  { get; private set; }
    [field:SerializeField] public string[] UtilitySkills  { get; private set; }
    [field:SerializeField] public string[] PassiveSkills  { get; private set; }
    [field:SerializeField] public string[] MotivationSkills  { get; private set; }
    
    public PlayerAttributes(bool hasBeenTakenTodayRewards, string job, int money , int goldToken,
        string[] achievements, string[] attackSkills, string[] defenseSkills, string[] utilitySkills, string[] passiveSkills, string[] motivationSkills)
    {
        HasBeenTakenTodayRewards = hasBeenTakenTodayRewards;
        Job = job;
        Money = money;
        GoldToken = goldToken;
        Achievements = achievements;
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
        PassiveSkills = passiveSkills;
        MotivationSkills = motivationSkills;
    }
}
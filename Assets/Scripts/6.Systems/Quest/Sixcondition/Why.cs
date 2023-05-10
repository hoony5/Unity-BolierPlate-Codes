using UnityEngine;

[System.Serializable]
public class Why
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string[] RewardsEachCount { get; private set; }
    [field:SerializeField] public string[] Rewards { get; private set; }
    [field:SerializeField] public string[] PunishmentEachCount { get; private set; }
    [field:SerializeField] public string[] Punishments { get; private set; }
    
    public Why(string name, string[] rewardsEachCount, string[] rewards, string[] punishmentEachCount, string[] punishments)
    {
        Name = name;
        RewardsEachCount = rewardsEachCount;
        Rewards = rewards;
        PunishmentEachCount = punishmentEachCount;
        Punishments = punishments;
    }
}

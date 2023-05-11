using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AchievementCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateAchievementAbility CreateAchievementAbility { get; private set; }
    [field:SerializeField] public CreateAchievementTraits CreateAchievementTraits { get; private set; }
    private Dictionary<string, Achievement> achievementsDataDictionary;

    public Achievement GetAchievementData(string achievementName)
    {
        return achievementsDataDictionary.TryGetValue(achievementName, out Achievement achievement) ? achievement : null;
    }

    public void ClearAchievementData()
    {
        achievementsDataDictionary.Clear();
    }
    
    public void Init()
    {
        List<Achievement> achievements = CreateAchievementAbility.SetAchievements();
        CreateAchievementTraits.SetAchievementAttributes(ref achievements);
        achievementsDataDictionary = achievements.ToDictionary(key => key.Name, value => value);
    }
}
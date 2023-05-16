using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AchievementCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateAchievementAbility CreateAchievementAbility { get; private set; }
    [field:SerializeField] public CreateAchievementTraits CreateAchievementTraits { get; private set; }
    private Dictionary<string, Achievement> achievementsDataDictionary = new Dictionary<string, Achievement>();

    private Dictionary<string, GrowableAchievement> growableAchievementsDictionary =
        new Dictionary<string, GrowableAchievement>();

    private Dictionary<string, EnhancableAchievement> enhancableAchievementsDictionary =
        new Dictionary<string, EnhancableAchievement>();

    private Dictionary<string, CombinableAchievement> combinableAchievementsDictionary =
        new Dictionary<string, CombinableAchievement>();

    public Achievement GetAchievement(string achievementName)
    {
        return achievementsDataDictionary.TryGetValue(achievementName, out Achievement achievement) ? achievement : null;
    }
    public GrowableAchievement GetGrowableAchievement(string achievementName)
    {
        return growableAchievementsDictionary.TryGetValue(achievementName, out GrowableAchievement achievement) ? achievement : null;
    }
    public EnhancableAchievement GetEnhancableAchievement(string achievementName)
    {
        return enhancableAchievementsDictionary.TryGetValue(achievementName, out EnhancableAchievement achievement) ? achievement : null;
    }
    public CombinableAchievement GetCombinableAchievement(string achievementName)
    {
        return combinableAchievementsDictionary.TryGetValue(achievementName, out CombinableAchievement achievement) ? achievement : null;
    }

    public void ClearAchievementData()
    {
        achievementsDataDictionary.Clear();
        growableAchievementsDictionary.Clear();
        enhancableAchievementsDictionary.Clear();
        combinableAchievementsDictionary.Clear();
    }

    public void Init()
    {
        InitAchievements();
        InitGrowableAchievements();
        InitEnhancableAchievements();
        InitCombinableAchievements();
    }

    private void InitAchievements()
    {
        List<Achievement> achievements = CreateAchievementAbility.GetAchievements();
        if (achievements.Count == 0) return;
        CreateAchievementTraits.SetAchievementAttributes(ref achievements);
        achievementsDataDictionary = achievements.ToDictionary(key => key.Name, value => value);
    }
    private void InitGrowableAchievements()
    {
        List<GrowableAchievement> achievements = CreateAchievementAbility.GetGrowableAchievements();
        if (achievements.Count == 0) return;
        CreateAchievementTraits.SetGrowInfo(ref achievements);
        growableAchievementsDictionary = achievements.ToDictionary(key => key.Name, value => value);
    }

    private void InitEnhancableAchievements()
    {
        List<EnhancableAchievement> achievements = CreateAchievementAbility.GetEnhancableAchievements();
        if (achievements.Count == 0) return;
        CreateAchievementTraits.SetEnhanceInfo(ref achievements);
        enhancableAchievementsDictionary = achievements.ToDictionary(key => key.Name, value => value);
    }
    private void InitCombinableAchievements()
    {
        List<CombinableAchievement> achievements = CreateAchievementAbility.GetCombinableAchievements();
        if (achievements.Count == 0) return;
        combinableAchievementsDictionary = achievements.ToDictionary(key => key.Name, value => value);
    }
}
using System.Collections.Generic;

public class CreateAchievementAbility : AbilityModelCreator
{
    private List<Achievement> achievements;
    protected string AchievementItemName => "Achievement";
    protected string CombinableSheetName => "Combinable";
    protected string AchievementSheetName => "AchievementStatuses";
    protected string GrowableAchievementSheetName => "GrowableAchievementStatuses";
    protected string CombinableAchievementSheetName => "CombinableAchievementStatuses";
    protected string EnhancableAchievementSheetName => "EnhancableAchievementStatuses";
    public List<GrowableAchievement> GetGrowableAchievements()
    {
        achievements ??= GetAchievements();
        List<GrowableAchievement> result = new List<GrowableAchievement>();
       
        for (var index = 0; index < achievements.Count; index++)
        {
            if(achievements[index].Attributes.Type != GrowableSheetName) continue;
            GrowableAchievement achievement = new GrowableAchievement(achievements[index]);
            result.Add(achievement);
        }
        SetAbilitiesValues(ref result);
        return result;
    }
    public List<EnhancableAchievement> GetEnhancableAchievements()
    {
        achievements ??= GetAchievements();
        List<EnhancableAchievement> result = new List<EnhancableAchievement>();
       
        for (var index = 0; index < achievements.Count; index++)
        {
            if(achievements[index].Attributes.Type != EnhancableSheetName) continue;
            EnhancableAchievement achievement = new EnhancableAchievement(achievements[index]);
            result.Add(achievement);
        }
        SetAbilitiesValues(ref result);
        return result;
    }

    public List<CombinableAchievement> GetCombinableAchievements()
    {
        achievements ??= GetAchievements();
        List<CombinableAchievement> result = new List<CombinableAchievement>();
       
        for (var index = 0; index < achievements.Count; index++)
        {
            if(achievements[index].Attributes.Type != CombinableSheetName) continue;
            CombinableAchievement achievement = new CombinableAchievement(achievements[index]);
            result.Add(achievement);
        }
        SetAbilitiesValues(ref result);
        return result;
    }
    public List<Achievement> GetAchievements()
    {
        achievements = new List<Achievement>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName == StatusTypesSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                abilityInfo = new AbilityInfo(LoadStatusTypesByModels(AchievementItemName, info.GetDataList()));
            }
            else if (info.sheetName == StatusesBaseSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                for (var index = 0; index < achievements.Count; index++)
                {
                    achievements[index] = new Achievement();
                    achievements[index].StatusAbility.SetAbility(abilityInfo);
                    achievements[index].StatusAbility.AbilityInfo
                        .SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                }
            }
        }

        SetAbilitiesValues(ref achievements);
        return achievements;
    }
    private void SetAbilitiesValues(ref List<Achievement> achievements)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != AchievementSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref achievements, info.sheetName, info.GetDataList());
        }
    } 
    private void SetAbilitiesValues(ref List<GrowableAchievement> achievements)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != GrowableAchievementSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref achievements, info.sheetName, info.GetDataList());
        }
    } 
    private void SetAbilitiesValues(ref List<CombinableAchievement> achievements)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != CombinableAchievementSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref achievements, info.sheetName, info.GetDataList());
        }
    } 
    private void SetAbilitiesValues(ref List<EnhancableAchievement> achievements)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EnhancableAchievementSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref achievements, info.sheetName, info.GetDataList());
        }
    }

    private void LoadAllOriginalStatuses(ref List<Achievement> achievements , string originalStatusType ,List<string[]> values)
    {
        foreach (Achievement achievement in achievements)
        {
            StatusBaseAbility status = achievement.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
            for (var index = 3; index < values.Count; index++)
            {
                string[] rowData = values[index];
                for (var i = 1; i < rowData.Length; i++)
                {
                    status.SetBaseValue(values[0][i], float.TryParse(rowData[i], out float Value) ? Value : 0);
                }
            }
        }
    }
    private void LoadAllOriginalStatuses(ref List<CombinableAchievement> achievements , string originalStatusType ,List<string[]> values)
    {
        foreach (CombinableAchievement achievement in achievements)
        {
            StatusBaseAbility status = achievement.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
            for (var index = 3; index < values.Count; index++)
            {
                string[] rowData = values[index];
                for (var i = 1; i < rowData.Length; i++)
                {
                    status.SetBaseValue(values[0][i], float.TryParse(rowData[i], out float Value) ? Value : 0);
                }
            }
        }
    }
    private void LoadAllOriginalStatuses(ref List<GrowableAchievement> achievements , string originalStatusType ,List<string[]> values)
    {
        foreach (GrowableAchievement achievement in achievements)
        {
            StatusBaseAbility status = achievement.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
            for (var index = 3; index < values.Count; index++)
            {
                string[] rowData = values[index];
                for (var i = 1; i < rowData.Length; i++)
                {
                    status.SetBaseValue(values[0][i], float.TryParse(rowData[i], out float Value) ? Value : 0);
                }
            }
        }
    }
    private void LoadAllOriginalStatuses(ref List<EnhancableAchievement> achievements , string originalStatusType ,List<string[]> values)
    {
        foreach (EnhancableAchievement achievement in achievements)
        {
            StatusBaseAbility status = achievement.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
            for (var index = 3; index < values.Count; index++)
            {
                string[] rowData = values[index];
                for (var i = 1; i < rowData.Length; i++)
                {
                    status.SetBaseValue(values[0][i], float.TryParse(rowData[i], out float Value) ? Value : 0);
                }
            }
        }
    }
}

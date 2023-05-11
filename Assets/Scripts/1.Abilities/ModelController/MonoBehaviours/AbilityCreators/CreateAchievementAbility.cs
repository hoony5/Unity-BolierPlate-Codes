using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CreateAchievementAbility : AbilityModelCreator
{
    public List<Achievement> SetAchievements()
    {
        List<Achievement> achievements = new List<Achievement>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusTypes":
                    info.LoadExcelDocument(CsvReader);
                    abilityInfo = new AbilityInfo(LoadStatusTypesByModels("Achievement", info.GetDataList()));
                    break;
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    for (var index = 0; index < achievements.Count; index++)
                    {
                        achievements[index] = new Achievement();
                        achievements[index].StatusAbility.SetAbility(abilityInfo);
                        achievements[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                    }
                    break;
                default:
                    continue;
            }
        }

        SetAbilitiesValues(ref achievements);
        return achievements;
    }
    private void SetAbilitiesValues(ref List<Achievement> achievements)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "AchievementStatuses":
                    info.LoadExcelDocument(CsvReader);
                    LoadAllOriginalStatuses(ref achievements, info.sheetName, info.GetDataList());
                    break;
                default:
                    continue;
            }
        }
    }

    [ToDo("Divide Datas each levels or contents")]
    private void LoadAllOriginalStatuses(ref List<Achievement> achievements , string originalStatusType ,List<string[]> values)
    {
        foreach (Achievement achievement in achievements)
        {
            StatusBaseAbility status = achievement.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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

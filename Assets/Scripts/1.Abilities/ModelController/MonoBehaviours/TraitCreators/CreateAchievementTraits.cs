using System;
using System.Collections.Generic;

[System.Serializable]
public class CreateAchievementTraits : TraitsCreator
{
    private List<AchievementAttributes> attributesList = new List<AchievementAttributes>();
    private string AchievementAttributesSheetName => "AchievementAttributes";
    private string EnhanceInfoSheetName => "EnhanceInfo";
    private string GrowInfoSheetName => "GrowInfo";

    public void SetAchievementAttributes(ref List<Achievement> achievements)
    {
        attributesList.Clear();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != AchievementAttributesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            attributesList = LoadAttributes(info.GetDataList());
        }

        for (int index = 0; index < achievements.Count; index++)
        {
            for (var i = 0; i < attributesList.Count; i++)
            {
                if (achievements[index].Name == attributesList[i].Name)
                    achievements[index].SetAttributes(attributesList[i]);
            }
        }
    } 
    public void SetEnhanceInfo(ref List<EnhancableAchievement> achievements)
    {
        List<(string Name, string MaxLevel, string MaxExp)> infos 
            = new List<(string Name, string MaxLevel, string MaxExp)>();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EnhanceInfoSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            infos = LoadInfo(info.GetDataList());
        }

        for (int index = 0; index < infos.Count; index++)
        {
            for (var i = 0; i < achievements.Count; i++)
            {
                if (achievements[i].Name == infos[index].Name)
                    achievements[i].SetBaseInfo
                    (int.TryParse(infos[index].MaxLevel, out int maxLevel) ? maxLevel : 1,
                        int.TryParse(infos[index].MaxExp, out int maxExp) ? maxExp : 1);
            }
        }
    }
    public void SetGrowInfo(ref List<GrowableAchievement> achievements)
    {
        List<(string Name, string MaxLevel, string MaxExp)> infos 
            = new List<(string Name, string MaxLevel, string MaxExp)>();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != GrowInfoSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            infos = LoadInfo(info.GetDataList());
        }

        for (int index = 0; index < infos.Count; index++)
        {
            for (var i = 0; i < achievements.Count; i++)
            {
                if (achievements[i].Name == infos[index].Name)
                    achievements[i].SetBaseInfo
                    (int.TryParse(infos[index].MaxLevel, out int maxLevel) ? maxLevel : 1,
                        int.TryParse(infos[index].MaxExp, out int maxExp) ? maxExp : 1);
            }
        }
    }
    private List<AchievementAttributes> LoadAttributes(List<string[]> values)
    {
        attributesList.Clear();
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            AchievementAttributes attributes = new AchievementAttributes
            (
                name:rowData[0],
                type:rowData[7],
                category:rowData[1],
                grade:Enum.TryParse(rowData[2], out Grade grade) ? grade : Grade.Common,
                passiveSkills:rowData[3].Split(','),
                motivationSkills:rowData[4].Split(','),
                conditions:rowData[5].Split(','),
                description:rowData[6]
            );
            if(!attributesList.Exists(i => i.Name == attributes.Name))
                attributesList.Add(attributes);
        }

        return attributesList;
    }
    private List<(string Name , string MaxLevel, string MaxExp)> LoadInfo(List<string[]> values)
    {
        List<(string name, string maxLevel, string maxExp)> result =
            new List<(string name, string maxLevel, string maxExp)>();
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            (string Name, string MaxLevel, string MaxExp) info = (rowData[0], rowData[1], rowData[2]);
            
            result.Add(info);
        }

        return result;
    }
}

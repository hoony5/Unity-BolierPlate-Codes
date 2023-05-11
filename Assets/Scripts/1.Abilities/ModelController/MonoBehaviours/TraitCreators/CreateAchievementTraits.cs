using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreateAchievementTraits : TraitsCreator
{
    public void SetAchievementAttributes(ref List<Achievement> achievements)
    {
        List<AchievementAttributes> attributesList = new List<AchievementAttributes>();
        
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "AchievementAttributes":
                    info.LoadExcelDocument(CsvReader);
                    attributesList = LoadAttributes(info.GetDataList());
                    break;
                default:
                    continue;
            }
        }

        for (int index = 0; index < achievements.Count; index++)
        {
            for(var i = 0 ; i < attributesList.Count; i++)
            {
                if(achievements[index].Name == attributesList[i].Name)
                    achievements[index].SetAttributes(attributesList[i]);
            }
        }
    }
    
    private List<AchievementAttributes> LoadAttributes(List<string[]> values)
    {
        List<AchievementAttributes> result = new List<AchievementAttributes>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            AchievementAttributes attributes = new AchievementAttributes
            (
                name:rowData[0],
                category:rowData[1],
                grade:Enum.TryParse(rowData[2], out Grade grade) ? grade : Grade.Common,
                passiveSkills:rowData[3].Split(','),
                motivationSkills:rowData[4].Split(','),
                conditions:rowData[5].Split(','),
                description:rowData[6]
            );
            if(!result.Exists(i => i.Name == attributes.Name))
                result.Add(attributes);
        }

        return result;
    }
}

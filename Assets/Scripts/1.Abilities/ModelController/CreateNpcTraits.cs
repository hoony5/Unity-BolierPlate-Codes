using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreateNpcTraits
{
    [field: SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field: SerializeField] public AbilityResourceInfo[] AllAbilityResourceInfos { get; private set; }

    public void SetNpcAttributes(ref List<NPC> npcs)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "Attribute":
                    info.LoadExcelDocument(CsvReader);
                    break;
                case "Loot":
                    info.LoadExcelDocument(CsvReader);
                    break;
                default:
                    continue;
            }
        }
    }
    
    private List<CharacterAttributes> LoadAttributes(List<string[]> values)
    {
        List<CharacterAttributes> result = new List<CharacterAttributes>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            CharacterAttributes attributes = new CharacterAttributes
            (
                name:rowData[0],
                elementalType:Enum.TryParse(rowData[1], out ElementalType elementalType) ? elementalType : ElementalType.Normal,
                race: rowData[2],
                grade:Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                isBoss:bool.TryParse(rowData[4], out bool isBoss) && isBoss,
                isElite:bool.TryParse(rowData[5], out bool isElite) && isElite,
                attackSkills:rowData[6].Split(','),
                defenseSkills:rowData[7].Split(','),
                utilitySkills:rowData[8].Split(','),
                passiveSkills:rowData[9].Split(','),
                places:rowData[10].Split(','),
                description:rowData[11]
            );
            if(!result.Exists(i => i.Name == attributes.Name))
                result.Add(attributes);
        }

        return result;
    }
    private List<CharacterLootInfo> LoadLootInfos(List<string[]> values)
    {
        List<CharacterLootInfo> result = new List<CharacterLootInfo>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            CharacterLootInfo lootInfo = new CharacterLootInfo
            (
                name:rowData[0],
                lootExp:int.TryParse(rowData[1], out int lootExp) ? lootExp : 0,
                lootMoney:int.TryParse(rowData[2], out int lootMoney) ? lootMoney : 0,
                lootCommonItems:rowData[3].Split(','),
                lootCommon:float.TryParse(rowData[4], out float lootCommon) ? lootCommon : 0,
                lootUncommonItems:rowData[5].Split(','),
                lootUncommon: float.TryParse(rowData[6], out float lootUncommon) ? lootUncommon : 0,
                lootRareItems:rowData[7].Split(','),
                lootRare:float.TryParse(rowData[8], out float lootRare) ? lootRare : 0,
                lootUniqueItems:rowData[9].Split(','),
                lootUnique:float.TryParse(rowData[10], out float lootUnique) ? lootUnique : 0,
                lootLegendaryItems:rowData[11].Split(','),
                lootLegendary:float.TryParse(rowData[12], out float lootLegendary) ? lootLegendary : 0,
                lootMythItems:rowData[13].Split(','),
                lootMyth:float.TryParse(rowData[14], out float lootMyth) ? lootMyth : 0
            );
            if(!result.Exists(i => i.Name == lootInfo.Name))
                result.Add(lootInfo);
        }

        return result;
    }
}

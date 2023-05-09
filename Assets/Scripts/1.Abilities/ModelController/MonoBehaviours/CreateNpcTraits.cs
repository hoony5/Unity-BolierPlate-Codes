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
        List<NPCAttributes> attributesList = new List<NPCAttributes>();
        List<NPCLootInfo> lootInfosList = new List<NPCLootInfo>();
        
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "Attribute":
                    info.LoadExcelDocument(CsvReader);
                    attributesList = LoadAttributes(info.GetDataList());
                    break;
                case "Loot":
                    info.LoadExcelDocument(CsvReader);
                    lootInfosList = LoadLootInfos(info.GetDataList());
                    break;
                default:
                    continue;
            }
        }

        for (int index = 0; index < npcs.Count; index++)
        {
            for(var i = 0 ; i < attributesList.Count; i++)
            {
                if(npcs[index].Name == attributesList[i].Name)
                    npcs[index].SetAttributes(attributesList[i]);
            }

            for (var k = 0; k < lootInfosList.Count; k++)
            {
                if(npcs[index].Name == lootInfosList[k].Name)
                    npcs[index].SetLootInfo(lootInfosList[k]);
            }
        }
    }
    
    private List<NPCAttributes> LoadAttributes(List<string[]> values)
    {
        List<NPCAttributes> result = new List<NPCAttributes>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            NPCAttributes attributes = new NPCAttributes
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
                motivationSkills:rowData[10].Split(','),
                places:rowData[11].Split(','),
                description:rowData[12]
            );
            if(!result.Exists(i => i.Name == attributes.Name))
                result.Add(attributes);
        }

        return result;
    }
    private List<NPCLootInfo> LoadLootInfos(List<string[]> values)
    {
        List<NPCLootInfo> result = new List<NPCLootInfo>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            NPCLootInfo lootInfo = new NPCLootInfo
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

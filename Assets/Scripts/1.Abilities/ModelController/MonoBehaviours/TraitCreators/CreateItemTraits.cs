using System;
using System.Collections.Generic;

[System.Serializable]
public class CreateItemTraits : TraitsCreator
{
    public void SetItemAttributes(ref List<Item> item)
    {
        List<ItemAttributes> attributesList = new List<ItemAttributes>();
        
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "ItemAttributes":
                    info.LoadExcelDocument(CsvReader);
                    attributesList = LoadItemAttributes(info.GetDataList());
                    break;
                default:
                    continue;
            }
        }

        for (int index = 0; index < item.Count; index++)
        {
            for(var i = 0 ; i < attributesList.Count; i++)
            {
                if(item[index].Name == attributesList[i].Name)
                    item[index].SetAttributes(attributesList[i]);
            }
        }
    }
    public void SetEquipmentAttributes(ref List<Equipment> equipments)
    {
        List<EquipmentAttributes> attributesList = new List<EquipmentAttributes>();
        
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "EquipmentAttributes":
                    info.LoadExcelDocument(CsvReader);
                    attributesList = LoadEquipmentAttributes(info.GetDataList());
                    break;
                default:
                    continue;
            }
        }

        for (int index = 0; index < equipments.Count; index++)
        {
            for(var i = 0 ; i < attributesList.Count; i++)
            {
                if(equipments[index].Name == attributesList[i].Name)
                    equipments[index].SetAttributes(attributesList[i]);
            }
        }
    }
    
    private List<ItemAttributes> LoadItemAttributes(List<string[]> values)
    {
        List<ItemAttributes> result = new List<ItemAttributes>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            ItemAttributes attributes = new ItemAttributes
            (
                name:rowData[0],
                elementalType:Enum.TryParse(rowData[1], out ElementalType elementalType) ? elementalType : ElementalType.Normal,
                category: rowData[2],
                grade:Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                passiveSkills:rowData[4].Split(','),
                places:rowData[5].Split(','),
                description:rowData[6]
            );
            if(!result.Exists(i => i.Name == attributes.Name))
                result.Add(attributes);
        }

        return result;
    }
    
    private List<EquipmentAttributes> LoadEquipmentAttributes(List<string[]> values)
    {
        List<EquipmentAttributes> result = new List<EquipmentAttributes>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            EquipmentAttributes attributes = new EquipmentAttributes
            (
                name:rowData[0],
                elementalType:Enum.TryParse(rowData[1], out ElementalType elementalType) ? elementalType : ElementalType.Normal,
                category: rowData[2],
                grade:Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                attackSkills:rowData[4].Split(','),
                defenseSkills:rowData[5].Split(','),
                utilitySkills:rowData[6].Split(','),
                passiveSkills:rowData[7].Split(','),
                places:rowData[8].Split(','),
                description:rowData[9]
            );
            if(!result.Exists(i => i.Name == attributes.Name))
                result.Add(attributes);
        }

        return result;
    }
}
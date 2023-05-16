using System;
using System.Collections.Generic;

[System.Serializable]
public class CreateItemTraits : TraitsCreator
{
    private List<ItemAttributes> itemAttributesList = new List<ItemAttributes>();
    private List<StatusItemAttributes> statusItemAttributesList = new List<StatusItemAttributes>();
    private List<EquipmentAttributes> equipmentAttributesList = new List<EquipmentAttributes>();

    private string ItemAttributesSheetName => "ItemAttributes";
    private string StatusItemAttributesSheetName => "StatusItemAttributes";
    private string EquipmentAttributesSheetName => "EquipmentAttributes";
    private string EnhanceInfoSheetName => "EnhanceInfo";
    private string GrowInfoSheetName => "GrowInfo";
    public List<Item> LoadItems()
    {
        List<Item> items = new List<Item>();
        statusItemAttributesList.Clear();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != ItemAttributesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            itemAttributesList = LoadItemAttributes(info.GetDataList());
        }

        for (int index = 0; index < items.Count; index++)
        {
            items[index] = new Item(itemAttributesList[index].Name);
            items[index].SetAttributes(itemAttributesList[index]);
        }

        return items;
    }

    public void SetStatusItemAttributes(ref List<StatusItem> item)
    {
        statusItemAttributesList.Clear();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != StatusItemAttributesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            statusItemAttributesList = LoadStatusItemAttributes(info.GetDataList());
        }

        for (int index = 0; index < item.Count; index++)
        {
            for (var i = 0; i < statusItemAttributesList.Count; i++)
            {
                if (item[index].Name == statusItemAttributesList[i].Name)
                    item[index].SetAttributes(statusItemAttributesList[i]);
            }
        }
    }

    public void SetEquipmentAttributes(ref List<Equipment> equipments)
    {
        equipmentAttributesList.Clear();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EquipmentAttributesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            equipmentAttributesList = LoadEquipmentAttributes(info.GetDataList());
        }

        for (int index = 0; index < equipments.Count; index++)
        {
            for (var i = 0; i < equipmentAttributesList.Count; i++)
            {
                if (equipments[index].Name == equipmentAttributesList[i].Name)
                    equipments[index].SetAttributes(equipmentAttributesList[i]);
            }
        }
    }

    public void SetGrowInfo(ref List<GrowableEquipment> equipments)
    {
        List<(string Name, string MaxLevel, string MaxExp)> infos
            = new List<(string Name, string MaxLevel, string MaxExp)>();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != GrowInfoSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            equipmentAttributesList = LoadEquipmentAttributes(info.GetDataList());
        }

        for (int index = 0; index < infos.Count; index++)
        {
            for (var i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Name == infos[index].Name)
                    equipments[i].SetBaseInfo
                    (int.TryParse(infos[index].MaxLevel, out int maxLevel) ? maxLevel : 1,
                        int.TryParse(infos[index].MaxExp, out int maxExp) ? maxExp : 1);
            }
        }
    }

    public void SetEnhanceInfo(ref List<EnhancableEquipment> equipments)
    {
        List<(string Name, string MaxLevel, string MaxExp)> infos
            = new List<(string Name, string MaxLevel, string MaxExp)>();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EnhanceInfoSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            equipmentAttributesList = LoadEquipmentAttributes(info.GetDataList());
        }

        for (int index = 0; index < infos.Count; index++)
        {
            for (var i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Name == infos[index].Name)
                    equipments[i].SetBaseInfo
                    (int.TryParse(infos[index].MaxLevel, out int maxLevel) ? maxLevel : 1,
                        int.TryParse(infos[index].MaxExp, out int maxExp) ? maxExp : 1);
            }
        }
    }

    private List<ItemAttributes> LoadItemAttributes(List<string[]> values)
    {
        itemAttributesList.Clear();

        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            ItemAttributes attributes = new ItemAttributes
            (
                name: rowData[0],
                elementalType: Enum.TryParse(rowData[1], out ElementalType elementalType)
                    ? elementalType
                    : ElementalType.Normal,
                category: rowData[2],
                grade: Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                maxCount: int.TryParse(rowData[4], out int maxCount) ? maxCount : 1,
                isQuestItem: bool.TryParse(rowData[5], out bool isQuestItem) && isQuestItem,
                description: rowData[6]
            );
            if (!itemAttributesList.Exists(i => i.Name == attributes.Name))
                itemAttributesList.Add(attributes);
        }

        return itemAttributesList;
    }

    private List<StatusItemAttributes> LoadStatusItemAttributes(List<string[]> values)
    {
        statusItemAttributesList.Clear();

        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            StatusItemAttributes attributes = new StatusItemAttributes
            (
                name: rowData[0],
                elementalType: Enum.TryParse(rowData[1], out ElementalType elementalType)
                    ? elementalType
                    : ElementalType.Normal,
                category: rowData[2],
                grade: Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                maxCount: int.TryParse(rowData[4], out int maxCount) ? maxCount : 1,
                isQuestItem: bool.TryParse(rowData[5], out bool isQuestItem) && isQuestItem,
                passiveSkills: rowData[6].Split(','),
                places: rowData[7].Split(','),
                description: rowData[8]
            );
            if (!statusItemAttributesList.Exists(i => i.Name == attributes.Name))
                statusItemAttributesList.Add(attributes);
        }

        return statusItemAttributesList;
    }

    private List<EquipmentAttributes> LoadEquipmentAttributes(List<string[]> values)
    {
        equipmentAttributesList.Clear();

        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            EquipmentAttributes attributes = new EquipmentAttributes
            (
                name: rowData[0],
                type: rowData[12],
                elementalType: Enum.TryParse(rowData[1], out ElementalType elementalType)
                    ? elementalType
                    : ElementalType.Normal,
                category: rowData[2],
                grade: Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                maxCount: int.TryParse(rowData[4], out int maxCount) ? maxCount : 1,
                isQuestItem: bool.TryParse(rowData[5], out bool isQuestItem) && isQuestItem,
                attackSkills: rowData[6].Split(','),
                defenseSkills: rowData[7].Split(','),
                utilitySkills: rowData[8].Split(','),
                passiveSkills: rowData[9].Split(','),
                places: rowData[10].Split(','),
                description: rowData[11]
            );
            if (!equipmentAttributesList.Exists(i => i.Name == attributes.Name))
                equipmentAttributesList.Add(attributes);
        }

        return equipmentAttributesList;
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
using System.Collections.Generic;

public class CreateItemAbility : AbilityModelCreator
{
    public List<Item> SetItems()
    {
        List<Item> items = new List<Item>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusTypes":
                    info.LoadExcelDocument(CsvReader);
                    abilityInfo = new AbilityInfo(LoadStatusTypesByModels("Item", info.GetDataList()));
                    break;
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    for (var index = 0; index < items.Count; index++)
                    {
                        items[index] = new Item();
                        items[index].StatusAbility.SetAbility(abilityInfo);
                        items[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                    }
                    break;
                default:
                    continue;
            }
        }

        SetAbilitiesValues(ref items);
        return items;
    }
    private void SetAbilitiesValues(ref List<Item> items)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "ItemStatuses":
                    info.LoadExcelDocument(CsvReader);
                    LoadAllOriginalStatuses(ref items, info.sheetName, info.GetDataList());
                    break;
                default:
                    continue;
            }
        }
    }

    [ToDo("Divide Datas each levels or contents")]
    private void LoadAllOriginalStatuses(ref List<Item> items , string originalStatusType ,List<string[]> values)
    {
        foreach (Item item in items)
        {
            StatusBaseAbility status = item.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
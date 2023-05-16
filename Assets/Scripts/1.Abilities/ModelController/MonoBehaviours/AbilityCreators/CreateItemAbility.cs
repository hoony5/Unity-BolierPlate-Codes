using System.Collections.Generic;

public class CreateItemAbility : AbilityModelCreator
{
    private string StatusItemName => "StatusItem";
    private string StatusItemStatusSheetName => "StatusItemStatuses";
    
    public List<StatusItem> SetStatusItems()
    {
        List<StatusItem> items = new List<StatusItem>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName == StatusTypesSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                abilityInfo = new AbilityInfo(LoadStatusTypesByModels(StatusItemName, info.GetDataList()));
            }
            else if (info.sheetName == StatusesBaseSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                for (var index = 0; index < items.Count; index++)
                {
                    items[index] = new StatusItem();
                    items[index].StatusAbility.SetAbility(abilityInfo);
                    items[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                }
            }
            else
            {
                continue;
            }
        }

        SetAbilitiesValues(ref items);
        return items;
    }
    private void SetAbilitiesValues(ref List<StatusItem> items)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != StatusItemStatusSheetName ) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref items, info.sheetName, info.GetDataList());
        }
    }
    private void LoadAllOriginalStatuses(ref List<StatusItem> items , string originalStatusType ,List<string[]> values)
    {
        foreach (StatusItem item in items)
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
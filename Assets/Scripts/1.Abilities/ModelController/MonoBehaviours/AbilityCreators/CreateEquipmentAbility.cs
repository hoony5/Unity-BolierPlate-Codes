using System.Collections.Generic;

public class CreateEquipmentAbility : AbilityModelCreator
{
    public List<Equipment> SetEquipments()
    {
        List<Equipment> equipments = new List<Equipment>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusTypes":
                    info.LoadExcelDocument(CsvReader);
                    abilityInfo = new AbilityInfo(LoadStatusTypesByModels("Equipment", info.GetDataList()));
                    break;
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    for (var index = 0; index < equipments.Count; index++)
                    {
                        equipments[index] = new Equipment();
                        equipments[index].StatusAbility.SetAbility(abilityInfo);
                        equipments[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                    }
                    break;
                default:
                    continue;
            }
        }

        SetAbilitiesValues(ref equipments);
        return equipments;
    }
    private void SetAbilitiesValues(ref List<Equipment> equipments)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "BattleStatuses":
                    info.LoadExcelDocument(CsvReader);
                    LoadAllUnitsOriginalStatuses(ref equipments, info.sheetName, info.GetDataList());
                    break;
                default:
                    continue;
            }
        }
    }

    [ToDo("Divide Datas each levels or contents")]
    private void LoadAllUnitsOriginalStatuses(ref List<Equipment> equipments , string originalStatusType ,List<string[]> values)
    {
        foreach (Equipment equipment in equipments)
        {
            StatusBaseAbility status = equipment.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
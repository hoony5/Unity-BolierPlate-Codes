using System.Collections.Generic;

public class CreateEquipmentAbility : AbilityModelCreator
{
    private List<Equipment> equipments;
    private string EquipmentItemName => "Equipment";
    private string EquipmentStatusesSheetName => "EquipmentStatuses";
    private string GrowableEquipmentStatusesSheetName => "GrowableEquipmentStatuses";
    private string EnhancableEquipmentStatusesSheetName => "EnhancableEquipmentStatuses";
    private string CombinableEquipmentStatusesSheetName => "CombinableEquipmentStatuses";
    public List<Equipment> GetEquipments()
    {
        equipments = new List<Equipment>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName == StatusTypesSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                abilityInfo = new AbilityInfo(LoadStatusTypesByModels(EquipmentItemName, info.GetDataList()));
            }
            else if (info.sheetName == StatusesBaseSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                for (var index = 0; index < equipments.Count; index++)
                {
                    equipments[index] = new Equipment();
                    equipments[index].StatusAbility.SetAbility(abilityInfo);
                    equipments[index].StatusAbility.AbilityInfo
                        .SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                }
            }
        }

        SetAbilitiesValues(ref equipments);
        return equipments;
    }
    public List<GrowableEquipment> GetGrowableEquipments()
    {
        equipments ??= GetEquipments();
        List<GrowableEquipment> result = new List<GrowableEquipment>();
        for (var index = 0; index < equipments.Count; index++)
        {
            if (equipments[index].Attributes.Type != GrowableSheetName) continue;
            GrowableEquipment equipment = new GrowableEquipment(equipments[index]);
            result.Add(equipment);
        }

        SetAbilitiesValues(ref result);
        return result;
    }
    public List<EnhancableEquipment> GetEnhancableEquipments()
    {
        equipments ??= GetEquipments();
        List<EnhancableEquipment> result = new List<EnhancableEquipment>();
        for (var index = 0; index < equipments.Count; index++)
        {
            if (equipments[index].Attributes.Type != EnhancableSheetName) continue;
            EnhancableEquipment equipment = new EnhancableEquipment(equipments[index]);
            result.Add(equipment);
        }

        SetAbilitiesValues(ref result);
        return result;
    }
    public List<CombinableEquipment> GetCombinableEquipments()
    {
        equipments ??= GetEquipments();
        List<CombinableEquipment> result = new List<CombinableEquipment>();
        for (var index = 0; index < equipments.Count; index++)
        {
            if (equipments[index].Attributes.Type != CombinableSheetName) continue;
            CombinableEquipment equipment = new CombinableEquipment(equipments[index]);
            result.Add(equipment);
        }

        SetAbilitiesValues(ref result);
        return result;
    }
    private void SetAbilitiesValues(ref List<Equipment> equipments)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EquipmentStatusesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref equipments, info.sheetName, info.GetDataList());
        }
    }
    private void SetAbilitiesValues(ref List<GrowableEquipment> equipments)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != GrowableEquipmentStatusesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref equipments, info.sheetName, info.GetDataList());
        }
    }
    private void SetAbilitiesValues(ref List<EnhancableEquipment> equipments)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EnhancableEquipmentStatusesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref equipments, info.sheetName, info.GetDataList());
        }
    }
    private void SetAbilitiesValues(ref List<CombinableEquipment> equipments)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != CombinableEquipmentStatusesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref equipments, info.sheetName, info.GetDataList());
        }
    }

    private void LoadAllOriginalStatuses(ref List<Equipment> equipments , string originalStatusType ,List<string[]> values)
    {
        foreach (Equipment equipment in equipments)
        {
            StatusBaseAbility status = equipment.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
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
    private void LoadAllOriginalStatuses(ref List<GrowableEquipment> equipments , string originalStatusType ,List<string[]> values)
    {
        foreach (GrowableEquipment equipment in equipments)
        {
            StatusBaseAbility status = equipment.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
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
    private void LoadAllOriginalStatuses(ref List<EnhancableEquipment> equipments , string originalStatusType ,List<string[]> values)
    {
        foreach (EnhancableEquipment equipment in equipments)
        {
            StatusBaseAbility status = equipment.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
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
    private void LoadAllOriginalStatuses(ref List<CombinableEquipment> equipments , string originalStatusType ,List<string[]> values)
    {
        foreach (CombinableEquipment equipment in equipments)
        {
            StatusBaseAbility status = equipment.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
            status.Clear();
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
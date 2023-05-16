using System.Collections.Generic;

public class CreatePetAbility : AbilityModelCreator
{
    private List<Pet> pets;
    private string PetItemName => "Pet";
    private string PetStatusSheetName => "PetStatuses";
    private string GrowablePetStatusSheetName => "GrowablePetStatuses";
    private string CombinablePetStatusSheetName => "CombinablePetStatuses";
    private string EnhancablePetStatusSheetName => "EnhancablePetStatuses";
    
    public List<Pet> GetPets()
    {
        pets = new List<Pet>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName == StatusTypesSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                abilityInfo = new AbilityInfo(LoadStatusTypesByModels(PetItemName, info.GetDataList()));
            }
            else if (info.sheetName == StatusesBaseSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                for (var index = 0; index < pets.Count; index++)
                {
                    pets[index] = new Pet();
                    pets[index].StatusAbility.SetAbility(abilityInfo);
                    pets[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                }
            }
        }

        SetAbilitiesValues(ref pets);
        return pets;
    }
    public List<GrowablePet> GetGrowablePet()
    {
        pets ??= GetPets();
        List<GrowablePet> result = new List<GrowablePet>();
        for (var index = 0; index < pets.Count; index++)
        {
            if (pets[index].Attributes.Type != GrowableSheetName) continue;
            GrowablePet pet = new GrowablePet(pets[index]);
            result.Add(pet);
        }

        SetAbilitiesValues(ref result);
        return result;
    }
    public List<EnhancablePet> GetEnhancablePet()
    {
        pets ??= GetPets();
        List<EnhancablePet> result = new List<EnhancablePet>();
        for (var index = 0; index < pets.Count; index++)
        {
            if (pets[index].Attributes.Type != EnhancableSheetName) continue;
            EnhancablePet pet = new EnhancablePet(pets[index]);
            result.Add(pet);
        }

        SetAbilitiesValues(ref result);
        return result;
    }
    public List<CombinablePet> GetCombinablePet()
    {
        pets ??= GetPets();
        List<CombinablePet> result = new List<CombinablePet>();
        for (var index = 0; index < pets.Count; index++)
        {
            if (pets[index].Attributes.Type != CombinableSheetName) continue;
            CombinablePet pet = new CombinablePet(pets[index]);
            result.Add(pet);
        }

        SetAbilitiesValues(ref result);
        return result;
    }
    private void SetAbilitiesValues(ref List<Pet> pets)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != PetStatusSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref pets, info.sheetName, info.GetDataList());
        }
    }
    private void SetAbilitiesValues(ref List<GrowablePet> pets)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != GrowablePetStatusSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref pets, info.sheetName, info.GetDataList());
        }
    }
    private void SetAbilitiesValues(ref List<CombinablePet> pets)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != CombinablePetStatusSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref pets, info.sheetName, info.GetDataList());
        }
    }
    private void SetAbilitiesValues(ref List<EnhancablePet> pets)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EnhancablePetStatusSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllOriginalStatuses(ref pets, info.sheetName, info.GetDataList());
        }
    }

    private void LoadAllOriginalStatuses(ref List<Pet> pets , string originalStatusType ,List<string[]> values)
    {
        foreach (Pet pet in pets)
        {
            StatusBaseAbility status = pet.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
    private void LoadAllOriginalStatuses(ref List<GrowablePet> pets , string originalStatusType ,List<string[]> values)
    {
        foreach (GrowablePet pet in pets)
        {
            StatusBaseAbility status = pet.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
    private void LoadAllOriginalStatuses(ref List<EnhancablePet> pets , string originalStatusType ,List<string[]> values)
    {
        foreach (EnhancablePet pet in pets)
        {
            StatusBaseAbility status = pet.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
    private void LoadAllOriginalStatuses(ref List<CombinablePet> pets , string originalStatusType ,List<string[]> values)
    {
        foreach (CombinablePet pet in pets)
        {
            StatusBaseAbility status = pet.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
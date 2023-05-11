using System.Collections.Generic;

public class CreatePetAbility : AbilityModelCreator
{
    public List<Pet> SetPets()
    {
        List<Pet> pets = new List<Pet>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusTypes":
                    info.LoadExcelDocument(CsvReader);
                    abilityInfo = new AbilityInfo(LoadStatusTypesByModels("Pet", info.GetDataList()));
                    break;
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    for (var index = 0; index < pets.Count; index++)
                    {
                        pets[index] = new Pet();
                        pets[index].StatusAbility.SetAbility(abilityInfo);
                        pets[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                    }
                    break;
                default:
                    continue;
            }
        }

        SetAbilitiesValues(ref pets);
        return pets;
    }
    private void SetAbilitiesValues(ref List<Pet> pets)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "PetStatuses":
                    info.LoadExcelDocument(CsvReader);
                    LoadAllOriginalStatuses(ref pets, info.sheetName, info.GetDataList());
                    break;
                default:
                    continue;
            }
        }
    }

    private void LoadAllOriginalStatuses(ref List<Pet> pets , string originalStatusType ,List<string[]> values)
    {
        foreach (Pet pet in pets)
        {
            StatusBaseAbility status = pet.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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
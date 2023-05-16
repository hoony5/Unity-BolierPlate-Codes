using System;
using System.Collections.Generic;

[System.Serializable]
public class CreatePetTraits : TraitsCreator
{
    private List<PetAttributes> attributesList = new List<PetAttributes>();
    private string PetAttributesSheetName => "PetAttributes";
    private string EnhanceInfoSheetName => "EnhanceInfo";
    private string GrowInfoSheetName => "GrowInfo";
    public void SetPetAttributes(ref List<Pet> pets)
    {
        attributesList.Clear();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != PetAttributesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            attributesList = LoadAttributes(info.GetDataList());
        }

        for (int index = 0; index < pets.Count; index++)
        {
            for(var i = 0 ; i < attributesList.Count; i++)
            {
                if(pets[index].Name == attributesList[i].Name)
                    pets[index].SetAttributes(attributesList[i]);
            }
        }
    }
    
    public void SetEnhanceInfo(ref List<EnhancablePet> pets)
    {
        List<(string Name, string MaxLevel, string MaxExp)> infos 
            = new List<(string Name, string MaxLevel, string MaxExp)>();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != EnhanceInfoSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            infos = LoadInfo(info.GetDataList());
        }

        for (int index = 0; index < infos.Count; index++)
        {
            for (var i = 0; i < pets.Count; i++)
            {
                if (pets[i].Name == infos[index].Name)
                    pets[i].SetBaseInfo
                    (int.TryParse(infos[index].MaxLevel, out int maxLevel) ? maxLevel : 1,
                        int.TryParse(infos[index].MaxExp, out int maxExp) ? maxExp : 1);
            }
        }
    }
    public void SetGrowInfo(ref List<GrowablePet> pets)
    {
        List<(string Name, string MaxLevel, string MaxExp)> infos 
            = new List<(string Name, string MaxLevel, string MaxExp)>();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != GrowInfoSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            infos = LoadInfo(info.GetDataList());
        }

        for (int index = 0; index < infos.Count; index++)
        {
            for (var i = 0; i < pets.Count; i++)
            {
                if (pets[i].Name == infos[index].Name)
                    pets[i].SetBaseInfo
                    (int.TryParse(infos[index].MaxLevel, out int maxLevel) ? maxLevel : 1,
                        int.TryParse(infos[index].MaxExp, out int maxExp) ? maxExp : 1);
            }
        }
    }
    private List<PetAttributes> LoadAttributes(List<string[]> values)
    {
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            PetAttributes attributes = new PetAttributes
            (
                name:rowData[0],
                type:rowData[9],
                elementalType:Enum.TryParse(rowData[1], out ElementalType elementalType) ? elementalType : ElementalType.Normal,
                race: rowData[2],
                grade:Enum.TryParse(rowData[3], out Grade grade) ? grade : Grade.Common,
                attackSkills:rowData[3].Split(','),
                defenseSkills:rowData[4].Split(','),
                utilitySkills:rowData[5].Split(','),
                passiveSkills:rowData[6].Split(','),
                motivationSkills:rowData[7].Split(','),
                description:rowData[8]
            );
            if(!attributesList.Exists(i => i.Name == attributes.Name))
                attributesList.Add(attributes);
        }

        return attributesList;
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
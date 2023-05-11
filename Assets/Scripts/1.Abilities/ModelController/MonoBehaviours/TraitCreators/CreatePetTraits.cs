using System;
using System.Collections.Generic;

[System.Serializable]
public class CreatePetTraits : TraitsCreator
{
    public void SetPetAttributes(ref List<Pet> pets)
    {
        List<PetAttributes> attributesList = new List<PetAttributes>();

        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "PetAttributes":
                    info.LoadExcelDocument(CsvReader);
                    attributesList = LoadAttributes(info.GetDataList());
                    break;
                default:
                    continue;
            }
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
    
    private List<PetAttributes> LoadAttributes(List<string[]> values)
    {
        List<PetAttributes> result = new List<PetAttributes>(values.Count);
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            PetAttributes attributes = new PetAttributes
            (
                name:rowData[0],
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
            if(!result.Exists(i => i.Name == attributes.Name))
                result.Add(attributes);
        }

        return result;
    }
}
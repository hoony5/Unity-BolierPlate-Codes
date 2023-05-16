using System.Collections.Generic;

public class CreateCharacterAbility : AbilityModelCreator
{
    private string CharacterItemName => "Character";
    private string NpcStatusesSheetName => "NpcStatuses";
    public Character SetCharacter()
    {
        Character character = new Character();
        AbilityInfo abilityInfo = null;
        
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName == StatusTypesSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                abilityInfo = new AbilityInfo(LoadStatusTypesByModels(CharacterItemName, info.GetDataList()));
            }
            else if (info.sheetName == StatusesBaseSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                character.StatusAbility.SetAbility(abilityInfo);
                character.StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
            }
        }

        return character;
    }
    public List<NPC> SetNpcs()
    {
        List<NPC> npcs = new List<NPC>();
        AbilityInfo abilityInfo = null;
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName == StatusTypesSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                abilityInfo = new AbilityInfo(LoadStatusTypesByModels(CharacterItemName, info.GetDataList()));
            }
            else if (info.sheetName == StatusesBaseSheetName)
            {
                info.LoadExcelDocument(CsvReader);
                for (var index = 0; index < npcs.Count; index++)
                {
                    npcs[index] = new NPC();
                    npcs[index].StatusAbility.SetAbility(abilityInfo);
                    npcs[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                }
            }
        }

        SetAbilitiesValues(ref npcs);
        return npcs;
    }
    private void SetAbilitiesValues(ref List<NPC> npcs)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            if (info.sheetName != NpcStatusesSheetName) continue;
            info.LoadExcelDocument(CsvReader);
            LoadAllUnitsOriginalStatuses(ref npcs, info.sheetName, info.GetDataList());
        }
    }

    private void LoadAllUnitsOriginalStatuses(ref List<NPC> npcs , string originalStatusType ,List<string[]> values)
    {
        foreach (NPC npc in npcs)
        {
            StatusBaseAbility status = npc.StatusAbility.AbilityInfo.StatusesMap[originalStatusType];
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

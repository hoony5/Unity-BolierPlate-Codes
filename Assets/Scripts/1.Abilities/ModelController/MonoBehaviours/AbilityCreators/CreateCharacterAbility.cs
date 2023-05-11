using System.Collections.Generic;
using UnityEngine;

public class CreateCharacterAbility : AbilityModelCreator
{
    [field: SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field:SerializeField] public AbilityResourceInfo[] AllAbilityResourceInfos { get; private set; }

    public Character SetCharacter()
    {
        Character character = new Character();
        AbilityInfo abilityInfo = null;
        
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusTypes":
                    info.LoadExcelDocument(CsvReader);
                    abilityInfo = new AbilityInfo(LoadStatusTypesByModels("Character", info.GetDataList()));
                    break;
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    character = new NPC();
                    character.StatusAbility.SetAbility(abilityInfo);
                    character.StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                    break;
                default:
                    continue;
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
            switch (info.sheetName)
            {
                case "StatusTypes":
                    info.LoadExcelDocument(CsvReader);
                    abilityInfo = new AbilityInfo(LoadStatusTypesByModels("Character", info.GetDataList()));
                    break;
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    for (var index = 0; index < npcs.Count; index++)
                    {
                        npcs[index] = new NPC();
                        npcs[index].StatusAbility.SetAbility(abilityInfo);
                        npcs[index].StatusAbility.AbilityInfo.SetStatusBaseInfo(LoadStatusBasicNames(info.GetDataList()));
                    }
                    break;
                default:
                    continue;
            }
        }

        SetAbilitiesValues(ref npcs);
        return npcs;
    }
    private void SetAbilitiesValues(ref List<NPC> npcs)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "BattleStatuses":
                    info.LoadExcelDocument(CsvReader);
                    LoadAllUnitsOriginalStatuses(ref npcs, info.sheetName, info.GetDataList());
                    break;
                default:
                    continue;
            }
        }
    }

    [ToDo("Divide Datas each levels or contents")]
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

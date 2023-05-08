using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateNpcAbility : MonoBehaviour
{
    [field: SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field:SerializeField] public AbilityResourceInfo[] AllAbilityResourceInfos { get; private set; }

    public Character SetCharacter()
    {
        Character character = new Character();
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    character.StatusAbility.Ability =
                        new CharacterAbility(LoadStatusBasicNames(info.GetDataList()).GetStatuses());
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
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "StatusesBase":
                    info.LoadExcelDocument(CsvReader);
                    foreach (NPC npc in npcs)
                    {
                        npc.StatusAbility.Ability =
                            new CharacterAbility(LoadStatusBasicNames(info.GetDataList()).GetStatuses());   
                    }
                    break;
                default:
                    continue;
            }
        }

        SetNpcsAbilitiesValues(ref npcs);
        return npcs;
    }

    private Character LoadOriginalStatuses(List<string[]> values)
    {
        Character character = new Character();
        
        for (var index = 3; index < values.Count; index++)
        {
            OriginalStatusComponent result = new OriginalStatusComponent();
            string[] rowData = values[index];
            for (var i = 1; i < rowData.Length; i++)
            {
                result.SetBaseValue(values[0][i], float.TryParse(rowData[i], out float Value) ? Value : 0);   
            }
            character.StatusAbility.Ability.SetOriginalStatusComponent(result);
        }

        return character;
    }
    private void SetNpcsAbilitiesValues(ref List<NPC> npcs)
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                case "BattleStatuses":
                    info.LoadExcelDocument(CsvReader);
                    npcs = LoadAllUnitsOriginalStatuses(info.GetDataList());
                    break;
                default:
                    continue;
            }
        }
    }

    [ToDo("Divide Datas each levels or contents")]
    private List<NPC> LoadAllUnitsOriginalStatuses(List<string[]> values)
    {
        List<NPC> npcs = new List<NPC>(values.Count);
        
        for (var index = 3; index < values.Count; index++)
        {
            NPC npc = new NPC();
            OriginalStatusComponent result = new OriginalStatusComponent();
            string[] rowData = values[index];
            for (var i = 1; i < rowData.Length; i++)
            {
                result.SetBaseValue(values[0][i], float.TryParse(rowData[i], out float Value) ? Value : 0);   
            }
            npc.StatusAbility.Ability.SetOriginalStatusComponent(result);
        }

        return npcs;
    }

    private StatusBaseAbility LoadStatusBasicNames(List<string[]> values)
    {
        List<StatusItemInfo> result = new List<StatusItemInfo>(values.Count);
        StatusBaseAbility ability = new StatusBaseAbility();
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowData = values[index];
            StatusItemInfo itemInfo = new StatusItemInfo
            (
                rawName: rowData[0],
                displayName: rowData[1],
                index: int.TryParse(rowData[2], out int Index) ? Index : 0
            );
            
            if(!result.Exists(i => i.RawName == itemInfo.RawName))
                result.Add(itemInfo);
        }
        ability.AddStatusesBaseInfo(result);
        return ability;
    }
}

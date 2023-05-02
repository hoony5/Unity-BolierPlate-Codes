using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleFormulaInfoManager
{
    [field:SerializeField] private AbilityResourceInfo[] AbilityResourceInfos { get; set; }
    [field:SerializeField] private List<BattleFormulaInfo> AllBattleFormulas {get; set;}

    public void LoadBattleFormulas()
    {
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            // add info sheet name
            AllBattleFormulas.AddRange(LoadAllBattleFormulas(info.GetAbilityDatas()));
        }
    }
    private List<BattleFormulaInfo> LoadAllBattleFormulas(List<string[]> values)
    {
        List<BattleFormulaInfo> result = new List<BattleFormulaInfo>(values.Count);
        foreach (string[] rowData in values)
        {
            BattleFormulaInfo formula = new BattleFormulaInfo
                (
                    name: rowData[0],
                    useClampValue: bool.Parse(rowData[1]),
                    min: int.Parse(rowData[2]),
                    max: int.Parse(rowData[3]),
                    description: rowData[4]
                );
            
            
            if(!result.Contains(formula)) 
                result.Add(formula);
        }

        return result;
    }
}

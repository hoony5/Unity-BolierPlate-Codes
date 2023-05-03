using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BattleFormulaStatManager
{
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get; private set; }
    private Dictionary<string, FormulaStat> AllFormulaStats {get; set;}
    
    public bool TryGetValue(string formulaName, out FormulaStat formulaStat)
    {
        return AllFormulaStats.TryGetValue(formulaName, out formulaStat);
    }
    public void LoadBattleFormulas()
    {
        List<FormulaStat> result = new List<FormulaStat>(64);
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            // add info sheet name
            result.AddRange(LoadAllFormulaStats(info.GetAbilityDatas()));
        }

        AllFormulaStats = result.ToDictionary(key => key.FormulaName, value => value);
    }

    private List<FormulaStat> LoadAllFormulaStats(List<string[]> values)
    {
        List<FormulaStat> stats = new List<FormulaStat>(values.Count);
        List<float> BaseValues = new List<float>(values.Count);
        string currentFormula = string.Empty;
        string nextFormula = string.Empty;
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];

            currentFormula = string.IsNullOrEmpty(rowDatas[0]) ? currentFormula : rowDatas[0];
            nextFormula = index < values.Count - 1 ? values[index + 1][0] : currentFormula;
            
            string currentStatusName = rowDatas[1];
            string nextStatusName = index < values.Count - 1 ? values[index + 1][1] : currentStatusName;

            if (!string.IsNullOrEmpty(nextStatusName))
            {
                if (index != values.Count - 1 && string.IsNullOrEmpty(nextStatusName)) continue;
                FormulaStat valueInfo = new FormulaStat
                (
                    formulaName: currentFormula,
                    statusName: rowDatas[1],
                    reflectStatTarget: Enum.TryParse(rowDatas[2], out ReflectStatTarget ReflectStatTarget) ? ReflectStatTarget : ReflectStatTarget.None,
                    reflectValue: float.TryParse(rowDatas[3], out float ReflectValue) ? ReflectValue : 0,
                    dataUnitType: Enum.TryParse(rowDatas[4], out DataUnitType DataUnitType) ? DataUnitType : DataUnitType.None,
                    baseValues: BaseValues.ToArray(),
                    maxLevel: int.TryParse(rowDatas[6], out int MaxLevel) ? MaxLevel : 1
                );
                stats.Add(valueInfo);
                currentFormula = nextFormula;
            }
            else
            {
                float baseValue = float.TryParse(rowDatas[5], out float BaseValue) ? BaseValue : 0;
                BaseValues.Add(baseValue);
            }
        }

        return stats;
    }
}

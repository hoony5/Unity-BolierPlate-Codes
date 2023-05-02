using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleFormulaStatManager
{
    [field:SerializeField] private AbilityResourceInfo[] AbilityResourceInfos { get; set; }
    [field:SerializeField] private Dictionary<string, List<FormulaStat>> AllFormulaStats {get; set;}
    
    public void LoadBattleFormulas()
    {
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            // add info sheet name
        }
    }

    private void LoadAllFormulaStats(List<string[]> values)
    {
        List<FormulaStat> stats = new List<FormulaStat>(values.Count);
        List<float> baseValues = new List<float>(values.Count);
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];

            string currentFormula = rowDatas[0];
            string nextFormula = index < values.Count - 1 ? values[index + 1][0] : currentFormula;
            string currentStatusName = rowDatas[1];
            string nextStatusName = index < values.Count - 1 ? values[index + 1][1] : currentStatusName;


            if (!string.IsNullOrEmpty(nextFormula))
            {
                if (index != values.Count - 1 && string.IsNullOrEmpty(nextFormula)) continue;
                if (!string.IsNullOrEmpty(nextStatusName))
                {
                    if (index != values.Count - 1 && string.IsNullOrEmpty(nextStatusName)) continue;
                    FormulaStat valueInfo = new FormulaStat
                    (
                        statusName: rowDatas[1],
                        reflectStatTarget: Enum.TryParse(rowDatas[2], out ReflectStatTarget ReflectStatTarget) ? ReflectStatTarget : ReflectStatTarget.None,
                        reflectValue: float.TryParse(rowDatas[3], out float ReflectValue) ? ReflectValue : 0,
                        calculationType: Enum.TryParse(rowDatas[4], out CalculationType CalculationType) ? CalculationType : CalculationType.None,
                        dataUnitType: Enum.TryParse(rowDatas[5], out DataUnitType DataUnitType) ? DataUnitType : DataUnitType.None,
                        baseValues: baseValues.ToArray(),
                        maxLevel: int.TryParse(rowDatas[7], out int maxLevel) ? maxLevel : 1
                    );
                    stats.Add(valueInfo);
                }
                else
                {
                    float BaseValue = float.TryParse(rowDatas[6], out float baseValue) ? baseValue : 0;
                    baseValues.Add(BaseValue);
                }
            }
            else
            {
                                               
            }
            
        }

        return stats;
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleFormula
{
    [field:SerializeField] public string FormulaName { get; private set; }
    [SerializeField] private List<BattleFormulaInfo> formulaInfoList;
    [SerializeField] private string description;
}

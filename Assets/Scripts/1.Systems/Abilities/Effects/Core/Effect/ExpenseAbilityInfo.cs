using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpenseAbilityInfo
{
    [field:SerializeField] public string EffectName { get; set; }
    [field:SerializeField] public List<ExpenseAbilityStat> ExpenseStats { get; set; }
    
    public ExpenseAbilityInfo()
    {
        ExpenseStats = new List<ExpenseAbilityStat>(16);
    }
    
    public ExpenseAbilityInfo(string effectName, List<ExpenseAbilityStat> expenseStats)
    {
        EffectName = effectName;
        ExpenseStats = expenseStats;
    }
}

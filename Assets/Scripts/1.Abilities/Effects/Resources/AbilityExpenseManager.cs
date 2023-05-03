using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AbilityExpenseManager : MonoBehaviour
{ 
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get; private set; }
    [SerializeField] private List<ExpenseAbilityInfo> costs;
    private Dictionary<string, ExpenseAbilityInfo> _costIndexMap;
    
    private void ConvertToDictionary()
    {
        _costIndexMap = new Dictionary<string, ExpenseAbilityInfo>(128);
        _costIndexMap = costs.ToDictionary(key => key.EffectName, value => value);
    }

    public void SetExpenseInfos(ref List<BattleBehaviour> battleBehaviours)
    {
        costs.Clear();
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            AllLoadExpenseInfo(info.GetAbilityDatas());
        }

        ConvertToDictionary();

        foreach (BattleBehaviour behaviour in battleBehaviours)
        {
            if (!_costIndexMap.TryGetValue(behaviour.BehaviourName, out ExpenseAbilityInfo value)) continue;
            
            behaviour.ExpenseAbilityInfos  = value;
        }
    }

    private void AllLoadExpenseInfo(List<string[]> values)
    {
        costs ??= new List<ExpenseAbilityInfo>();
        
        List<ExpenseAbilityStat> stats = new List<ExpenseAbilityStat>();
        string currentEffectName = string.Empty;
        string nextEffectName = string.Empty;

        for (var index = 0; index < values.Count; index++)
        {
            currentEffectName = string.IsNullOrEmpty(values[index][0]) ? currentEffectName : values[index][0];
            nextEffectName = index < values.Count - 1 ? values[index + 1][0] : currentEffectName;

            if (!string.IsNullOrEmpty(nextEffectName))
            {
                if (index != values.Count - 1 && string.IsNullOrEmpty(nextEffectName)) continue;

                ExpenseAbilityInfo info = new ExpenseAbilityInfo()
                {
                    EffectName = currentEffectName,
                    ExpenseStats = stats
                };
                costs.Add(info);
                stats.Clear();
            }
            else
            {
                ExpenseAbilityStat stat = new ExpenseAbilityStat()
                {
                    IsContinuousCost = bool.TryParse(values[index][1], out bool isContinuousCost) && isContinuousCost,
                    ReflectCurrentStatName = values[index][2],
                    ReflectMaxStatName = values[index][3],
                    Cost = float.TryParse(values[index][4], out float cost) ? cost : 0f,
                    CurrentLevel = int.TryParse(values[index][5], out int currentLevel) ? currentLevel : 0,
                    MaxLevel = int.TryParse(values[index][6], out int maxLevel) ? maxLevel : 0,
                    DataUnitType = (DataUnitType)Enum.Parse(typeof(DataUnitType), values[index][7]),
                    CalculationType = (CalculationType)Enum.Parse(typeof(CalculationType), values[index][8]),
                };
                stats.Add(stat);
            }
        }
    }
}

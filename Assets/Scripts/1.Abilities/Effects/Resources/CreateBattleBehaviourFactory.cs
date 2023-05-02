using System.Collections.Generic;
using UnityEngine;

public class CreateBattleBehaviourFactory : MonoBehaviour
{
    private List<BattleBehaviour> allBattleBehaviours;
    private Dictionary<string,BattleBehaviour> allBattleBehavioursMap = new Dictionary<string, BattleBehaviour>(64);
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos{get; private set;}
    [field:SerializeField] public BattleFormula[] AllBattleFormulas{get; private set;}
    [field:SerializeField] public AbilityExpenseManager ExpenseManager{get; private set;}
    [field:SerializeField] public BehaviourInfoManager BehaviourInfoManager{get; private set;}
    [field:SerializeField] public BehaviourReferenceManager BehaviourReferenceManager{get; private set;}

    public bool TryGetValue(string key, out BattleBehaviour value)
    {
        return allBattleBehavioursMap.TryGetValue(key, out value);
    }
    public void CreateBehaviourList()
    {
        allBattleBehaviours = new List<BattleBehaviour>(16);
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            List<string[]> data = info.GetAbilityDatas();
            // info SheetName ?
            
            allBattleBehaviours.AddRange(LoadBattleBehaviourInfos(data));
        }

        SetBattleFormulaInfos(ref allBattleBehaviours);
        ExpenseManager.SetExpenseInfos(ref allBattleBehaviours);
    }
    private List<BattleBehaviour> LoadBattleBehaviourInfos(List<string[]> values)
    {
        List<BattleBehaviour> result = new List<BattleBehaviour>(values.Count);
        foreach (string[] rowData in values)
        {
            BattleBehaviour behaviour = new BattleBehaviour()
            {
                BehaviourName = rowData[0],
                Effects = rowData[1].Split(',')
            };
            
            if(!result.Contains(behaviour)) 
                result.Add(behaviour);
        }
        BehaviourInfoManager.SetBehaviourLevelInfo(ref result);
        BehaviourReferenceManager.SetBehaviourReferenceInfo(ref result);
        return result;
    }
    
    private void SetBattleFormulaInfos(ref List<BattleBehaviour> inputBehaviours)
    {
        foreach (BattleFormula formula in AllBattleFormulas)
        {
            foreach (BattleBehaviour behaviour in inputBehaviours)
            {
                if (behaviour.BehaviourName != formula.FormulaName) continue;
                behaviour.BattleFormula = formula;
            }
        }
    }
}

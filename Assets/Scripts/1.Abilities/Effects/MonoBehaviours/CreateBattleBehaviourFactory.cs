﻿using System.Collections.Generic;
using UnityEngine;
using Utility.ExcelReader;

public class CreateBattleBehaviourFactory : MonoBehaviour
{
    [field:SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    
    private List<BattleBehaviour> allBattleBehaviours;
    private Dictionary<string,BattleBehaviour> allBattleBehavioursMap = new Dictionary<string, BattleBehaviour>(64);
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos{get; private set;}
    [field:SerializeField] public CreateBattleFormula FormulaCreator{get; private set;}
    [field:SerializeField] public AbilityExpenseManager ExpenseManager{get; private set;}
    [field:SerializeField] public BehaviourInfoManager BehaviourInfoManager{get; private set;}
    [field:SerializeField] public BehaviourReferenceManager BehaviourReferenceManager{get; private set;}

    public bool TryGetValue(string keyWhichIsBehaviourName, out BattleBehaviour value)
    {
        return allBattleBehavioursMap.TryGetValue(keyWhichIsBehaviourName, out value);
    }
    public void CreateBehaviourList()
    {
        allBattleBehaviours = new List<BattleBehaviour>(16);
        LoadDocuments(AbilityResourceInfos);
        LoadDocuments(ExpenseManager.AbilityResourceInfos);
        
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            List<string[]> data = info.GetDataList();
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
            BattleBehaviour behaviour = new BattleBehaviour
            (
                name: rowData[0],
                effects: rowData[1].Split(','),
                grade:rowData[2],
                rank:rowData[3],
                maxRank:rowData[4]
            );
            
            if(!result.Contains(behaviour)) 
                result.Add(behaviour);
        }
        BehaviourInfoManager.SetBehaviourLevelInfo(ref result);
        BehaviourReferenceManager.SetBehaviourReferenceInfo(ref result);
        return result;
    }
    
    private void SetBattleFormulaInfos(ref List<BattleBehaviour> inputBehaviours)
    {
        foreach (BattleFormulaInfo info in FormulaCreator.BattleFormulaInfoManager.AllBattleFormulas)
        {
            foreach (BattleBehaviour behaviour in inputBehaviours)
            {
                if (behaviour.Name != info.Name) continue;
                behaviour.FormulaInfo = info;
            }
        }
    }
    private void LoadDocuments(AbilityResourceInfo[] abilityResourceInfos)
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            info.LoadExcelDocument(CsvReader);
        }    
    }
}

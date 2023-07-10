using UnityEngine;

[System.Serializable]
public class BattleBehaviour
{
    [field:SerializeField] public string Name {get;  set;}
    [field:SerializeField] public string[] Effects{get; set;}
    [field:SerializeField] public Rank Rank {get;  set;}
    [field:SerializeField] public Rank MaxRank {get;  set;}
    [field:SerializeField] public Grade Grade {get;  set;}
    [field:SerializeField] public BattleFormulaInfo FormulaInfo{get; set;}
    [field:SerializeField] public BehaviourValueInfo BehaviourValueInfo{get; set;}
    [field:SerializeField] public ExpenseAbilityInfo ExpenseAbilityInfos{get; set;}
    [field:SerializeField] public BehaviourReferenceInfo BehaviourReferenceInfo{get; set;}
    public void UpgradeRank()
    {
        int rank = (int)Rank;
        int maxRank = (int)MaxRank;
        
        rank++;
        if(rank > maxRank) Rank = MaxRank;
        else Rank = (Rank)rank;
    }
}
using UnityEngine;

[System.Serializable]
public class BattleBehaviour
{
    [field:SerializeField] public string Name {get; private set;}
    [field:SerializeField] public string[] Effects{get; set;}
    [field:SerializeField] public Rank Rank {get; private set;}
    [field:SerializeField] public Rank MaxRank {get; private set;}
    [field:SerializeField] public Grade Grade {get; private set;}
    [field:SerializeField] public BattleFormulaInfo FormulaInfo{get; set;}
    [field:SerializeField] public BehaviourValueInfo BehaviourValueInfo{get; set;}
    [field:SerializeField] public ExpenseAbilityInfo ExpenseAbilityInfos{get; set;}
    [field:SerializeField] public BehaviourReferenceInfo BehaviourReferenceInfo{get; set;}

    public BattleBehaviour(string name, string[] effects, string grade, string rank, string maxRank)
    {
        Name = name;
        Effects = effects;
        Rank = (Rank)System.Enum.Parse(typeof(Rank), rank);
        MaxRank = (Rank)System.Enum.Parse(typeof(Rank), maxRank);
        Grade = (Grade)System.Enum.Parse(typeof(Grade), grade);
    }

    public void UpgradeRank()
    {
        int rank = (int)Rank;
        int maxRank = (int)MaxRank;
        
        rank++;
        if(rank > maxRank) Rank = MaxRank;
        else Rank = (Rank)rank;
    }
}
using UnityEngine;

[System.Serializable]
public class BattleBehaviour
{
    [field:SerializeField] public string BehaviourName {get; set;}
    [field:SerializeField] public string[] Effects{get; set;}
    [field:SerializeField] public BattleFormulaInfo FormulaInfo{get; set;}
    [field:SerializeField] public BehaviourValueInfo BehaviourValueInfo{get; set;}
    [field:SerializeField] public ExpenseAbilityInfo ExpenseAbilityInfos{get; set;}
    [field:SerializeField] public BehaviourReferenceInfo BehaviourReferenceInfo{get; set;}
}
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newSkillAbility", menuName = "ScriptableObject/Battle/SkillAbility", order = 0)]
public class SkillAbility : ScriptableObject
{
    // Target Stat Infos
    // Ex. other Defense -= my Damage *0.1f , Hp -= my Damage
    [SerializeField] private SkillItemInfo[] skillEffectsForTarget;
    [SerializeField] private SkillItemInfo[] skillEffectsForPlayer;
    
    public void Apply(Character character)
    {
        foreach (SkillItemInfo skillEffect in skillEffectsForTarget)
        {
            string appliedStatusName = skillEffect.StatusItemInfo.RawName;
            float appliedStatusBaseValue = skillEffect.StatusItemInfo.Value;
            float finalizedValue = 0;
            CalculationOrder calculationOrder = skillEffect.CalculationOrder;
            
            switch (calculationOrder)
            {
                case CalculationOrder.Adjustment:
                    finalizedValue = appliedStatusBaseValue + skillEffect.Adjustment;
                    finalizedValue *= skillEffect.Scale;
                    break;
                case CalculationOrder.Scale:
                    finalizedValue= appliedStatusBaseValue * skillEffect.Scale;
                    finalizedValue += skillEffect.Adjustment;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            character.StatusAbility.GetStatusValue(appliedStatusName);
        }
    }
}
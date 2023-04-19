using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CastTeamAbility : Effect, ICastTeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
    public bool HasThresholdPassed(float threshold)
    {
        return threshold >= Threshold;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
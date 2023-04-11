using System.Collections.Generic;
using UnityEngine;

public class CastTeamAbility : Effect, ICastTeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}
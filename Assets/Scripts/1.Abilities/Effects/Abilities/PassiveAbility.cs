using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : Effect, ITeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
}
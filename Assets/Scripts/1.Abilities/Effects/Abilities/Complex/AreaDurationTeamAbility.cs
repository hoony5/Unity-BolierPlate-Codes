using System.Collections.Generic;
using UnityEngine;

public class AreaDurationTeamAbility : Effect, IAreaDurationTeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
}
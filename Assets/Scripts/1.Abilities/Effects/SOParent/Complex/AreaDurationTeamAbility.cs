using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaDurationTeamEffect", menuName = "ScriptableObject/Battle/Complex/AreaDurationTeamEffect", order = 0)]
public class AreaDurationTeamAbility : EffectReferenceInfo, IAreaDurationTeamAbility
{
    public float Duration { get; set; }
    public float Range { get; set; }
    public bool BuffOrDebuff { get; set; }
    public List<EffectAbility> EffectAbilities { get; set; }
    
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
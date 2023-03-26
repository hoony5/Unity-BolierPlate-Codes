using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CastAreaDurationTeamEffect",
    menuName = "ScriptableObject/Battle/Combined/Time/CastAreaDurationTeamEffect", order = 0)]
public class CastAreaDurationTeamEffect : EffectInfoBase, ICastAreaDurationTeamEffect
{
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
}
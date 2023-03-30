using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DurationTeamEffect", menuName = "ScriptableObject/Battle/Combined/Time/DurationTeamEffect", order = 0)]
public class DurationTeamAbility : EffectItem, IDurationTeamAbility
{
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
}
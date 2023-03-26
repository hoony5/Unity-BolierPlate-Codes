using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CastTeamEffect",
    menuName = "ScriptableObject/Battle/Combined/Time/CastTeamEffect", order = 0)]
public class CastTeamEffect : EffectInfoBase, ICastTeamEffect
{
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}
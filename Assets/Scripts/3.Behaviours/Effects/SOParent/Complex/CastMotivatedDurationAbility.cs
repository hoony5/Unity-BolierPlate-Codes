using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CastMotivatedDurationEffect",
    menuName = "ScriptableObject/Battle/Combined/Time/CastMotivatedDurationEffect", order = 0)]
public class CastMotivatedDurationAbility : EffectInfoBase, ICastMotivatedDurationAbility
{
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public float Threshold { get; set; }
    [field: SerializeField] public List<EffectAbility> EffectAbilities { get; set; }

    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
}
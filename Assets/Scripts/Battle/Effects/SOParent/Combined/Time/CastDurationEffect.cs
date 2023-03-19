using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CastDurationEffect",
    menuName = "ScriptableObject/Battle/Combined/Time/CastDurationEffect", order = 0)]
public class CastDurationEffect : EffectInfoBase, IDurationEffect, IThresholdEffect
{
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public float Threshold { get; set; }

    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}
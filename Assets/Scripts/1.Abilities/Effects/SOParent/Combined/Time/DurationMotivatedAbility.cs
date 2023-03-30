
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DurationMotivatedEffect", menuName = "ScriptableObject/Battle/Combined/Time/DurationMotivatedEffect", order = 0)]
public class DurationMotivatedAbility : EffectItem, IDurationMotivatedAbility
{
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
}
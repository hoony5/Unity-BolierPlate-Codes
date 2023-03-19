using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaDurationAimedMotivatedEffect", menuName = "ScriptableObject/Battle/Complex/AreaDurationAimedMotivatedEffect", order = 0)]
public class AreaDurationAimedMotivatedEffect : EffectInfoBase, IAreaEffect, ISearchAbilityEffect, ISearchStateEffect, IDurationEffect,IMotivatedEffect
{
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    [field:SerializeField] public List<StatusItemInfo> SearchStats { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
   
    public bool TryUpdateEffect(Character other, string abilityName, float threshold, float value)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckState(Character character, string stateName)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
}
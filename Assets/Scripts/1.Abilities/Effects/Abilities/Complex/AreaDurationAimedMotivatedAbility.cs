﻿using System.Collections.Generic;
using UnityEngine;

public class AreaDurationAimedMotivatedAbility : Effect, IAreaDurationAimedMotivatedAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    [field:SerializeField] public List<StatusItemInfo> SearchStats { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
   
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

    public bool TryCheckTag(Character other, string tag)
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
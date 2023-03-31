﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaAimedEffect", menuName = "ScriptableObject/Battle/Combined/Area/AreaAimedEffect", order = 0)]
public class AreaAimedAbility : EffectReferenceInfo, IAreaAimedAbility
{
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public List<StatusItemInfo> SearchStats { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    
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
}
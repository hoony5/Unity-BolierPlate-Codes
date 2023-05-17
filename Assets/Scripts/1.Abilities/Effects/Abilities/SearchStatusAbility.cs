﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SearchStatusAbility : Effect, ISearchStatusAbility,ISearchStateAbility,ISearchTagAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<SearchStatusItem> SearchStats { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public bool FindCharacterStatus(Character other, float threshold = 0.01f)
    {
        for(var i = 0 ; i < SearchStats.Count; i++)
        {
            SearchStatusItem stat = SearchStats[i];
            stat.isMeetCondition = stat.statusItemInfo.Value - other.StatusAbility.GetStatusValue(stat.statusItemInfo.RawName) < threshold;
        
            if (!stat.isMeetCondition)
                return false;
        }

        return true;
    }

    public bool FindCharacterState(Character character, string stateName)
    {
        bool positiveBattleEffect = character.StatusAbility.EffectDashBoard.ExistPositiveBattleEffect(stateName);
        bool negativeBattleEffect = character.StatusAbility.EffectDashBoard.ExistNegativeBattleEffect(stateName);
        bool positiveGlobalEffect = character.StatusAbility.EffectDashBoard.ExistPositiveGlobalEffect(stateName);
        bool negativeGlobalEffect = character.StatusAbility.EffectDashBoard.ExistNegativeGlobalEffect(stateName);
        
        return positiveGlobalEffect || negativeGlobalEffect || positiveBattleEffect || negativeBattleEffect;
    }

    public bool FindTag(Character other)
    {
        return other.Transform is not null && other.Transform.CompareTag(SearchTag);
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
    public void AddStackCount()
    {
        if (!IsStackable) return;
        StackCount++;
        if(StackCount > MaxStackCount) StackCount = MaxStackCount;
    }

    public void SubtractStackCount()
    {
        if (!IsStackable) return;
        StackCount--;
        if (StackCount <= 0) StackCount = 1;
    }

    public void ResetStackCount()
    {
        if (!IsStackable) return;
        StackCount = 1;
    }
}
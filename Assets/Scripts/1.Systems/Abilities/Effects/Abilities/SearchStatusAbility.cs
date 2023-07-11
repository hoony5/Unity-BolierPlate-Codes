using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SearchStatusAbility : Effect, ISearchStatusAbility, ISearchStateAbility, ISearchTagAbility
{
    [field:SerializeField] public SearchType SearchType { get; set; }
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

    public bool IsHitChance(float chance) => chance <= Chance;

    public bool SearchTarget(Character other)
    {
        switch (SearchType)
        {
            case SearchType.None:
                return false;
            case SearchType.Status:
                return FindCharacterStatus(other);
            case SearchType.State:
                return FindCharacterState(other);
            case SearchType.Tag:
                return FindTag(other);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public bool FindCharacterStatus(Character other, float threshold = 0.01f)
    {
        foreach (SearchStatusItem stat in SearchStats)
        {
            stat.isMeetCondition = stat.StatusItemInfo.Value - other.StatusAbility.GetStatusValue(stat.StatusItemInfo.RawName) < threshold;

            if (!stat.isMeetCondition)
                return false;
        }

        return true;
    }

    public bool FindCharacterState(Character character)
    {
        bool positiveBattleEffect = character.StatusAbility.EffectDashBoard.ExistPositiveBattleEffect(SearchState);
        bool negativeBattleEffect = character.StatusAbility.EffectDashBoard.ExistNegativeBattleEffect(SearchState);
        bool positiveGlobalEffect = character.StatusAbility.EffectDashBoard.ExistPositiveGlobalEffect(SearchState);
        bool negativeGlobalEffect = character.StatusAbility.EffectDashBoard.ExistNegativeGlobalEffect(SearchState);

        return positiveGlobalEffect || negativeGlobalEffect || positiveBattleEffect || negativeBattleEffect;
    }

    public bool FindTag(Character other)
    {
        return other.Transform is not null && other.Transform.CompareTag(SearchTag);
    }

    public void AddStackCount()
    {
        if (CanChangeStackCount())
        {
            StackCount++;
            if (StackCount > MaxStackCount) StackCount = MaxStackCount;
        }
    }

    public void SubtractStackCount()
    {
        if (CanChangeStackCount())
        {
            StackCount--;
            if (StackCount <= 0) StackCount = 1;
        }
    }

    public void ResetStackCount()
    {
        if (CanChangeStackCount())
        {
            StackCount = 1;
        }
    }

    private bool CanChangeStackCount()
    {
        return IsStackable;
    }
}

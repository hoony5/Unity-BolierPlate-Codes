using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaDurationAimedStatusAbility : Effect, IAreaDurationAimedStatusAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }

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
    public int DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result)
    {
        Transform transform = character.transform;
        Vector3 position = transform.position;
        Vector3 detectorSize = new Vector3(Range, position.y * 0.5f, Range);
        return Physics.OverlapBoxNonAlloc(position,  detectorSize, result, Quaternion.identity, areaMask);
    }
    public bool FindTag(Character other)
    {
        return other.CompareTag(SearchTag);
    }
   
    public bool FindCharacterState(Character character, string stateName)
    {
        bool positiveBattleEffect = character.StatusAbility.EffectDashBoard.ExistPositiveBattleEffect(stateName);
        bool negativeBattleEffect = character.StatusAbility.EffectDashBoard.ExistNegativeBattleEffect(stateName);
        bool positiveGlobalEffect = character.StatusAbility.EffectDashBoard.ExistPositiveGlobalEffect(stateName);
        bool negativeGlobalEffect = character.StatusAbility.EffectDashBoard.ExistNegativeGlobalEffect(stateName);
        
        return positiveGlobalEffect || negativeGlobalEffect || positiveBattleEffect || negativeBattleEffect;
    }

    public bool HasTimePassed(float currentDuration)
    {
        return currentDuration >= Duration;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
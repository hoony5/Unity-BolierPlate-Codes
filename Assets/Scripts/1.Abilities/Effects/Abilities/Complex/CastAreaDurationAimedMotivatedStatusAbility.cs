using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CastAreaDurationAimedMotivatedStatusAbility : Effect, ICastAreaDurationAimedMotivatedStatusAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public MotivationInfo MotivationInfo { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
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
    public bool HasThresholdPassed(float threshold, bool isHit)
    {
        return  threshold >= Threshold && !isHit;
    }
 
    public void SetMotivationActive(Character character, Character orOther)
    {
        if(orOther is null) return;
        if(MotivationInfo.MotivationStatusInfos is null) return;
        if(MotivationInfo.MotivationStatusInfos.Count == 0) return;
        
        for (var i = 0; i < MotivationInfo.MotivationStatusInfos.Count; i++)
        {
            MotivationStatusInfo motivationData = MotivationInfo.MotivationStatusInfos[i];

            if (motivationData.MotivationComparerType is ComparerType.None) continue;
            if (motivationData.ApplyTargetType is ApplyTargetType.None) continue;

            // motivation Active
            motivationData.MotivationActive = motivationData.MotivationComparerType switch
            {
                ComparerType.Equal => IsMotivatedWhenApproximately(character, orOther),
                ComparerType.GreaterOrEqual => IsMotivatedWhenLess(character, orOther),
                ComparerType.LessOrEqual => IsMotivatedWhenGreater(character, orOther),
                _ => false
            };

            if (!motivationData.MotivationActive) continue;
            
            // Apply motivation Value to character Data
            ApplyMotivationStatus(character,orOther,motivationData);
        }
    }
    public void ApplyMotivationStatus(Character character, Character enemy, MotivationStatusInfo motivationStatusInfo)
    {
        //Calculate 
        string statusName = motivationStatusInfo.HasReflectMaxStatus ?
            motivationStatusInfo.MaxStatName :
            motivationStatusInfo.CurrentStatName;
        
        int index = motivationStatusInfo.HasReflectMyStatus ? 
            character.StatusAbility.Ability.AllStatusInfos.GetStatusIndex(statusName) :
            enemy.StatusAbility.Ability.AllStatusInfos.GetStatusIndex(statusName);

        float status = motivationStatusInfo.HasReflectMyStatus ? 
            character.StatusAbility.Ability.MotivationStatus.GetStatuses()[index].Value :
            enemy.StatusAbility.Ability.MotivationStatus.GetStatuses()[index].Value;

        float motivatedValue = motivationStatusInfo.CalculationType switch
        {
            CalculationType.None => 0,
            CalculationType.Equalize => motivationStatusInfo.MotivatedValue,
            CalculationType.Add when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Numeric =>
                status + motivationStatusInfo.MotivatedValue,
            CalculationType.Add when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Percentage =>
                status + status * (1 + 0.01f * motivationStatusInfo.MotivatedValue),
            CalculationType.Multiply when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Numeric =>
                status * motivationStatusInfo.MotivatedValue,
            _ => throw new ArgumentOutOfRangeException(@$"
{motivationStatusInfo.CalculationType} |
{motivationStatusInfo.MotivatedValueUnitType} not implement yet.")
        } 
        // if stackable, multiply stackCount
        * (IsStackable ? StackCount : 1); 

        // if previousValue is not 0, reset currentValue
        if (Mathf.Abs(motivationStatusInfo.PreviousValue) > 0.001f)
        {
            switch (motivationStatusInfo.ApplyTargetType)
            {
                case ApplyTargetType.Player:
                case ApplyTargetType.PlayerTeam:
                case ApplyTargetType.RandomPlayerTeam:
                    character.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, -motivationStatusInfo.PreviousValue);
                    break;
                case ApplyTargetType.Enemy:
                case ApplyTargetType.EnemyTeam:
                case ApplyTargetType.RandomEnemyTeam:
                    enemy.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, -motivationStatusInfo.PreviousValue);
                    break;
                case ApplyTargetType.RandomAll:
                case ApplyTargetType.All:
                    character.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, -motivationStatusInfo.PreviousValue);
                    enemy.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, -motivationStatusInfo.PreviousValue);
                    break;
            }
        }
        //override previousValue to new value
        motivationStatusInfo.PreviousValue = motivatedValue;
        
        switch (motivationStatusInfo.ApplyTargetType)
        {
            case ApplyTargetType.Player:
            case ApplyTargetType.PlayerTeam:
            case ApplyTargetType.RandomPlayerTeam:
                character.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, motivatedValue);
                break;
            case ApplyTargetType.Enemy:
            case ApplyTargetType.EnemyTeam:
            case ApplyTargetType.RandomEnemyTeam:
                enemy.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, motivatedValue);
                break;
            case ApplyTargetType.RandomAll:
            case ApplyTargetType.All:
                character.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, motivatedValue);
                enemy.StatusAbility.Ability.MotivationStatus.AddBaseValue(statusName, motivatedValue);
                break;
        }
    }
    public bool IsMotivatedWhenGreater(Character character, Character orOther)
    {
        if (MotivationInfo.MotivationStatusInfos is null) return false;
        if (MotivationInfo.MotivationStatusInfos.Count == 0) return false;

        Character target;
        float criteriaValue = 0;
        for (var i = 0; i < MotivationInfo.MotivationStatusInfos.Count; i++)
        {
            MotivationStatusInfo motivationData = MotivationInfo.MotivationStatusInfos[i];
            target = motivationData.HasReflectMyStatus ? character : orOther;
            
            if(motivationData.ReflectValueUnitType is DataUnitType.Percentage)
                criteriaValue = character.StatusAbility.GetStatusValue(motivationData.MaxStatName) * motivationData.ReflectValue * 0.01f;
            if(motivationData.ReflectValueUnitType is DataUnitType.Numeric)
                criteriaValue = motivationData.ReflectValue;
            
            bool isFulfillCondition =  target.StatusAbility.GetStatusValue(motivationData.CurrentStatName) > criteriaValue;

            if (!isFulfillCondition) return false;
        }

        return true;
    }
    public bool IsMotivatedWhenLess(Character character, Character orOther)
    {
      
        if (MotivationInfo.MotivationStatusInfos is null) return false;
        if (MotivationInfo.MotivationStatusInfos.Count == 0) return false;

        Character target;
        float criteriaValue = 0;
        for (var i = 0; i < MotivationInfo.MotivationStatusInfos.Count; i++)
        {
            MotivationStatusInfo motivationData = MotivationInfo.MotivationStatusInfos[i];
            target = motivationData.HasReflectMyStatus ? character : orOther;
            
            if(motivationData.ReflectValueUnitType is DataUnitType.Percentage)
                criteriaValue = character.StatusAbility.GetStatusValue(motivationData.MaxStatName) * motivationData.ReflectValue * 0.01f;
            if(motivationData.ReflectValueUnitType is DataUnitType.Numeric)
                criteriaValue = motivationData.ReflectValue;
            
            bool isFulfillCondition =  target.StatusAbility.GetStatusValue(motivationData.CurrentStatName) < criteriaValue;

            if (!isFulfillCondition) return false;
        }

        return true;
    }
    public bool IsMotivatedWhenApproximately(Character character, Character orOther, float threshold = 0.01f)
    {
        
        if (MotivationInfo.MotivationStatusInfos is null) return false;
        if (MotivationInfo.MotivationStatusInfos.Count == 0) return false;

        Character target;
        float criteriaValue = 0;
        for (var i = 0; i < MotivationInfo.MotivationStatusInfos.Count; i++)
        {
            MotivationStatusInfo motivationData = MotivationInfo.MotivationStatusInfos[i];
            target = motivationData.HasReflectMyStatus ? character : orOther;
            
            if(motivationData.ReflectValueUnitType is DataUnitType.Percentage)
                criteriaValue = character.StatusAbility.GetStatusValue(motivationData.MaxStatName) * motivationData.ReflectValue * 0.01f;
            if(motivationData.ReflectValueUnitType is DataUnitType.Numeric)
                criteriaValue = motivationData.ReflectValue;
            
            bool isFulfillCondition =  target.StatusAbility.GetStatusValue(motivationData.CurrentStatName) - criteriaValue < threshold;

            if (!isFulfillCondition) return false;
        }

        return true;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
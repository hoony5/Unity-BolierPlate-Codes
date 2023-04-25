using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaMotivatedAbility : Effect, IAreaMotivatedAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public MotivationInfo MotivationInfo { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public int DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result)
    {
        Transform transform = character.transform;
        Vector3 position = transform.position;
        Vector3 detectorSize = new Vector3(Range, position.y * 0.5f, Range);
        return Physics.OverlapBoxNonAlloc(position,  detectorSize, result, Quaternion.identity, areaMask);
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
            float status = 0;
            status = motivationData.HasReflectMyStatus ? 
                character.StatusAbility.GetStatusValue(motivationData.CurrentStatName) : 
                orOther.StatusAbility.GetStatusValue(motivationData.CurrentStatName);
            
        }
    }

    public void ApplyMotivationStatus(Character character, Character enemy, MotivationStatusInfo motivationStatusInfo)
    {
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
            _ => throw new ArgumentOutOfRangeException(
                $"{motivationStatusInfo.CalculationType} | {motivationStatusInfo.MotivatedValueUnitType} not implement yet.")
        };
        
        switch (motivationStatusInfo.ApplyTargetType)
        {
            case ApplyTargetType.Player:
                character.StatusAbility.Ability.MotivationStatus.SetBaseValue(statusName, motivatedValue);
                break;
            case ApplyTargetType.Enemy:
                enemy.StatusAbility.Ability.MotivationStatus.SetBaseValue(statusName, motivatedValue);
                break;
        }
    }

    public void ApplyMotivationStatus(Character[] ourTeam, Character[] enemies, MotivationStatusInfo motivationStatusInfo)
    {
        int index = motivationStatusInfo.HasReflectMyStatus ? 
            character.StatusAbility.Ability.AllStatusInfos.GetStatusIndex(motivationStatusInfo.CurrentStatName) :
            enemy.StatusAbility.Ability.AllStatusInfos.GetStatusIndex(motivationStatusInfo.CurrentStatName);
        
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
            _ => throw new ArgumentOutOfRangeException(
                $"{motivationStatusInfo.CalculationType} | {motivationStatusInfo.MotivatedValueUnitType} not implement yet.")
        };
        
        if (motivationStatusInfo.ApplyTargetType is ApplyTargetType.Player)
        {
            character.StatusAbility.Ability.MotivationStatus.SetBaseValue(motivationStatusInfo.CurrentStatName, motivatedValue);
        }
        
        if (motivationStatusInfo.ApplyTargetType is ApplyTargetType.Enemy)
        {
            enemy.StatusAbility.Ability.MotivationStatus.SetBaseValue(motivationStatusInfo.CurrentStatName, motivatedValue);
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
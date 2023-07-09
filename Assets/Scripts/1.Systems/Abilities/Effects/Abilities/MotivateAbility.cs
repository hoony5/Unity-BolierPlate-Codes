using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MotivateAbility : Effect, IMotivatedAbility
{
    private readonly string MotivationStatusName = "MotivationStatus";
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public MotivationInfo MotivationInfo { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public void SetMotivationActive(Character character, Character orOther)
    {
        if(orOther is null) return;
        if(MotivationInfo.MotivationStatusInfos is null) return;
        if(MotivationInfo.MotivationStatusInfos.Count == 0) return;
    
        foreach (MotivationStatusInfo motivationData in MotivationInfo.MotivationStatusInfos.Where(IsValidMotivationData))
        {
            motivationData.MotivationActive = DetermineMotivationActive(character, orOther, motivationData);

            if (!motivationData.MotivationActive) continue;
        
            ApplyMotivationStatus(character, orOther, motivationData);
        }
    }

    private bool IsValidMotivationData(MotivationStatusInfo motivationData)
    {
        return motivationData.MotivationComparerType is not ComparerType.None 
               && motivationData.ApplyTargetType is not ApplyTargetType.None;
    }

    private bool DetermineMotivationActive(Character character, Character orOther, MotivationStatusInfo motivationData)
    {
        return motivationData.MotivationComparerType switch
        {
            ComparerType.Equal => IsMotivatedWhenApproximately(character, orOther),
            ComparerType.GreaterOrEqual => IsMotivatedWhenLess(character, orOther),
            ComparerType.LessOrEqual => IsMotivatedWhenGreater(character, orOther),
            _ => false
        };
    }


    private void AddMotivationStatus(Character character, MotivationStatusInfo motivationStatusInfo,
        string statusName, float value)
    {
        if (!character.TryGetStatusAbility(MotivationStatusName, out StatusBaseAbility statusBaseAbility))
            return;

        switch (motivationStatusInfo.CalculationType)
        {
            case CalculationType.None:
            case CalculationType.Equalize:
                statusBaseAbility.SetBaseValue(statusName, value);
                break;
            case CalculationType.Add:
            case CalculationType.Multiply:
                statusBaseAbility.AddBaseValue(statusName, value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void RemoveMotivationStatus(Character character, MotivationStatusInfo motivationStatusInfo, string statusName)
    {
        if (!character.TryGetStatusAbility(MotivationStatusName, out StatusBaseAbility statusBaseAbility))
            return;
        
        switch (motivationStatusInfo.CalculationType)
        {
            case CalculationType.None:
            case CalculationType.Equalize:
                statusBaseAbility.SetBaseValue(statusName, -motivationStatusInfo.PreviousValue);
                motivationStatusInfo.PreviousValue = 0;
                break;
            case CalculationType.Add:
                statusBaseAbility.AddBaseValue(statusName, -motivationStatusInfo.AddedValue);
                motivationStatusInfo.AddedValue = 0;
                break;
            case CalculationType.Multiply:
                statusBaseAbility.AddBaseValue(statusName, -motivationStatusInfo.MultipliedValue);
                motivationStatusInfo.MultipliedValue = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public void ApplyMotivationStatus(Character character, Character enemy, MotivationStatusInfo motivationStatusInfo)
    {
        //Calculate 
        string statusName = motivationStatusInfo.HasReflectMaxStatus ?
            motivationStatusInfo.MaxStatName :
            motivationStatusInfo.CurrentStatName;
        
        if (!character.TryGetStatusAbility(MotivationStatusName, out StatusBaseAbility playerAbility))
            return;
        
        if (!enemy.TryGetStatusAbility(MotivationStatusName, out StatusBaseAbility enemyAbility))
            return;
        
        float status = motivationStatusInfo.HasReflectMyStatus ? 
            playerAbility.GetBaseValue(statusName) :
            enemyAbility.GetBaseValue(statusName);

        float motivatedValue = motivationStatusInfo.CalculationType switch
        {
            CalculationType.None => 0,
            CalculationType.Equalize when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Numeric 
                => motivationStatusInfo.MotivatedValue,
            CalculationType.Equalize when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Percentage 
                => status * 0.01f * motivationStatusInfo.MotivatedValue,
            CalculationType.Add when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Numeric 
                => status + motivationStatusInfo.MotivatedValue,
            CalculationType.Add when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Percentage 
                => status * 0.01f * motivationStatusInfo.MotivatedValue,
            CalculationType.Multiply when motivationStatusInfo.MotivatedValueUnitType is DataUnitType.Numeric 
                => status * motivationStatusInfo.MotivatedValue,
            _ => throw new ArgumentOutOfRangeException(@$"
{motivationStatusInfo.CalculationType} |
{motivationStatusInfo.MotivatedValueUnitType} not implement yet.")
        } 
        // if stackable, multiply stackCount
        * (IsStackable ? StackCount : 1); 
        
        //override previousValue to new value
        switch (motivationStatusInfo.CalculationType)
        {
            case CalculationType.None:
                break;
            case CalculationType.Equalize:
                motivationStatusInfo.PreviousValue = motivatedValue;
                break;
            case CalculationType.Add:
                motivationStatusInfo.AddedValue = motivatedValue;
                break;
            case CalculationType.Multiply:
                motivationStatusInfo.MultipliedValue = motivatedValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        switch (motivationStatusInfo.ApplyTargetType)
        {
            case ApplyTargetType.Player:
            case ApplyTargetType.PlayerTeam:
            case ApplyTargetType.RandomPlayerTeam:
                AddMotivationStatus(character, motivationStatusInfo, statusName, motivatedValue);
                break;
            case ApplyTargetType.Enemy:
            case ApplyTargetType.EnemyTeam:
            case ApplyTargetType.RandomEnemyTeam:
                AddMotivationStatus(enemy, motivationStatusInfo, statusName, motivatedValue);
                break;
            case ApplyTargetType.RandomAll:
            case ApplyTargetType.All:
                AddMotivationStatus(character, motivationStatusInfo, statusName, motivatedValue);
                AddMotivationStatus(enemy, motivationStatusInfo, statusName, motivatedValue);
                break;
        }
    }

    public void ResetStatus(Character character, MotivationStatusInfo motivationStatusInfo)
    {
        RemoveMotivationStatus(character, motivationStatusInfo,
            string.IsNullOrEmpty(motivationStatusInfo.MaxStatName)
                ? motivationStatusInfo.CurrentStatName
                : motivationStatusInfo.MaxStatName);
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
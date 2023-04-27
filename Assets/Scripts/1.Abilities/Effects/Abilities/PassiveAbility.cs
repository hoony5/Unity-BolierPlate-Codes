using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveAbility : Effect, IPassiveAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    private void AddSkillStatus(Character character, EffectAbilityStat stat,
        string statusName, float value)
    {
        switch (stat.CalculationType)
        {
            case CalculationType.None:
            case CalculationType.Equalize:
                character.StatusAbility.Ability.SkillStatus.SetBaseValue(statusName, value);
                break;
            case CalculationType.Add:
            case CalculationType.Multiply:
                character.StatusAbility.Ability.SkillStatus.AddBaseValue(statusName, value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void RemoveSkillStatus(Character character, EffectAbilityStat stat, string statusName)
    {
        switch (stat.CalculationType)
        {
            case CalculationType.None:
            case CalculationType.Equalize:
                character.StatusAbility.Ability.SkillStatus.SetBaseValue(statusName, stat.PreviousValue);
                stat.PreviousValue = 0;
                break;
            case CalculationType.Add:
                character.StatusAbility.Ability.SkillStatus.AddBaseValue(statusName, -stat.AddedValue);
                stat.AddedValue = 0;
                break;
            case CalculationType.Multiply:
                character.StatusAbility.Ability.SkillStatus.AddBaseValue(statusName, -stat.MultipliedValue);
                stat.MultipliedValue = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public void CalculateTeamStatus(Character character, EffectAbilityStat stat)
    {
        float appliedValue = stat.Value * (IsStackable ? StackCount : 1);
        int index = character.StatusAbility.Ability.AllStatusInfos.GetStatusIndex(stat.RawName);
        float status = character.StatusAbility.Ability.SkillStatus.GetStatuses()[index].Value;
        float modifierStatus = stat.CalculationType switch
        {
            CalculationType.None => 0,
            CalculationType.Equalize when stat.DataUnitType is DataUnitType.Numeric 
                => appliedValue,
            CalculationType.Equalize when stat.DataUnitType is DataUnitType.Percentage 
                => status * 0.01f * appliedValue,
            CalculationType.Add when stat.DataUnitType is DataUnitType.Numeric 
                => status + appliedValue,
            CalculationType.Add when stat.DataUnitType is DataUnitType.Percentage 
                => status * 0.01f * appliedValue,
            CalculationType.Multiply 
                => status * appliedValue,
            _ => throw new ArgumentOutOfRangeException()
        };
        AddSkillStatus(character, stat, stat.RawName, modifierStatus);
    }

    public void ResetStatus(Character character, EffectAbilityStat stat)
    {
        RemoveSkillStatus(character, stat, stat.RawName);
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

   public void UpdateForTeam(Character[] team, EffectAbilityStat stat , ApplyTargetType targetType)
    {
        if(team is null || team.Length == 0) return;
        
        bool lengthValidation = team.Length >= ApplyTargetCount;
        int teamLength = !lengthValidation ? team.Length : ApplyTargetCount;
        
        for (var index = 0; index < teamLength; index++)
        {
            Character member = null;
            if (targetType is ApplyTargetType.RandomAll or ApplyTargetType.RandomEnemyTeam
                or ApplyTargetType.RandomPlayerTeam)
            {
                int seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                UnityEngine.Random.InitState(seed);
                int randomMemberIndex = UnityEngine.Random.Range(0, teamLength);
                member = team[randomMemberIndex];
                CalculateTeamStatus(member, stat);
                continue;
            }
            member = team[index];
            CalculateTeamStatus(member, stat);
        }
    }
    public void UpdateAbility(Character[] ourTeam, Character[] enemyTeam)
    {
        for (var x = 0; x < EffectAbilities.Count; x++)
        {
            EffectAbilityInfo effectAbilityInfo = EffectAbilities[x];
            for (var y = 0; y < effectAbilityInfo.abtilityStats.Count; y++)
            {
                EffectAbilityStat stat = effectAbilityInfo.abtilityStats[y];
                switch (stat.ApplyTargetType)
                {
                    case ApplyTargetType.None:
                        break;
                    case ApplyTargetType.RandomPlayerTeam:
                    case ApplyTargetType.PlayerTeam:
                        UpdateForTeam(ourTeam, stat, ApplyTargetType);
                        break;
                    case ApplyTargetType.RandomEnemyTeam:
                    case ApplyTargetType.EnemyTeam:
                        UpdateForTeam(enemyTeam, stat, ApplyTargetType);
                        break;
                    case ApplyTargetType.RandomAll:
                    case ApplyTargetType.All:
                        UpdateForTeam(ourTeam, stat, ApplyTargetType);
                        UpdateForTeam(enemyTeam, stat, ApplyTargetType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public void UpdateAbility(Character player, Character enemy)
    { 
        for (var x = 0; x < EffectAbilities.Count; x++)
        {
            EffectAbilityInfo effectAbilityInfo = EffectAbilities[x];
            for (var y = 0; y < effectAbilityInfo.abtilityStats.Count; y++)
            {
                EffectAbilityStat stat = effectAbilityInfo.abtilityStats[y];
                switch (stat.ApplyTargetType)
                {
                    case ApplyTargetType.None:
                        break;
                    case ApplyTargetType.Player:
                        CalculateTeamStatus(player, stat);
                        break;
                    case ApplyTargetType.Enemy:
                        CalculateTeamStatus(enemy, stat);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}
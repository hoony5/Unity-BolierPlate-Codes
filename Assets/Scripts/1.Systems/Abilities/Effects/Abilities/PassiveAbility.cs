using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PassiveAbility : Effect, IPassiveAbility
{
    private readonly string SkillStatusName = "SkillStatus";
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    private delegate void ModifyValue(StatusBaseAbility statusBaseAbility, string statusName, float value);

    private Dictionary<CalculationType, ModifyValue> valueModifiers = new Dictionary<CalculationType, ModifyValue>
    {
        { CalculationType.None, (statusBaseAbility, statusName, value) => {} },
        { CalculationType.Equalize, (statusBaseAbility, statusName, value) => statusBaseAbility.SetBaseValue(statusName, value) },
        { CalculationType.Add, (statusBaseAbility, statusName, value) => statusBaseAbility.AddBaseValue(statusName, value) },
        { CalculationType.Multiply, (statusBaseAbility, statusName, value) => statusBaseAbility.AddBaseValue(statusName, value) },
    };

    private void ModifySkillStatus(Character character, EffectAbilityStat stat, string statusName, float value, bool isAdding)
    {
        if(!character.TryGetStatusAbility(SkillStatusName, out StatusBaseAbility statusBaseAbility)) 
            return;
    
        if (!valueModifiers.ContainsKey(stat.CalculationType))
            throw new ArgumentOutOfRangeException();

        float adjustedValue = isAdding ? value : -value;
        valueModifiers[stat.CalculationType].Invoke(statusBaseAbility, statusName, adjustedValue);
    }

    private void ResetAddedValue(EffectAbilityStat stat)
    {
        switch (stat.CalculationType)
        {
            case CalculationType.None:
            case CalculationType.Equalize:
            case CalculationType.Add:
                stat.PreviousValue = 0;
                break;
            case CalculationType.Multiply:
                stat.MultipliedValue = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void CalculateTeamStatus(Character character, EffectAbilityStat stat)
    {
        float appliedValue = stat.Value * (IsStackable ? StackCount : 1);

        if(!character.TryGetStatusAbility(SkillStatusName, out StatusBaseAbility statusBaseAbility)) 
            return;

        float status = statusBaseAbility.GetBaseValue(stat.RawName);
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

        ModifySkillStatus(character, stat, stat.RawName, modifierStatus, true);
    }

    public void ResetStatus(Character character, EffectAbilityStat stat)
    {
        ModifySkillStatus(character, stat, stat.RawName, stat.PreviousValue, false);
        ResetAddedValue(stat);
    }

    public void AddStackCount()
    {
        if (!IsStackable) return;
        StackCount = Math.Min(StackCount + 1, MaxStackCount);
    }

    public void SubtractStackCount()
    {
        if (!IsStackable) return;
        StackCount = Math.Max(StackCount - 1, 1);
    }

    public void ResetStackCount()
    {
        if (!IsStackable) return;
        StackCount = 1;
    }

    public void UpdateForTeam(Character[] team, EffectAbilityStat stat, ApplyTargetType targetType)
    {
        if (team is null || team.Length == 0) return;
        
        int teamLength = Math.Min(team.Length, ApplyTargetCount);

        for (var index = 0; index < teamLength; index++)
        {
            Character member = null;

            if (targetType is ApplyTargetType.RandomAll or ApplyTargetType.RandomEnemyTeam or ApplyTargetType.RandomPlayerTeam)
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
        foreach (EffectAbilityStat stat in EffectAbilities.SelectMany(effectAbilityInfo => effectAbilityInfo.abtilityStats))
        {
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

    public void UpdateAbility(Character player, Character enemy)
    {
        foreach (EffectAbilityStat stat in EffectAbilities.SelectMany(effectAbilityInfo => effectAbilityInfo.abtilityStats))
        {
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

    public bool HitTheChance(float tryChance)
    {
        return tryChance <= Chance;
    }
}

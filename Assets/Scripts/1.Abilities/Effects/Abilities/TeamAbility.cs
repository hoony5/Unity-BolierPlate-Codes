using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamAbility : Effect, ITeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public void CalculateTeamStatus(Character character, EffectAbilityStat stat)
    {
        List<StatusItemInfo> stats = BuffOrDebuff ? 
            character.StatusAbility.Ability.BuffStat.GetStatuses() : 
            character.StatusAbility.Ability.DebuffStat.GetStatuses();
        
        switch (stat.CalculationType)
        {
            case CalculationType.None:
                break;
            case CalculationType.Equalize:
                character.StatusAbility.SetBaseValue(stats, stat.StatRawName, stat.Value);
                break;
            case CalculationType.Add:
                character.StatusAbility.AddBaseValue(stats, stat.StatRawName, stat.Value);
                break;
            /// ex. stat.Value is 1.2
            case CalculationType.Multiply:
                character.StatusAbility.MultiplyBaseValue(stats, stat.StatRawName, stat.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stat.CalculationType), stat.CalculationType, null);
        }
    }

    public void UpdateForTeam(Character[] team, EffectAbilityStat stat)
    {
        if(team is null || team.Length == 0) return;
        
        bool lengthValidation = team.Length >= ApplyTargetCount;
        int teamLength;
        
        teamLength = !lengthValidation ? team.Length : ApplyTargetCount;
        
        for (var index = 0; index < teamLength; index++)
        {
            var member = team[index];
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
                    case ApplyTargetType.PlayerTeam:
                        UpdateForTeam(ourTeam, stat);
                        break;
                    case ApplyTargetType.EnemyTeam:
                        UpdateForTeam(enemyTeam, stat);
                        break;
                    case ApplyTargetType.All:
                        UpdateForTeam(ourTeam, stat);
                        UpdateForTeam(enemyTeam, stat);
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
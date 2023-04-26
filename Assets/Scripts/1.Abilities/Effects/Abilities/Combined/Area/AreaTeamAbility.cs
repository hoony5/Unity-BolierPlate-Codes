using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaTeamAbility : Effect, IAreaTeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public bool IsPassive { get; set; }
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

    public void CalculateTeamStatus(Character character, EffectAbilityStat stat)
    {
        float appliedValue = stat.Value * (IsStackable ? StackCount : 1);
        int index = character.StatusAbility.Ability.AllStatusInfos.GetStatusIndex(stat.RawName);
        
        stat.PreviousValue = BuffOrDebuff ? 
            character.StatusAbility.Ability.BuffStat.GetStatuses()[index].Value : 
            character.StatusAbility.Ability.DebuffStat.GetStatuses()[index].Value;
        
        switch (stat.CalculationType)
        {
            case CalculationType.None:
                break;
            case CalculationType.Equalize:
                if(BuffOrDebuff)
                    character.StatusAbility.Ability.BuffStat.SetBaseValue(stat.RawName, appliedValue);
                else
                    character.StatusAbility.Ability.DebuffStat.SetBaseValue(stat.RawName, appliedValue);
                break;
            case CalculationType.Add:
                if(BuffOrDebuff)
                    character.StatusAbility.Ability.BuffStat.AddBaseValue(stat.RawName, appliedValue);
                else
                    character.StatusAbility.Ability.DebuffStat.AddBaseValue(stat.RawName, appliedValue);
                break;
            /// ex. stat.Value is 1.2
            case CalculationType.Multiply:
                if(BuffOrDebuff)
                    character.StatusAbility.Ability.BuffStat.MultiplyBaseValue(stat.RawName, appliedValue);
                else
                    character.StatusAbility.Ability.DebuffStat.MultiplyBaseValue(stat.RawName, appliedValue);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stat.CalculationType), stat.CalculationType, null);
        }
    }

    public void ResetStatus(Character character, EffectAbilityStat stat)
    {
        character.StatusAbility.Ability.BuffStat.SetBaseValue(stat.RawName, stat.PreviousValue);
    }
    public void UpdateForTeam(Character[] team, EffectAbilityStat stat , ApplyTargetType targetType)
    {
        if(team is null || team.Length == 0) return;
        
        bool lengthValidation = team.Length >= ApplyTargetCount;
        int teamLength;
        
        teamLength = !lengthValidation ? team.Length : ApplyTargetCount;
        
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
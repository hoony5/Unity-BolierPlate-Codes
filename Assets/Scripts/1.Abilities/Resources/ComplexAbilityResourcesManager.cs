using System;
using System.Collections.Generic;
using UnityEngine;

public class ComplexAbilityResourcesManager : MonoBehaviour
{
    public AllEffectAbilities allEffectAbilities;
    public AbilityResourceInfo[] abilityResourceInfos;
    private readonly int EffectAbilitiesCapacity = 5;
    /// Values is RowDatas from CSV or Excel file
    public List<Effect> LoadAllComplexAbilities()
    {
        List<Effect> result = new List<Effect>(128);
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            switch (info.typeName)
            {
                default:
                    Debug.Log($"{info.typeName} is not supported on the this script");
                    break;
                case "AreaDurationAimedAbility":
                    result.AddRange(LoadAreaDurationAimedAbility(info.GetAbilityDatas()));
                    break;
                case "AreaDurationAimedMotivatedAbility":
                    result.AddRange(LoadAreaDurationAimedMotivatedAbility(info.GetAbilityDatas()));
                    break;
                case "AreaDurationTeamAbility":
                    result.AddRange(LoadAreaDurationTeamAbility(info.GetAbilityDatas()));
                    break;
                case "CastAreaDurationAimedAbility":
                    result.AddRange(LoadCastAreaDurationAimedAbility(info.GetAbilityDatas()));
                    break;
                case "CastAreaDurationAimedMotivatedAbility":
                    result.AddRange(LoadCastAreaDurationAimedMotivatedAbility(info.GetAbilityDatas()));
                    break;
                case "CastAreaDurationTeamAbility":
                    result.AddRange(LoadCastAreaDurationTeamAbility(info.GetAbilityDatas()));
                    break;
                case "CastMotivatedDurationAbility":
                    result.AddRange(LoadCastMotivatedDurationAbility(info.GetAbilityDatas()));
                    break;
            }
        }

        return result;
    }
    public List<AreaDurationAimedStatusAbility> LoadAreaDurationAimedAbility(List<string[]> values)
    {
        List<AreaDurationAimedStatusAbility> areaDurationAimedAbilities = new List<AreaDurationAimedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaDurationAimedStatusAbility effect = new AreaDurationAimedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[4], out float range) ? range : 1,
                SearchState = rowDatas[5],
                SearchTag = rowDatas[6],
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[7], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[8], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[9], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[10],
            };
            if (areaDurationAimedAbilities.Contains(effect)) continue;
            areaDurationAimedAbilities.Add(effect);
        }

        return areaDurationAimedAbilities;
    }
    public List<AreaDurationAimedMotivatedStatusAbility> LoadAreaDurationAimedMotivatedAbility(List<string[]> values)
    {
        List<AreaDurationAimedMotivatedStatusAbility> areaDurationAimedMotivatedAbilities = new List<AreaDurationAimedMotivatedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaDurationAimedMotivatedStatusAbility effect = new AreaDurationAimedMotivatedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[4], out float range) ? range : 1,
                Motivation = float.TryParse(rowDatas[5], out float motivation) ? motivation : -1,
                MotivationStat = rowDatas[6],
                SearchState = rowDatas[7],
                SearchTag = rowDatas[8],
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[9], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[10], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[11], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[12],
            };
            if (areaDurationAimedMotivatedAbilities.Contains(effect)) continue;
            areaDurationAimedMotivatedAbilities.Add(effect);
        }

        return areaDurationAimedMotivatedAbilities;
    }
    public List<AreaDurationTeamAbility> LoadAreaDurationTeamAbility(List<string[]> values)
    {
        List<AreaDurationTeamAbility> areaDurationTeamAbilities = new List<AreaDurationTeamAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaDurationTeamAbility effect = new AreaDurationTeamAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[4], out float range) ? range : 1 ,
                BuffOrDebuff = bool.TryParse(rowDatas[5], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[6], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[7], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[8], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[9],
            };
            if (areaDurationTeamAbilities.Contains(effect)) continue;
            areaDurationTeamAbilities.Add(effect);
        }

        return areaDurationTeamAbilities;
    }
    public List<CastAreaDurationAimedStatusAbility> LoadCastAreaDurationAimedAbility(List<string[]> values)
    {
        List<CastAreaDurationAimedStatusAbility> castAreaDurationAimedAbilities = new List<CastAreaDurationAimedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastAreaDurationAimedStatusAbility effect = new CastAreaDurationAimedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[4], out float range) ? range : 1 ,
                SearchState = rowDatas[5],
                SearchTag = rowDatas[6],
                Threshold = float.TryParse(rowDatas[7], out float threshold) ? threshold : 0,
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[8], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[9], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[10], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[11],
            };
            if (castAreaDurationAimedAbilities.Contains(effect)) continue;
            castAreaDurationAimedAbilities.Add(effect);
        }

        return castAreaDurationAimedAbilities;
    }
    public List<CastAreaDurationAimedMotivatedStatusAbility> LoadCastAreaDurationAimedMotivatedAbility(List<string[]> values)
    {
        List<CastAreaDurationAimedMotivatedStatusAbility> castAreaDurationAimedMotivatedAbilities = new List<CastAreaDurationAimedMotivatedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastAreaDurationAimedMotivatedStatusAbility effect = new CastAreaDurationAimedMotivatedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[4], out float range) ? range : 1 ,
                Motivation = float.TryParse(rowDatas[5], out float motivation) ? motivation : -1,
                MotivationStat = rowDatas[6],
                SearchState = rowDatas[7],
                SearchTag = rowDatas[8],
                Threshold = float.TryParse(rowDatas[9], out float threshold) ? threshold : 0,
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[10], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[11], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[12], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[13],
            };
            if (castAreaDurationAimedMotivatedAbilities.Contains(effect)) continue;
            castAreaDurationAimedMotivatedAbilities.Add(effect);
        }

        return castAreaDurationAimedMotivatedAbilities;
    }
    public List<CastAreaDurationTeamAbility> LoadCastAreaDurationTeamAbility(List<string[]> values)
    {
        List<CastAreaDurationTeamAbility> castAreaDurationTeamAbilities = new List<CastAreaDurationTeamAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastAreaDurationTeamAbility effect = new CastAreaDurationTeamAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable, 
                StackCount = 1, 
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                BuffOrDebuff = bool.TryParse(rowDatas[3], out bool buffOrDebuff) && buffOrDebuff, 
                Duration = float.TryParse(rowDatas[4], out float duration) ? duration : 0, 
                Range = float.TryParse(rowDatas[5], out float range) ? range : 1 , 
                Threshold = float.TryParse(rowDatas[6], out float threshold) ? threshold : 0, 
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[7], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[8], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[9], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[10],
            };
            if (castAreaDurationTeamAbilities.Contains(effect)) continue;
            castAreaDurationTeamAbilities.Add(effect);
        }

        return castAreaDurationTeamAbilities;
    }
    public List<CastMotivatedDurationAbility> LoadCastMotivatedDurationAbility(List<string[]> values)
    {
        List<CastMotivatedDurationAbility> castMotivatedDurationAbilities = new List<CastMotivatedDurationAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastMotivatedDurationAbility effect = new CastMotivatedDurationAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                Threshold = float.TryParse(rowDatas[4], out float threshold) ? threshold : 0,
                Motivation = float.TryParse(rowDatas[5], out float motivation) ? motivation : -1,
                MotivationStat = rowDatas[6],
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[7], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[8], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[9], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[10],
            };
            if (castMotivatedDurationAbilities.Contains(effect)) continue;
            castMotivatedDurationAbilities.Add(effect);
        }

        return castMotivatedDurationAbilities;
    }
}
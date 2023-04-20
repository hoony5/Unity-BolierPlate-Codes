
using System;
using System.Collections.Generic;
using UnityEngine;

public class CombinedAbilityResourcesManager : MonoBehaviour
{
    public AllEffectAbilities allEffectAbilities;
    public AbilityResourceInfo[] abilityResourceInfos;
    private readonly int EffectAbilitiesCapacity = 5;

    /// Values is RowDatas from CSV or Excel file
    
    public List<Effect> LoadAllCombineAbilities()
    {
        List<Effect> result = new List<Effect>(128);
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            switch (info.typeName)
            {
                default:
                    Debug.Log($"{info.typeName} is not supported on the this script");
                    break;
                case "AreaAimedAbility":
                    result.AddRange(LoadAreaAimedAbility(info.GetAbilityDatas()));
                    break;
                case "AreaCastAbility":
                    result.AddRange(LoadAreaCastAbility(info.GetAbilityDatas()));
                    break;
                case "AreaDurationAbility":
                    result.AddRange(LoadAreaDurationAbility(info.GetAbilityDatas()));
                    break;
                case "AreaMotivatedAbility":
                    result.AddRange(LoadAreaMotivatedAbility(info.GetAbilityDatas()));
                    break;
                case "AreaTeamAbility":
                    result.AddRange(LoadAreaTeamAbility(info.GetAbilityDatas()));
                    break;
                case "CastAimedAbility":
                    result.AddRange(LoadCastAimedAbility(info.GetAbilityDatas()));
                    break;
                case "CastDurationAbility":
                    result.AddRange(LoadCastDurationAbility(info.GetAbilityDatas()));
                    break;
                case "CastTeamAbility":
                    result.AddRange(LoadCastTeamAbility(info.GetAbilityDatas()));
                    break;
                case "DurationAimedAbility":
                    result.AddRange(LoadDurationAimedAbility(info.GetAbilityDatas()));
                    break;
                case "DurationMotivatedAbility":
                    result.AddRange(LoadDurationMotivatedAbility(info.GetAbilityDatas()));
                    break;
                case "DurationTeamAbility":
                    result.AddRange(LoadDurationTeamAbility(info.GetAbilityDatas()));
                    break;
            }
        }

        return result;
    }
    public List<AreaAimedStatusAbility> LoadAreaAimedAbility(List<string[]> values)
    {
        List<AreaAimedStatusAbility> areaAimedAbilities = new List<AreaAimedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
                
            AreaAimedStatusAbility effect = new AreaAimedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                SearchState = rowDatas[3],
                SearchTag = rowDatas[4],
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[5], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[6], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[7], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[8],
            };
            if (areaAimedAbilities.Contains(effect)) continue;
            areaAimedAbilities.Add(effect);
        }

        return areaAimedAbilities;
    }

    public List<AreaCastAbility> LoadAreaCastAbility(List<string[]> values)
    {
        List<AreaCastAbility> areaCastAbilities = new List<AreaCastAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaCastAbility effect = new AreaCastAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Threshold = float.TryParse(rowDatas[3], out float threshold) ? threshold : 0,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
            };
            if (areaCastAbilities.Contains(effect)) continue;
            areaCastAbilities.Add(effect);
        }

        return areaCastAbilities;
    }

    public List<AreaDurationAbility> LoadAreaDurationAbility(List<string[]> values)
    {
        List<AreaDurationAbility> areaDurationAbilities = new List<AreaDurationAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaDurationAbility effect = new AreaDurationAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
            };
            if (areaDurationAbilities.Contains(effect)) continue;
            areaDurationAbilities.Add(effect);
        }

        return areaDurationAbilities;
    }

    public List<AreaMotivatedAbility> LoadAreaMotivatedAbility(List<string[]> values)
    {
        List<AreaMotivatedAbility> areaMotivatedAbilities = new List<AreaMotivatedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaMotivatedAbility effect = new AreaMotivatedAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Motivation = float.TryParse(rowDatas[3], out float motivation) ? motivation : -1,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7]
            };
            if (areaMotivatedAbilities.Contains(effect)) continue;
            areaMotivatedAbilities.Add(effect);
        }

        return areaMotivatedAbilities;
    }

    public List<AreaTeamAbility> LoadAreaTeamAbility(List<string[]> values)
    {
        List<AreaTeamAbility> areaTeamAbilities = new List<AreaTeamAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaTeamAbility effect = new AreaTeamAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[1], out float range) ? range : 1,
                BuffOrDebuff = bool.TryParse(rowDatas[2], out bool buffOrDebuff) && buffOrDebuff,
                IsPassive = bool.TryParse(rowDatas[3], out bool isPassive) && isPassive,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
            };
            if (areaTeamAbilities.Contains(effect)) continue;
            areaTeamAbilities.Add(effect);
        }

        return areaTeamAbilities;
    }

    public List<CastAimedStatusAbility> LoadCastAimedAbility(List<string[]> values)
    {
        List<CastAimedStatusAbility> castAimedAbilities = new List<CastAimedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastAimedStatusAbility effect = new CastAimedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Threshold = float.TryParse(rowDatas[1], out float threshold) ? threshold : 0,
                SearchState = rowDatas[2],
                SearchTag = rowDatas[3],
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
            };
            if (castAimedAbilities.Contains(effect)) continue;
            castAimedAbilities.Add(effect);
        }

        return castAimedAbilities;
    }

    public List<CastDurationAbility> LoadCastDurationAbility(List<string[]> values)
    {
        List<CastDurationAbility> castDurationAbility = new List<CastDurationAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastDurationAbility effect = new CastDurationAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Threshold = float.TryParse(rowDatas[3], out float threshold) ? threshold : 0,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
            };
            if (castDurationAbility.Contains(effect)) continue;
            castDurationAbility.Add(effect);
        }

        return castDurationAbility;
    }

    public List<CastTeamAbility> LoadCastTeamAbility(List<string[]> values)
    {
        List<CastTeamAbility> castTeamAbilities = new List<CastTeamAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastTeamAbility effect = new CastTeamAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                BuffOrDebuff = bool.TryParse(rowDatas[1], out bool buffOrDebuff) && buffOrDebuff,
                Threshold = float.TryParse(rowDatas[2], out float threshold) ? threshold : 0,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[3], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[4], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[5], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[6],
            };
            if (castTeamAbilities.Contains(effect)) continue;
            castTeamAbilities.Add(effect);
        }

        return castTeamAbilities;
    }

    public List<DurationAimedStatusAbility> LoadDurationAimedAbility(List<string[]> values)
    {
        List<DurationAimedStatusAbility> durationAimedAbilities = new List<DurationAimedStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            DurationAimedStatusAbility effect = new DurationAimedStatusAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                SearchState = rowDatas[3],
                SearchTag = rowDatas[4],
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity),
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[5], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[6], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[7], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[8],
            };
            if (durationAimedAbilities.Contains(effect)) continue;
            durationAimedAbilities.Add(effect);
        }

        return durationAimedAbilities;
    }

    public List<DurationMotivatedAbility> LoadDurationMotivatedAbility(List<string[]> values)
    {
        List<DurationMotivatedAbility> durationMotivatedAbilities = new List<DurationMotivatedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            DurationMotivatedAbility effect = new DurationMotivatedAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[1], out float duration) ? duration : 0,
                Motivation = float.TryParse(rowDatas[2], out float motivation) ? motivation : -1,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[3], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[4], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[5], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[6],
            };
            if (durationMotivatedAbilities.Contains(effect)) continue;
            durationMotivatedAbilities.Add(effect);
        }

        return durationMotivatedAbilities;
    }

    public List<DurationTeamAbility> LoadDurationTeamAbility(List<string[]> values)
    {
        List<DurationTeamAbility> durationTeamAbilities = new List<DurationTeamAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            DurationTeamAbility effect = new DurationTeamAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                BuffOrDebuff = bool.TryParse(rowDatas[3], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7]
            };
            if (durationTeamAbilities.Contains(effect)) continue;
            durationTeamAbilities.Add(effect);
        }

        return durationTeamAbilities;
    }
}
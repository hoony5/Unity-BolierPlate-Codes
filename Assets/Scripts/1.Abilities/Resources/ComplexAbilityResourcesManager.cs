using System.Collections.Generic;
using UnityEngine;

public class ComplexAbilityResourcesManager : MonoBehaviour
{
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
    public List<AreaDurationAimedAbility> LoadAreaDurationAimedAbility(List<string[]> values)
    {
        List<AreaDurationAimedAbility> areaDurationAimedAbilities = new List<AreaDurationAimedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            AreaDurationAimedAbility effect = new AreaDurationAimedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[3], out float range) ? range : 1,
                SearchState = rowDatas[4],
                SearchTag = rowDatas[5],
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[6],
            };
            if (areaDurationAimedAbilities.Contains(effect)) continue;
            areaDurationAimedAbilities.Add(effect);
        }

        return areaDurationAimedAbilities;
    }
    public List<AreaDurationAimedMotivatedAbility> LoadAreaDurationAimedMotivatedAbility(List<string[]> values)
    {
        List<AreaDurationAimedMotivatedAbility> areaDurationAimedMotivatedAbilities = new List<AreaDurationAimedMotivatedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            AreaDurationAimedMotivatedAbility effect = new AreaDurationAimedMotivatedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[3], out float range) ? range : 1,
                SearchState = rowDatas[4],
                SearchTag = rowDatas[5],
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[6],
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
            AreaDurationTeamAbility effect = new AreaDurationTeamAbility()
            {
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[1], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1 ,
                BuffOrDebuff = bool.TryParse(rowDatas[3], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
            };
            if (areaDurationTeamAbilities.Contains(effect)) continue;
            areaDurationTeamAbilities.Add(effect);
        }

        return areaDurationTeamAbilities;
    }
    public List<CastAreaDurationAimedAbility> LoadCastAreaDurationAimedAbility(List<string[]> values)
    {
        List<CastAreaDurationAimedAbility> castAreaDurationAimedAbilities = new List<CastAreaDurationAimedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            CastAreaDurationAimedAbility effect = new CastAreaDurationAimedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[3], out float range) ? range : 1 ,
                SearchState = rowDatas[4],
                SearchTag = rowDatas[5],
                Threshold = float.TryParse(rowDatas[6], out float threshold) ? threshold : 0,
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[7],
            };
            if (castAreaDurationAimedAbilities.Contains(effect)) continue;
            castAreaDurationAimedAbilities.Add(effect);
        }

        return castAreaDurationAimedAbilities;
    }
    public List<CastAreaDurationAimedMotivatedAbility> LoadCastAreaDurationAimedMotivatedAbility(List<string[]> values)
    {
        List<CastAreaDurationAimedMotivatedAbility> castAreaDurationAimedMotivatedAbilities = new List<CastAreaDurationAimedMotivatedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            CastAreaDurationAimedMotivatedAbility effect = new CastAreaDurationAimedMotivatedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Range = float.TryParse(rowDatas[3], out float range) ? range : 1 ,
                SearchState = rowDatas[4],
                SearchTag = rowDatas[5],
                Threshold = float.TryParse(rowDatas[6], out float threshold) ? threshold : 0,
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[7],
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
            CastAreaDurationTeamAbility effect = new CastAreaDurationTeamAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable, 
                StackCount = 1, 
                BuffOrDebuff = bool.TryParse(rowDatas[2], out bool buffOrDebuff) && buffOrDebuff, 
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0, 
                Range = float.TryParse(rowDatas[4], out float range) ? range : 1 , 
                Threshold = float.TryParse(rowDatas[5], out float threshold) ? threshold : 0, 
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[6],
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
            CastMotivatedDurationAbility effect = new CastMotivatedDurationAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Threshold = float.TryParse(rowDatas[3], out float threshold) ? threshold : 0,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
            };
            if (castMotivatedDurationAbilities.Contains(effect)) continue;
            castMotivatedDurationAbilities.Add(effect);
        }

        return castMotivatedDurationAbilities;
    }
}
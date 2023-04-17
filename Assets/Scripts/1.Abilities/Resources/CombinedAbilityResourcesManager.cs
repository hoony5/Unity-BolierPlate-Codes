
using System.Collections.Generic;
using UnityEngine;

public class CombinedAbilityResourcesManager : MonoBehaviour
{
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
                    result.AddRange(LoadAreaAimedAbility(info.GetInfo()));
                    break;
                case "AreaCastAbility":
                    result.AddRange(LoadAreaCastAbility(info.GetInfo()));
                    break;
                case "AreaDurationAbility":
                    result.AddRange(LoadAreaDurationAbility(info.GetInfo()));
                    break;
                case "AreaMotivatedAbility":
                    result.AddRange(LoadAreaMotivatedAbility(info.GetInfo()));
                    break;
                case "AreaTeamAbility":
                    result.AddRange(LoadAreaTeamAbility(info.GetInfo()));
                    break;
                case "CastAimedAbility":
                    result.AddRange(LoadCastAimedAbility(info.GetInfo()));
                    break;
                case "CastDurationAbility":
                    result.AddRange(LoadCastDurationAbility(info.GetInfo()));
                    break;
                case "CastTeamAbility":
                    result.AddRange(LoadCastTeamAbility(info.GetInfo()));
                    break;
                case "DurationAimedAbility":
                    result.AddRange(LoadDurationAimedAbility(info.GetInfo()));
                    break;
                case "DurationMotivatedAbility":
                    result.AddRange(LoadDurationMotivatedAbility(info.GetInfo()));
                    break;
                case "DurationTeamAbility":
                    result.AddRange(LoadDurationTeamAbility(info.GetInfo()));
                    break;
            }
        }

        return result;
    }
    public List<AreaAimedAbility> LoadAreaAimedAbility(List<string[]> values)
    {
        List<AreaAimedAbility> areaAimedAbilities = new List<AreaAimedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            AreaAimedAbility effect = new AreaAimedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                SearchState = rowDatas[3],
                SearchTag = rowDatas[4],
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[5],
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
            AreaCastAbility effect = new AreaCastAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Threshold = float.TryParse(rowDatas[3], out float threshold) ? threshold : 0,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
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
            AreaDurationAbility effect = new AreaDurationAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Duration = float.TryParse(rowDatas[3], out float duration) ? duration : 0,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
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
            AreaMotivatedAbility effect = new AreaMotivatedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[3]
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
            AreaTeamAbility effect = new AreaTeamAbility()
            {
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[1], out float range) ? range : 1,
                BuffOrDebuff = bool.TryParse(rowDatas[2], out bool buffOrDebuff) && buffOrDebuff,
                IsPassive = bool.TryParse(rowDatas[3], out bool isPassive) && isPassive,
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
            };
            if (areaTeamAbilities.Contains(effect)) continue;
            areaTeamAbilities.Add(effect);
        }

        return areaTeamAbilities;
    }

    public List<CastAimedAbility> LoadCastAimedAbility(List<string[]> values)
    {
        List<CastAimedAbility> castAimedAbilities = new List<CastAimedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            CastAimedAbility effect = new CastAimedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Threshold = float.TryParse(rowDatas[1], out float threshold) ? threshold : 0,
                SearchState = rowDatas[2],
                SearchTag = rowDatas[3],
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
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
            CastDurationAbility effect = new CastDurationAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                Threshold = float.TryParse(rowDatas[3], out float threshold) ? threshold : 0,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4],
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
            CastTeamAbility effect = new CastTeamAbility()
            {
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                BuffOrDebuff = bool.TryParse(rowDatas[1], out bool buffOrDebuff) && buffOrDebuff,
                Threshold = float.TryParse(rowDatas[2], out float threshold) ? threshold : 0,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[3],
            };
            if (castTeamAbilities.Contains(effect)) continue;
            castTeamAbilities.Add(effect);
        }

        return castTeamAbilities;
    }

    public List<DurationAimedAbility> LoadDurationAimedAbility(List<string[]> values)
    {
        List<DurationAimedAbility> durationAimedAbilities = new List<DurationAimedAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            DurationAimedAbility effect = new DurationAimedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float duration) ? duration : 0,
                SearchState = rowDatas[3],
                SearchTag = rowDatas[4],
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity),
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[5],
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
            DurationMotivatedAbility effect = new DurationMotivatedAbility()
            {
                IsStackable = bool.TryParse(rowDatas[0], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[1], out float duration) ? duration : 0,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[2],
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
            DurationTeamAbility effect = new DurationTeamAbility()
            {
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                BuffOrDebuff = bool.TryParse(rowDatas[3], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = new List<EffectAbility>(EffectAbilitiesCapacity),
                Description = rowDatas[4]
            };
            if (durationTeamAbilities.Contains(effect)) continue;
            durationTeamAbilities.Add(effect);
        }

        return durationTeamAbilities;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class OneAbilityResourcesManager : MonoBehaviour
{
    public AllEffectAbilities allEffectAbilities;
    public AbilityResourceInfo[] abilityResourceInfos;
    private readonly int EffectAbilitiesCapacity = 5;
    public List<Effect> LoadAllOneAbilities()
    {
        List<Effect> result = new List<Effect>(128);
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            switch (info.typeName)
            {
                default:
                    Debug.Log($"{info.typeName} is not supported on the this script");
                    break;
                case "AreaAbility":
                     result.AddRange(LoadAreaAbility(info.GetAbilityDatas()));
                    break;
                case "CastAbility":
                     result.AddRange(LoadCastAbility(info.GetAbilityDatas()));
                    break;
                case "DurationAbility":
                     result.AddRange(LoadDurationAbility(info.GetAbilityDatas()));
                    break;
                case "MotivateAbility":
                    result.AddRange(LoadMotivateAbility(info.GetAbilityDatas()));
                    break;
                case "PassiveAbility":
                    result.AddRange(LoadPassiveAbility(info.GetAbilityDatas()));
                    break;
                case "TeamAbility":
                    result.AddRange(LoadTeamAbility(info.GetAbilityDatas()));
                    break;
                case "SearchAbility":
                    result.AddRange(LoadSearchAbility(info.GetAbilityDatas()));
                    break;
            }
        }

        return result;
    }
    /// Values is RowDatas from CSV or Excel file
    public List<AreaAbility> LoadAreaAbility(List<string[]> values)
    {
        List<AreaAbility> areaAbilities = new List<AreaAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaAbility effect = new AreaAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                EffectAbilities = ability.abilityInfos,
                Range = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Description = rowDatas[3],
            };
            if (areaAbilities.Contains(effect)) continue;
            areaAbilities.Add(effect);
        }

        return areaAbilities;
    }
    public List<CastAbility> LoadCastAbility(List<string[]> values)
    {
        List<CastAbility> castAbilities = new List<CastAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastAbility effect = new CastAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                EffectAbilities = ability.abilityInfos,
                Threshold = float.TryParse(rowDatas[2], out float range) ? range : 1,
                Description = rowDatas[3],
            };
            if (castAbilities.Contains(effect)) continue;
            castAbilities.Add(effect);
        }

        return castAbilities;
    }
    public List<DurationAbility> LoadDurationAbility(List<string[]> values)
    {
        List<DurationAbility> durationAbilities = new List<DurationAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            DurationAbility effect = new DurationAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[2], out float range) ? range : 1,
                EffectAbilities = ability.abilityInfos,
                Description = rowDatas[3],
            };
            if (durationAbilities.Contains(effect)) continue;
            durationAbilities.Add(effect);
        }

        return durationAbilities;
    }
    public List<MotivateAbility> LoadMotivateAbility(List<string[]> values)
    {
        List<MotivateAbility> motivationAbilities = new List<MotivateAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            MotivateAbility effect = new MotivateAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                EffectAbilities = ability.abilityInfos,
                Description = rowDatas[2],
            };
            if (motivationAbilities.Contains(effect)) continue;
            motivationAbilities.Add(effect);
        }

        return motivationAbilities;
    }
    public List<PassiveAbility> LoadPassiveAbility(List<string[]> values)
    {
        List<PassiveAbility> passiveAbilities = new List<PassiveAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            PassiveAbility effect = new PassiveAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                BuffOrDebuff = bool.TryParse(rowDatas[2], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = ability.abilityInfos,
                Description = rowDatas[3],
            };
            if (passiveAbilities.Contains(effect)) continue;
            passiveAbilities.Add(effect);
        }

        return passiveAbilities;
    }
    public List<SearchAbility> LoadSearchAbility(List<string[]> values)
    {
        List<SearchAbility> searchAbilities = new List<SearchAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            SearchAbility effect = new SearchAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable, 
                StackCount = 1, 
                SearchState = rowDatas[2], 
                SearchTag = rowDatas[3], 
                SearchStats = new List<StatusItemInfo>(EffectAbilitiesCapacity), 
                EffectAbilities = ability.abilityInfos, 
                Description = rowDatas[4],
            };
            if (searchAbilities.Contains(effect)) continue;
            searchAbilities.Add(effect);
        }

        return searchAbilities;
    }
    public List<TeamAbility> LoadTeamAbility(List<string[]> values)
    {
        List<TeamAbility> teamAbilities = new List<TeamAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = allEffectAbilities.GetEffectAbility(rowDatas[0]);
            TeamAbility effect = new TeamAbility()
            {
                _effectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                BuffOrDebuff = bool.TryParse(rowDatas[2], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = ability.abilityInfos,
                Description = rowDatas[3],
            };
            if (teamAbilities.Contains(effect)) continue;
            teamAbilities.Add(effect);
        }

        return teamAbilities;
    }
}
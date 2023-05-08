using System;
using System.Collections.Generic;
using UnityEngine;

public class OneAbilityResourcesManager : MonoBehaviour
{
    [field:SerializeField] public EffectAbilityManager AllEffectAbilities {get; private set;}
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos {get; private set;}
    private readonly int EffectAbilitiesCapacity = 5;
    public List<Effect> LoadAllOneAbilities()
    {
        List<Effect> result = new List<Effect>(128);
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            switch (info.sheetName)
            {
                default:
                    Debug.Log($"{info.sheetName} is not supported on the this script");
                    break;
                case "AreaAbility":
                     result.AddRange(LoadAreaAbility(info.GetDataList()));
                    break;
                case "CastAbility":
                     result.AddRange(LoadCastAbility(info.GetDataList()));
                    break;
                case "DurationAbility":
                     result.AddRange(LoadDurationAbility(info.GetDataList()));
                    break;
                case "MotivateAbility":
                    result.AddRange(LoadMotivateAbility(info.GetDataList()));
                    break;
                case "PassiveAbility":
                    result.AddRange(LoadPassiveAbility(info.GetDataList()));
                    break;
                case "TeamAbility":
                    result.AddRange(LoadTeamAbility(info.GetDataList()));
                    break;
                case "SearchAbility":
                    result.AddRange(LoadSearchAbility(info.GetDataList()));
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
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            AreaAbility effect = new AreaAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                EffectAbilities = ability.abilityInfos,
                Range = float.TryParse(rowDatas[3], out float range) ? range : 1,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
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
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            CastAbility effect = new CastAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                EffectAbilities = ability.abilityInfos,
                Threshold = float.TryParse(rowDatas[3], out float range) ? range : 1,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
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
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            DurationAbility effect = new DurationAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                StackCount = 1,
                Duration = float.TryParse(rowDatas[3], out float range) ? range : 1,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
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
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            MotivateAbility effect = new MotivateAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                EffectAbilities = ability.abilityInfos,
                MotivationInfo = new MotivationInfo(),
                Chance = float.TryParse(rowDatas[3], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[4], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[5], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[6],
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
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            PassiveAbility effect = new PassiveAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[3], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[4], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[5], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[6],
            };
            if (passiveAbilities.Contains(effect)) continue;
            passiveAbilities.Add(effect);
        }

        return passiveAbilities;
    }
    public List<SearchStatusAbility> LoadSearchAbility(List<string[]> values)
    {
        List<SearchStatusAbility> searchAbilities = new List<SearchStatusAbility>(values.Count);
        foreach (string[] rowDatas in values)
        {
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            SearchStatusAbility effect = new SearchStatusAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable, 
                StackCount = 1, 
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                SearchState = rowDatas[3], 
                SearchTag = rowDatas[4], 
                SearchStats = new List<SearchStatusItem>(EffectAbilitiesCapacity), 
                Chance = float.TryParse(rowDatas[5], out float chance) ? chance : 1,
                EffectAbilities = ability.abilityInfos, 
                ApplyTargetCount = int.TryParse(rowDatas[6], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[7], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[8],
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
            EffectAbility ability = AllEffectAbilities.GetEffectAbility(rowDatas[0]);
            TeamAbility effect = new TeamAbility()
            {
                EffectName = rowDatas[0],
                IsStackable = bool.TryParse(rowDatas[1], out bool isStackable) && isStackable,
                StackCount = 1,
                MaxStackCount = int.TryParse(rowDatas[2], out int maxStackCount) ? maxStackCount : 0,
                BuffOrDebuff = bool.TryParse(rowDatas[3], out bool buffOrDebuff) && buffOrDebuff,
                EffectAbilities = ability.abilityInfos,
                Chance = float.TryParse(rowDatas[4], out float chance) ? chance : 1,
                ApplyTargetCount = int.TryParse(rowDatas[5], out int applyTargetCount) ? applyTargetCount : 1,
                ApplyTargetType = Enum.TryParse(rowDatas[6], out ApplyTargetType  applyTargetType) ? applyTargetType : ApplyTargetType.None,
                Description = rowDatas[7],
            };
            if (teamAbilities.Contains(effect)) continue;
            teamAbilities.Add(effect);
        }

        return teamAbilities;
    }
}
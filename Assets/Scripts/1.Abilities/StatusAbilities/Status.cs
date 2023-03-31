using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Status : MonoBehaviour
{
    [field: SerializeField] public CharacterAbility Ability { get; set; }
    [field: SerializeField] public EffectDashBoard EffectDashBoard { get; set; }

    [SerializeField] private List<StatusItemInfo> _totalStatuses = new List<StatusItemInfo>(128);
    
    public float GetFinalizeValue(string statusName)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName, StringComparison.Ordinal))
            {
                return stat.Value;
            }
        }

        return 0;
    }

    public float GetStatusValue(string statusName)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName, StringComparison.Ordinal))
            {
                return stat.Value;
            }
        }

        return 0;
    }
    public float GetStatusValue(int index)
    {
        return _totalStatuses[index].Value;
    }

    public void UpdateTotalStatuses()
    {
        string[] keys = Ability.AllStatusInfos.GetStatusIndexMapKeys;
        foreach (string statusName in keys)
        {
            UpdateStatusValue(statusName);
        }
    }

    public void UpdateStatusValue(string statusName)
    {
        int index = Ability.AllStatusInfos.GetStatusIndex(statusName);
        _totalStatuses[index].Value = Ability.BuffStat.GetStatuses()[index].Value
                                      + Ability.DebuffStat.GetStatuses()[index].Value
                                      + Ability.OriginalStat.GetStatuses()[index].Value
                                      + Ability.PetStat.GetStatuses()[index].Value
                                      + Ability.EquipmentStat.GetStatuses()[index].Value
                                      + Ability.SkillStatus.GetStatuses()[index].Value;
    }

    public void SetBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.Value = value;
            }
        }
    }public void AddBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.Value += value;
            }
        }
    }
    public void MultiplyBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.Value *= value;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status : MonoBehaviour
{
    [field: SerializeField] public CharacterAbility Ability { get; set; }
    [field: SerializeField] public EffectDashBoard EffectDashBoard { get; set; }

    [SerializeField] private List<StatusItemInfo> _totalStatuses = new List<StatusItemInfo>(128);
    
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
        float totalValue = Ability.BuffStat.GetStatuses()[index].Value
                           + Ability.DebuffStat.GetStatuses()[index].Value
                           + Ability.OriginalStat.GetStatuses()[index].Value
                           + Ability.PetStat.GetStatuses()[index].Value
                           + Ability.EquipmentStat.GetStatuses()[index].Value
                           + Ability.SkillStatus.GetStatuses()[index].Value
                           + Ability.MotivationStatus.GetStatuses()[index].Value;
        _totalStatuses[index].SetValue(totalValue);
    }

    public void SetTotalValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.SetValue(value);
            }
        }
    }
    public void AddTotalValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.AddValue(value);
            }
        }
    }
    public void MultiplyTotalValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in _totalStatuses)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.MultiplyValue(value);
            }
        }
    }
}
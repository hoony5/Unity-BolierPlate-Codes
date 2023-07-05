using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status
{
    [field: SerializeField] public AbilityInfo AbilityInfo { get; set; }
    [field: SerializeField] public EffectDashBoard EffectDashBoard { get; set; }

    [SerializeField] private List<StatusItemInfo> _totalStatuses = new List<StatusItemInfo>(128);

    public Status()
    {
        AbilityInfo = new AbilityInfo();
        EffectDashBoard = new EffectDashBoard();
    }
    public void SetAbility(AbilityInfo abilityInfo)
    {
        AbilityInfo = abilityInfo;
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
        string[] keys = AbilityInfo.AllStatusInfos.GetStatusIndexMapKeys;
        foreach (string statusName in keys)
        {
            UpdateStatusValue(statusName);
        }
    }

    public void UpdateStatusValue(string statusName)
    {
        int index = AbilityInfo.AllStatusInfos.GetStatusIndex(statusName);
        float totalValue = 0;
        foreach (StatusBaseAbility statuse in AbilityInfo.Statuses)
        {
            totalValue += statuse.GetStatuses()[index].Value;
        }
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
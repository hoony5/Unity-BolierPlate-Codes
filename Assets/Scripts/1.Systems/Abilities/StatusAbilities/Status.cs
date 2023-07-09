using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public class Status
{
    [field: SerializeField] public AbilityInfo AbilityInfo { get; set; }
    [field: SerializeField] public EffectDashBoard EffectDashBoard { get; set; }

    [SerializeField] private SerializedDictionary<string, StatusItemInfo> _totalStatuses = new SerializedDictionary<string, StatusItemInfo>(128);

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
        if (_totalStatuses.ContainsKey(statusName)) 
            return _totalStatuses[statusName].Value;
        Debug.LogError($"{statusName} is not in Status");
        return 0;

    }

    public void UpdateStatusValue(string statusName)
    {
        if (AbilityInfo.TryGetAllStatusBaseInfo(statusName, out float baseValue))
        {
            _totalStatuses[statusName].SetValue(baseValue);
        }
        else
        {
            Debug.LogError($"{statusName} is not in Status");
        }
    }

    public void SetTotalValue(string statusName, float value)
    {
        if (!_totalStatuses.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in Status");
            return;
        }
        _totalStatuses[statusName].SetValue(value);
    }
    public void AddTotalValue(string statusName, float value)
    {
        if (!_totalStatuses.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in Status");
            return;
        }
        _totalStatuses[statusName].AddValue(value);
    }
    public void MultiplyTotalValue(string statusName, float value)
    {
        if (!_totalStatuses.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in Status");
            return;
        }
        _totalStatuses[statusName].MultiplyValue(value);
    }
}
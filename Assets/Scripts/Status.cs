using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status : MonoBehaviour
{
    [field: SerializeField] private CharacterAbility Ability { get; set; }

    [SerializeField] private List<StatusItemInfo> _totalStatuses = new List<StatusItemInfo>(128);

    public float GetFinalizeValue(string statusName)
    {
        //return _totalStatuses[Ability.AllStatusInfos.GetStatusIndex(statusName)].Value;
        return _totalStatuses.Find(i => i.RawName.Equals(statusName, StringComparison.Ordinal)).Value; 
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

    public void SetBaseValue(string statusName, int value)
    {
        StatusItemInfo stat = _totalStatuses.Find(i => i.RawName.Equals(statusName, StringComparison.Ordinal));
        if (stat is null)
        {
            _totalStatuses.Add(new StatusItemInfo(){RawName = statusName, Value = value});
            return;
        }

        stat.Value = value;
    }
}
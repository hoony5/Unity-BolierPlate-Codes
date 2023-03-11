using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status : MonoBehaviour
{
    [field: SerializeField] private CharacterAbility Ability { get; set; }
    
    [SerializeField] private List<StatusItemInfo> _totalStatuses = new List<StatusItemInfo>(128);
    
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
}
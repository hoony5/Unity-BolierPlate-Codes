using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    [field:SerializeField] private SerializedDictionary<string, StatusBaseAbility> StatusesMap {get; set;} 
        = new SerializedDictionary<string, StatusBaseAbility>(128);
    
    public bool TryGetStatusAbility(string statType, out StatusBaseAbility statusBaseAbility)
    {
        return StatusesMap.TryGetValue(statType, out statusBaseAbility);
    }

    public bool TryGetAllStatusBaseInfo(string statusName ,out float sum)
    {
        sum = 0;
        foreach (KeyValuePair<string, StatusBaseAbility> status in StatusesMap)
        {
            sum += status.Value.GetBaseValue(statusName); 
        }
        return true;
    }
}

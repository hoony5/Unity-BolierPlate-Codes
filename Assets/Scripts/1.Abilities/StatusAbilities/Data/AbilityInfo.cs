using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    [field:SerializeField] public AllStatusInfos AllStatusInfos{get; private set;}
    [field:SerializeField] public List<StatusBaseAbility> Statuses {get; private set;}
    [field:SerializeField] public Dictionary<string, StatusBaseAbility> StatusesMap {get; private set;}

    public AbilityInfo(){ }
    public AbilityInfo(List<StatusBaseAbility> statuses)
    {
        Statuses = statuses;
        StatusesMap = statuses.ToDictionary(x => x.Name, x => x);
    }
    public void SetStatusBaseInfo(List<StatusItemInfo> statusBaseInfo)
    {
        foreach (StatusBaseAbility stat in Statuses)
        {
            stat.SetStatusesBaseInfo(statusBaseInfo);
        }
    }
}

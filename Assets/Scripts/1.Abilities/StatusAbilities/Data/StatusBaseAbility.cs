using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusBaseAbility
{
    [field: SerializeField] protected List<StatusItemInfo> statusItems = new List<StatusItemInfo>(128);

    public void AddStatusesBaseInfo(List<StatusItemInfo> list)
    {
        statusItems = list;
    }
    
    protected void ClearValues()
    {
        for (var index = 0; index < statusItems.Count; index++)
        {
            statusItems[index].SetValue(0);
        }
    }

    public List<StatusItemInfo> GetStatuses()
    {
        return statusItems;
    }

    public void SetBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in statusItems)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.SetValue(value);
            }
        }
    }
    public void AddBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in statusItems)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.AddValue(value);
            }
        }
    }
    public void MultiplyBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in statusItems)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.MultiplyValue(value);
            }
        }
    }
}

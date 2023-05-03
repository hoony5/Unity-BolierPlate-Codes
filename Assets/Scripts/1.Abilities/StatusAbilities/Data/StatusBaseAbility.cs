using System.Collections.Generic;
using UnityEngine;

public class StatusBaseAbility : MonoBehaviour
{
    [field: SerializeField] protected List<StatusItemInfo> statusItems = new List<StatusItemInfo>(128);

    public void ClearValues()
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
        for (var index = 0; index < statusItems.Count; index++)
        {
            StatusItemInfo stat = statusItems[index];
            if (stat.RawName.Equals(statusName))
            {
                stat.SetValue(value);
            }
        }
    }
    public void AddBaseValue(string statusName, float value)
    {
        for (var index = 0; index < statusItems.Count; index++)
        {
            StatusItemInfo stat = statusItems[index];
            if (stat.RawName.Equals(statusName))
            {
                stat.AddValue(value);
            }
        }
    }
    public void MultiplyBaseValue(string statusName, float value)
    {
        for (var index = 0; index < statusItems.Count; index++)
        {
            StatusItemInfo stat = statusItems[index];
            if (stat.RawName.Equals(statusName))
            {
                stat.MultiplyValue(value);
            }
        }
    }
}

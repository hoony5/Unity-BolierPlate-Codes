using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OriginalStatusComponent : MonoBehaviour
{
    [field: SerializeField] private List<StatusItemInfo> statusItems = new List<StatusItemInfo>(128);

    public List<StatusItemInfo> GetStatuses()
    {
        return statusItems;
    }

    public void ClearValues()
    {
        for (var index = 0; index < statusItems.Count; index++)
        {
            statusItems[index].Value = 0;
        }
    }
}
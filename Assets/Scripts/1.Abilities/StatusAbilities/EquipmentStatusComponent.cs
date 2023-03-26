using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentStatusComponent : MonoBehaviour
{
    [field: SerializeField] private List<StatusItemInfo> statusItems = new List<StatusItemInfo>(128);

    public void ClearValues()
    {
        for (var index = 0; index < statusItems.Count; index++)
        {
            statusItems[index].Value = 0;
        }
    }

    public List<StatusItemInfo> GetStatuses()
    {
        return statusItems;
    }
}
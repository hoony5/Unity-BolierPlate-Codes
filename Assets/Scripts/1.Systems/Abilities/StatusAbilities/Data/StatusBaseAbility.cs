using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable]
public class StatusBaseAbility
{
    [field:SerializeField]  public string Name { get; set; }
    [field: SerializeField] protected SerializedDictionary<string, StatusItemInfo> statusItems = new SerializedDictionary<string, StatusItemInfo>(128);
    public StatusBaseAbility(){}
    public StatusBaseAbility(string name) => Name = name;
    public void Clear()
    {
        statusItems.Clear();
    }
    protected void ClearValues()
    {
        foreach (KeyValuePair<string, StatusItemInfo> status in statusItems)
        {
            status.Value.SetValue(0);
        }
    }

    public float GetBaseValue(string statusName)
    {
        if (!statusItems.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in {Name} status");
            return 0;
        }
        
        return statusItems[statusName].Value;
    }
    public void SetBaseValue(string statusName, float value)
    {
        if (!statusItems.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in {Name} status");
            return;
        }
        
        statusItems[statusName].SetValue(value);
    }
    public void AddBaseValue(string statusName, float value)
    {
        if (!statusItems.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in {Name} status");
            return;
        }
        
        statusItems[statusName].AddValue(value);
    }
    public void MultiplyBaseValue(string statusName, float value)
    {
        if (!statusItems.ContainsKey(statusName))
        {
            Debug.LogError($"{statusName} is not in {Name} status");
            return;
        }
        
        statusItems[statusName].MultiplyValue(value);
    }
}

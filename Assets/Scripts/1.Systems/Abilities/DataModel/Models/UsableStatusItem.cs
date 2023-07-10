using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UsableStatusItem : StatusItem , IUsable
{
    [field:SerializeField] public UnityEvent OnUse { get; set; }

    public UsableStatusItem(StatusItem item)
    {
        Name = item.Name;
        Attributes = item.Attributes;
        StatusAbility = item.StatusAbility;
    }
    
    public bool Use(int count)
    {
        bool success = TryAddCount(-count);

        if (!success) return false;
        OnUse?.Invoke();
        return true;
    }

    public bool Abandon(int count)
    {
        throw new System.NotImplementedException();
    }

    public bool Fill(int count)
    {
        bool success = TryAddCount(count);
        return success;
    }
}
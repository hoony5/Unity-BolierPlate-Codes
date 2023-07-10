using UnityEngine;

[System.Serializable]
public class StatusItem : ModuleController
{
    [field: SerializeField] public string Name { get;  set; }
    [field: SerializeField] public int Count { get;  set; }
    [field: SerializeField] public StatusItemAttributes Attributes { get;  set; }
    [field:SerializeField] public Status StatusAbility { get;  set; }
    public void SetAttributes(StatusItemAttributes statusItemAttributes)
    {
        Attributes = statusItemAttributes;
    }
    protected bool TryAddCount(int count)
    {
        Count += count;
        if (Count > Attributes.MaxCount)
        {
            Count = Attributes.MaxCount;
            return false;
        }

        if (Count >= 0) return true;
        Count = 0;
        return false;
    }

}
using UnityEngine;

[System.Serializable]
public class Equipment : ModuleController, IEquipable, IDissolvable
{
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Transform Transform { get; protected set; }
    [field: SerializeField] public int Count { get; protected set; }
    [field:SerializeField] public Status StatusAbility { get; protected set; }
    [field: SerializeField] public EquipmentAttributes Attributes { get; protected set; }
    public void SetAttributes(EquipmentAttributes equipmentAttributes)
    {
        Attributes = equipmentAttributes;
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
    public bool Equip(bool isValidate)
    {
        throw new System.NotImplementedException();
    }

    public bool Unequip()
    {
        throw new System.NotImplementedException();
    }

    public void DissolveBy(int count)
    {
        throw new System.NotImplementedException();
    }
}
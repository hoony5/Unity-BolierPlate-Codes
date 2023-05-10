using UnityEngine;

[System.Serializable]
public class Equipment : Item, IEquipable, IEnhancable, IDissolvable
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Status StatusAbility { get; private set; }
    public bool Equip(bool isValidate)
    {
        throw new System.NotImplementedException();
    }

    public bool Unequip()
    {
        throw new System.NotImplementedException();
    }

    public bool Enhance(int enhanceLevel)
    {
        throw new System.NotImplementedException();
    }

    public bool Broke()
    {
        throw new System.NotImplementedException();
    }

    public bool Repair()
    {
        throw new System.NotImplementedException();
    }

    public void Dissolve(int count)
    {
        throw new System.NotImplementedException();
    }
}
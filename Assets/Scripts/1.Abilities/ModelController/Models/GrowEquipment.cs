using UnityEngine;

[System.Serializable]
public class GrowEquipment : Item, IEquipable
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: SerializeField] public int CurrentExp { get; private set; }
    [field: SerializeField] public int MaxExp { get; private set; }
    [field: SerializeField] public Status StatusAbility { get; private set; }
    
    public bool Equip(bool isValidate)
    {
        throw new System.NotImplementedException();
    }

    public bool Unequip()
    {
        throw new System.NotImplementedException();
    }

    public void ResetLevel() => Level = 0;
    public void ResetExp() => CurrentExp = 0;
    public void AddLevel(int level)
    {
        Level += level;
        if(Level > MaxLevel) Level = MaxLevel;
        if(Level < 0) Level = 0;
    }

    public bool TryAddExp(int exp)
    {
        CurrentExp += exp;
        if (CurrentExp >= MaxExp)
        {
            CurrentExp = MaxExp;
            return true;
        }
        if(CurrentExp < 0) CurrentExp = 0;
        return false;
    }

    public void LevelUp()
    {
        AddLevel(1);
        ResetExp();
    }
}

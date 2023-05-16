using System;
using UnityEngine;

[Serializable]
public class GrowablePet : Pet, IGrowable
{
    [field:SerializeField] public int Level { get; private set; }
    [field:SerializeField] public int MaxLevel { get; private set; }
    [field:SerializeField] public int CurrentExp { get; private set; }
    [field:SerializeField] public int MaxExp { get; private set; }    
    
    public GrowablePet(Pet pet)
    {
        Name = pet.Name;
        Attributes = pet.Attributes;
        StatusAbility = pet.StatusAbility;
    }
    public void SetBaseInfo(int maxLevel, int maxExp)
    {
        MaxLevel = maxLevel;
        MaxExp = maxExp;
    }
    public void ResetLevel() => Level = 0;

    public void ResetExp() => CurrentExp = 0;

    public void AddLevel(int level)
    {
         if (Level + level > MaxLevel) return;
          if (Level + level < 0) return;
          Level += level;
    }

    public bool TryAddExp(int exp)
    {
        if (CurrentExp + exp > MaxExp) return false;
        if (CurrentExp + exp < 0) return false;
        CurrentExp += exp;
        return true;
    }

    public void LevelUp()
    {
        if (Level >= MaxLevel) return;
        Level++;
    }
}
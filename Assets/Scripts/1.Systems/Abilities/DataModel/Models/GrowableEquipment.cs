﻿using UnityEngine;

[System.Serializable]
public class GrowableEquipment : Equipment, IGrowable
{
    [field:SerializeField] public int Level { get;  set; }
    [field:SerializeField] public int MaxLevel { get;  set; }
    [field:SerializeField] public int CurrentExp { get;  set; }
    [field:SerializeField] public int MaxExp { get;  set; }
    
    public GrowableEquipment(Equipment equipment)
    {
        Name = equipment.Name;
        Attributes = equipment.Attributes;
        StatusAbility = equipment.StatusAbility;
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

using System;
using UnityEngine;

// Companionable
[Serializable]
public class EnhancablePet : Pet, IEnhancable
{
    [field:SerializeField] public bool IsDisabled { get;  set; }
    [field:SerializeField] public int Level { get;  set; }
    [field:SerializeField] public int MaxLevel { get;  set; }
    [field:SerializeField] public int CurrentExp { get;  set; }
    [field:SerializeField] public int MaxExp { get;  set; }
    
    public EnhancablePet(Pet pet)
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
    [ToDo("Revise Random Solution")]
    public bool Enhance(int enhanceLevel)
    {
        if (IsDisabled) return false;
        if (CurrentExp < MaxExp) return false;
        Level += enhanceLevel;
        CurrentExp = 0;
        return true;
    }

    public bool Broke()
    {
        if(IsDisabled) return false;
        
        IsDisabled = true;
        CurrentExp = 0;
        return IsDisabled;
    }

    public bool Repair()
    {
        if(!IsDisabled) return false;
        
        IsDisabled = false;
        return !IsDisabled;
    }
}
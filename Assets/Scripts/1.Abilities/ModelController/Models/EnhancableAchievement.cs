using System;
using UnityEngine;

[Serializable]
public class EnhancableAchievement : Achievement, IEnhancable
{
    [field:SerializeField] public bool IsDisabled { get; private set; }
    [field:SerializeField] public int Level { get; private set; }
    [field:SerializeField] public float Chance { get; private set; }
    [field:SerializeField] public int MaxLevel { get; private set; }
    [field:SerializeField] public int CurrentExp { get; private set; }
    [field:SerializeField] public int MaxExp { get; private set; }

    public EnhancableAchievement(Achievement achievement)
    {
        Name = achievement.Name;
        Attributes = achievement.Attributes;
        StatusAbility = achievement.StatusAbility;
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

        CurrentExp = 0;
        IsDisabled = true;
        return IsDisabled;
    }

    public bool Repair()
    {
        if(!IsDisabled) return false;
        
        IsDisabled = false;
        return !IsDisabled;
    }
}
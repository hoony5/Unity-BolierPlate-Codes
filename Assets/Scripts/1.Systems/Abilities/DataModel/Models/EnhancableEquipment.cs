using UnityEngine;

[System.Serializable]
public class EnhancableEquipment : Equipment , IEnhancable
{
    [field:SerializeField] public bool IsDisabled { get;  set; }
    [field:SerializeField] public int Level { get;  set; }
    [field:SerializeField] public int MaxLevel { get;  set; }
    [field:SerializeField] public int CurrentExp { get;  set; }
    [field:SerializeField] public int MaxExp { get;  set; }

    public EnhancableEquipment(Equipment equipment)
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
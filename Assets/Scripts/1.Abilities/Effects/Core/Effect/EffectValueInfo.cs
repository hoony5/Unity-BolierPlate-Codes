using System;
using UnityEngine;

[System.Serializable]
public class EffectValueInfo
{
    [field:SerializeField] public string EffectName;
    [field:SerializeField] public int Level;
    [field:SerializeField] public int MaxLevel;
    [field:SerializeField] public float BaseValue;
    [field:SerializeField] public float CoolTime;
    [field:SerializeField] public float ValuePerLevel;

    public void ClearValues()
    {
        BaseValue = 0;
        CoolTime = 0;
    }
    public void ResetLevel()
    {
        Level = 0;
    }
    public void AddLevel(int value)
    {
        Level += value;
        if (Level > MaxLevel) Level = MaxLevel;
        if (Level < 0) Level = 0;
    }
}
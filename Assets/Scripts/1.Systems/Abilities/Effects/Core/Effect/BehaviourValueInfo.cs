using System;
using UnityEngine;

[System.Serializable]
public class BehaviourValueInfo
{
    [field:SerializeField] public string BehaviourName { get; set; }
    [field:SerializeField] public int Level { get; set; }
    [field:SerializeField] public int MaxLevel { get; set; }
    [field:SerializeField] public int CurrentExp { get; set; }
    [field:SerializeField] public int[] MaxExps  { get; set; }
    [field:SerializeField] public float[] BaseValues { get; set; }
    [field:SerializeField] public float[] CoolTimes { get; set; }
    [field:SerializeField] public float ValuePerLevel { get; set; }

    public void ClearValues()
    {
        Array.Clear(BaseValues, 0, BaseValues.Length);
        Array.Clear(CoolTimes, 0, CoolTimes.Length);
    }
    public void ResetLevel()
    {
        Level = 0;
    }

    public void ResetExp()
    {
        CurrentExp = 0;
    }

    public void LevelUp()
    {
        AddLevel(1);
        ResetExp();
    }
    public void AddLevel(int value)
    {
        Level += value;
        if (Level > MaxLevel) Level = MaxLevel;
        if (Level < 0) Level = 0;
    }

    public bool TryAddExp(int exp)
    {
        CurrentExp += exp;
        if (CurrentExp > MaxExps[Level])
        {
            CurrentExp = MaxExps[Level];
            return true;
        }
        if (CurrentExp < 0) CurrentExp = 0;
        return false;
    }
}
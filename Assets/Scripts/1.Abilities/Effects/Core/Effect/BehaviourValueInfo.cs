using System;
using UnityEngine;

[System.Serializable]
public class BehaviourValueInfo
{
    [field:SerializeField] public string BehaviourName;
    [field:SerializeField] public int Level;
    [field:SerializeField] public int MaxLevel;
    [field:SerializeField] public float[] BaseValues;
    [field:SerializeField] public float[] CoolTimes;
    [field:SerializeField] public float ValuePerLevel;

    public void ClearValues()
    {
        Array.Clear(BaseValues, 0, BaseValues.Length);
        Array.Clear(CoolTimes, 0, CoolTimes.Length);
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
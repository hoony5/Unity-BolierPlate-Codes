using System;
using UnityEngine;

[System.Serializable]
public class EffectValueInfo
{
    [SerializeField] private string effectName;
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private float[] baseValues;
    [SerializeField] private float[] coolTimes;
    [SerializeField] private float[] costs;

    public string EffectName => effectName;
    public int Level => level;
    public int MaxLevel => maxLevel;
    
    public float GetValue => baseValues[level];
    public float GetCoolTime => coolTimes[level];
    public float GetCost => costs[level];

    public void ClearValues()
    {
        Array.Clear(baseValues, 0, baseValues.Length);
        Array.Clear(coolTimes, 0, coolTimes.Length);
        Array.Clear(costs, 0, costs.Length);
    }
    public void ResetLevel()
    {
        level = 0;
    }
    public void AddLevel(int value)
    {
        level += value;
        if (level > maxLevel) level = maxLevel;
        if (level < 0) level = 0;
    }
    public EffectValueInfo(string effectName, int maxLevel, float[] baseValues, float[] coolTimes, float[] costs)
    {
        this.effectName = effectName;
        this.maxLevel = maxLevel;
        this.baseValues = baseValues;
        this.coolTimes = coolTimes;
        this.costs = costs;
    }

}
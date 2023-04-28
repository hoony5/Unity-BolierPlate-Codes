using System;
using UnityEngine;

[System.Serializable]
public class EffectValueInfo
{
    [SerializeField] private string effectName;
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private float baseValue;
    [SerializeField] private float coolTime;
    [SerializeField] private float cost;

    public string EffectName => effectName;
    public int Level => level;
    public int MaxLevel => maxLevel;
    
    public float GetValue => baseValue;
    public float GetCoolTime => coolTime;
    public float GetCost => cost;

    public void ClearValues()
    {
        baseValue = 0;
        coolTime = 0;
        cost = 0;
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
    public EffectValueInfo(string effectName, int maxLevel, float baseValue, float coolTime, float cost)
    {
        this.effectName = effectName;
        this.maxLevel = maxLevel;
        this.baseValue = baseValue;
        this.coolTime = coolTime;
        this.cost = cost;
    }

}
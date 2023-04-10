using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class BattleCalculator
{
    #region Basic Calculation
    // try hit
    public static bool IsHit(float chance, float hitRate, float avoidanceRate, bool cannotAttack)
    {
        return !cannotAttack && chance <= hitRate - avoidanceRate;
    }
    
    /// <summary>
    /// ratio is percentage 4.5% => 4.5
    /// </summary>
    /// <param name="baseValue"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public static float GetRatioValue(float baseValue, float ratio)
    {
        return baseValue * ratio * 0.01f;
    }
    // Heal or Damage 
    public static float GetRawDamage(float chance, float inputDamage, float criticalRate, float criticalPercentage)
    {
        return chance <= criticalRate ? inputDamage * criticalPercentage : inputDamage;
    }
    
    // Resist heal or damage
    public static float IgnoreRawDamage(float chance, float inputDamage, float resistanceRate, float resistancePercentage)
    {
        return chance <= resistanceRate ? inputDamage * resistancePercentage : inputDamage;
    }
    
    // Penetration attack or defense raw Value
    public static float AddedRawValue(float baseValue, float rawValue, float min = 0, float max = float.MaxValue)
    {
        float result = baseValue + rawValue;
        if(result < min)
            result = min;
        else if(result > max)
            result = max;
        return result;
    }
    // check state
    public static bool IsLastBehaviour(float current, float lastThreshold, bool isDead)
    {
        return !isDead && current <= lastThreshold;
    }
    public static bool IsNextBehaviour(float baselineValue, float thresholdValue, bool isDead)
    {
        return !isDead && baselineValue >= thresholdValue;
    }
    // check dead
    public static bool IsZero(float value)
    {
        return value <= 0;
    }
    #endregion
}
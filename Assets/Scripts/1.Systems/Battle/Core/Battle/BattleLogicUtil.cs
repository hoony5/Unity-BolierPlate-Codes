using System;
using UnityEngine;

public static class BattleLogicUtil
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
        return chance <= criticalRate ? inputDamage * 0.01f * criticalPercentage : inputDamage;
    }
    
    // Resist heal or damage
    public static float IgnoreRawDamage(float chance, float inputDamage, float resistanceRate, float resistancePercentage)
    {
        return chance <= resistanceRate ? inputDamage * 0.01f * resistancePercentage : inputDamage;
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

    #region Effects Calcul
    public static bool ApplyEffect(this Character from, Character to, IAbility effect, BattleEnvironment current)
    {
        float chance = NumberEx.RandomRangeByInt().IntToFloat(); // -> move to battleEnvironment
        if (!effect.HitTheChance(chance))
        {
#if UNITY_EDITOR
            DebugEx.Yellow($"Chance Missed");
#endif
            return false;
        }
        switch (effect)
        {
            /// Mono Effect
            case CastAbility castAbility:
                bool stayPressButton = castAbility.HasThresholdPassed( 0/* theshold */);
                break;
            case DurationAbility durationAbility:
                bool timeIsPassed = durationAbility.HasTimePassed(0/* duration : time */);
                break;
            case AreaAbility areaAbility:
                // bool isCorrectTarget = areaAbility.DetectObjectOnValidateArea(other, 0,  /* ref new Collider[0] */);
                break;
            case MotivateAbility motivateAbility:
                motivateAbility.SetMotivationActive(from, to);
                break;
            case PassiveAbility passiveAbility:
                passiveAbility.UpdateAbility(from, to);
                break;
            case SearchStatusAbility searchStatusAbility:
                // divide type searchStatusAbility.
                break;
            case TeamAbility teamAbility:
                teamAbility.UpdateAbility(from, to);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effect));
        }

        return false;
    }

    #endregion
}
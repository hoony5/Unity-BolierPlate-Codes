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

    #region Effects Calcul

    /// Battle Hierarchy
    /// Effect
    /// 1. wait until Threshold passed - external
    /// 1.1 is Hit? 
    /// 2. Detect Area
    /// 3. apply duration
    /// 4.Search Tag on the colliders
    /// 4-1 .Search State on the Searched Tags
    /// 4-2 .Search Status on the Searched Tags
    /// 5. Get Searched Targets
    /// 6. Motivated
    /// 7.1 Check Condition for Motivation
    /// 7.2 if Motivated, Get Motivated Targets
    /// 8. Get Motivated Targets
    /// 9. Apply Team Effect
    /// 10. Done
    /// Passive
    /// 1. Check Only Battle or Global
    /// 2. Apply it content type by content type
    public static bool ApplyEffect(this Character from, Character other, IAbility effect, BattleEnvironment current)
    {
        switch (effect)
        {
            /// Complex Effect
            case CastAreaDurationAimedMotivatedStatusAbility castAreaDurationAimedMotivatedStatusAbility:

                bool isHit = castAreaDurationAimedMotivatedStatusAbility.HitTheChance(current.hitRate);
                if(!isHit) return false;
                
                // Check Time Passed
                bool hasTimePassed = castAreaDurationAimedMotivatedStatusAbility.HasThresholdPassed(current.threshold);
                if (!hasTimePassed) return false;
                
                string effectName = castAreaDurationAimedMotivatedStatusAbility._effectName;
                
                // Find Target Condition
                bool isFound = castAreaDurationAimedMotivatedStatusAbility.FindTag(other);
                bool isStateFound = castAreaDurationAimedMotivatedStatusAbility.FindCharacterState(other, effectName);
                bool isStatusesFound = castAreaDurationAimedMotivatedStatusAbility.FindCharacterStatus(other);
                bool isTarget = isFound && isStateFound && isStatusesFound;

                // Check Motivation
                if (!isTarget) return false;
                Character target = castAreaDurationAimedMotivatedStatusAbility.ApplyTargetType is ApplyTargetType.Enemy ? other : from;
                bool isMotivated = false;
                switch (castAreaDurationAimedMotivatedStatusAbility.MotivationComparerType)
                {
                    default:
                   case ComparerType.None :
                       break;
                   case ComparerType.Equal :
                       isMotivated = castAreaDurationAimedMotivatedStatusAbility.IsMotivatedWhenApproximately(target);
                       break;
                   case ComparerType.LessOrEqual :
                       isMotivated = castAreaDurationAimedMotivatedStatusAbility.IsMotivatedWhenLess(target);
                       break;
                   case ComparerType.GreaterOrEqual:
                       isMotivated = castAreaDurationAimedMotivatedStatusAbility.IsMotivatedWhenGreater(target);
                       break;
                }

                
                
                Timer timer = Clock.Instance.GetFreeTimer();
                timer.SetDuration(castAreaDurationAimedMotivatedStatusAbility.Duration);
                timer.SetMaxTime(castAreaDurationAimedMotivatedStatusAbility.Duration);
                timer.SetOwner(from);
                timer.Start();
                current.SetEffectTimer(effectName, timer);
                break;
            case CastAreaDurationAimedStatusAbility castAreaDurationAimedStatusAbility:
                break;
            case CastAreaDurationTeamAbility castAreaDurationTeamAbility:
                break;
            case CastMotivatedDurationAbility castMotivatedDurationAbility:
                break;
            case AreaDurationAimedMotivatedStatusAbility areaDurationAimedMotivatedStatusAbility:
                break;
            case AreaDurationAimedStatusAbility areaDurationAimedStatusAbility:
                break;
            case AreaDurationTeamAbility areaDurationTeamAbility:
                break;
            /// Combine Effect
                // area
            case AreaCastAbility areaCastAbility:
                break;
            case AreaAimedStatusAbility areaAimedStatusAbility:
                break;
            case AreaDurationAbility areaDurationAbility:
                break;
            case AreaMotivatedAbility areaMotivatedAbility:
                break;
            case AreaTeamAbility areaTeamAbility:
                break;
                // time
            case CastAimedStatusAbility castAimedStatusAbility:
                break;
            case CastDurationAbility castDurationAbility:
                break;
            case CastTeamAbility castTeamAbility:
                break;
            case DurationMotivatedAbility durationMotivatedAbility:
                break;
            case DurationTeamAbility durationTeamAbility:
                break;
            case DurationAimedStatusAbility durationAimedStatusAbility:
                break;
            /// Mono Effect
            case CastAbility castAbility:
                break;
            case DurationAbility durationAbility:
                break;
            case AreaAbility areaAbility:
                break;
            case MotivateAbility motivateAbility:
                break;
            case PassiveAbility passiveAbility:
                break;
            case SearchStatusAbility searchStatusAbility:
                break;
            case TeamAbility teamAbility:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effect));
        }

        return false;
    }

    #endregion
}
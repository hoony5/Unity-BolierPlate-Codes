using System;
using UnityEngine;
using UnityEngine.Profiling;

public static class BattleLogicEx
{
    public static void HandleBattleEnv(ref BattleEnvironment environment)
    {
        
    }
    public static bool TryApplyEffect(this Character from, Character to, IAbility effect, BattleEnvironment current)
    {
        // when executing effect , check if the effect is hit or not. of course, when hit basic attack not flee.
        
        float chance = NumberEx.RandomRangeByInt().IntToFloat();
        if (!effect.IsHitChance(chance))
        {
#if UNITY_EDITOR
            ColorLog.Yellow($"Chance Missed");
#endif
            return false;
        }

        switch (effect)
        {
            case CastAbility castAbility:
                return HandleCastAbility(castAbility);
            case DurationAbility durationAbility:
                return HandleDurationAbility(durationAbility);
            case AreaAbility areaAbility:
                HandleAreaAbility(from, areaAbility, current);
                break;
            case MotivateAbility motivateAbility:
                HandleMotivateAbility(from, to, motivateAbility);
                break;
            case PassiveAbility passiveAbility:
                HandlePassiveAbility(from, to, passiveAbility);
                break;
            case SearchStatusAbility searchStatusAbility:
                return HandleSearchStatusAbility(to, searchStatusAbility);
            case TeamAbility teamAbility:
                HandleTeamAbility(from, to, teamAbility);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effect));
        }

        return false;
    }

    private static bool HandleCastAbility(CastAbility castAbility)
    {
        return castAbility.HasThresholdPassed(0/* theshold */);
    }

    private static bool HandleDurationAbility(DurationAbility durationAbility)
    {
        return durationAbility.HasTimePassed(0/* duration : time */);
    }

    private static void HandleAreaAbility(Character from, AreaAbility areaAbility, BattleEnvironment current)
    {
#if UNITY_EDITOR
        Profiler.BeginSample("Cautious - Watch Grade :: DetectObjectOnValidateArea");
#endif
        Collider[] colliders = new Collider[areaAbility.ApplyTargetCount];
        areaAbility.DetectObjectOnValidateArea(from, current.AreaMask , ref colliders);
        current.EffectTargets = colliders.DeepCopy();
#if UNITY_EDITOR
        Profiler.EndSample();
#endif
    }

    private static void HandleMotivateAbility(Character from, Character to, MotivateAbility motivateAbility)
    {
        motivateAbility.SetMotivationActive(from, to);
    }

    private static void HandlePassiveAbility(Character from, Character to, PassiveAbility passiveAbility)
    {
        passiveAbility.UpdateAbility(from, to);
    }

    private static bool HandleSearchStatusAbility(Character to, SearchStatusAbility searchStatusAbility)
    {
        return searchStatusAbility.SearchTarget(to);
    }

    private static void HandleTeamAbility(Character from, Character to, TeamAbility teamAbility)
    {
        teamAbility.UpdateAbility(from, to);
    }
}

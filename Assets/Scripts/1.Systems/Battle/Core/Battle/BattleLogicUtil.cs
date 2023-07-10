using System;

public static class BattleLogicUtil
{
    #region Effects Calcul
    public static bool ApplyEffect(this Character from, Character to, IAbility effect, BattleEnvironment current)
    {
        float chance = NumberEx.RandomRangeByInt().IntToFloat(); // -> move to battleEnvironment
        if (!effect.IsHitChance(chance))
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
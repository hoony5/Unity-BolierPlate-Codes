public interface IPassiveAbility : IAbility
{
    void ResetStatus(Character character, EffectAbilityStat stat);
    void CalculateTeamStatus(Character character, EffectAbilityStat stat);
    void UpdateAbility(Character[] ourTeam, Character[] enemyTeam);
    void UpdateAbility(Character player, Character enemy);
    void UpdateForTeam(Character[] team, EffectAbilityStat stat, ApplyTargetType targetType);
}
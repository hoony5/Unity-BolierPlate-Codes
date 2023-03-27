/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Effect to pet statuses that can be applied to Abilities.
/// </summary>
public interface IAbilityToSkillAbility : IAbility
{
    void EffectToSkillAbility(SkillStatusComponent ability);
}
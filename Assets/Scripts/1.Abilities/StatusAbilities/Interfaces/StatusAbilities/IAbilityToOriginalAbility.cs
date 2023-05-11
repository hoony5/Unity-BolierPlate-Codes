/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Effect to base statuses that can be applied to Abilities.
/// </summary>
public interface IAbilityToOriginalAbility : IAbility
{
    void EffectToOriginalAbility(StatusBaseAbility ability);
}
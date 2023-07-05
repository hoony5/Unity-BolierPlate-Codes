/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Debuffs that can be applied to Abilities.
/// </summary>
public interface IDebuffToAbility : IAbility
{
    void DebuffToAbility(StatusBaseAbility ability);
}
/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Debuffs that can be applied to Abilities.
/// </summary>
public interface IDebuffToAbility : IEffect
{
    void DebuffToAbility(DebuffStatusComponent ability);
}
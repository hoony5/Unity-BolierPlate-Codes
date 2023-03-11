/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Buffs that can be applied to Abilities.
/// </summary>
public interface IBuffToAbility : IEffect
{
    void BuffToAbility(BuffStatusComponent ability);
}
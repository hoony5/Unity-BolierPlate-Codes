/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Effect of Equipment that can be applied to Abilities.
/// </summary>
public interface IAbilityToEquipmentAbility : IAbility
{
    void EffectToEquipmentAbility(StatusBaseAbility ability);
}
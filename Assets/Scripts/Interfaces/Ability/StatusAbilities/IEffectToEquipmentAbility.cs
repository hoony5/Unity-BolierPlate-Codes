/// <summary>
/// This interface is for Battle Effects that can be applied to Abilities.
/// for Effect of Equipment that can be applied to Abilities.
/// </summary>
public interface IEffectToEquipmentAbility : IEffect
{
    void EffectToEquipmentAbility(EquipmentStatusComponent ability);
}
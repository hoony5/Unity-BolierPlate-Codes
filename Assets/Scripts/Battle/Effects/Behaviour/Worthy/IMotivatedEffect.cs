using System.Collections.Generic;

/// <summary>
/// effect that is applied when the character is motivated
/// </summary>
public interface IMotivatedEffect : IEffect
{
    public List<EffectAbility> Effects { get; set; }
    void UpdateEffect(Character character, Character other, bool isMotivated);
}
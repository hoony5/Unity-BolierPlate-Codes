using System.Collections.Generic;

/// <summary>
/// effect that is applied when the character is motivated
/// </summary>
public interface IMotivatedEffect : IEffect
{
    public List<EffectAbility> EffectAbilities { get; set; }
    bool TryCheckMotivation(bool isMotivated);
}
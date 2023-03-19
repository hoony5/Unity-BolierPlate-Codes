using System.Collections.Generic;

/// <summary>
/// if the Team within a count people, the effect will be applied.
/// </summary>
public interface ITeamEffect : IEffect
{
    bool BuffOrDebuff { get; set; }
    List<EffectAbility> Effects { get; set; }
    void UpdateEffect(Character character, Character other, int count);
}
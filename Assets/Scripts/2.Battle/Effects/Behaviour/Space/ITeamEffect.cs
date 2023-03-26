using System.Collections.Generic;

/// <summary>
/// if the Team within a count people, the effect will be applied.
/// </summary>
public interface ITeamEffect : IEffect
{
    bool BuffOrDebuff { get; set; }
    List<EffectAbility> EffectAbilities { get; set; }
    void UpdateAbility(Character[] characters);
}
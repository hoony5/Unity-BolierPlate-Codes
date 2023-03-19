using System.Collections.Generic;

/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchAbilityEffect : IEffect
{
    List<StatusItemInfo> SearchStats { get; set; }
    void UpdateEffect(Character character, Character other, string abilityName, float threshold);
}
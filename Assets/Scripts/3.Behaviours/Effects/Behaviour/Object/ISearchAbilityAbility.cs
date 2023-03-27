using System.Collections.Generic;

/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchAbilityAbility : IAbility
{
    List<StatusItemInfo> SearchStats { get; set; }
    bool TryUpdateEffect(Character character, string abilityName, float threshold, float value);
}
using System.Collections.Generic;

/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchStatusAbility : IAbility
{
    List<SearchStatusItem> SearchStats { get; set; }
    bool FindCharacterStatus(Character character, float threshold = 0.01f);
}
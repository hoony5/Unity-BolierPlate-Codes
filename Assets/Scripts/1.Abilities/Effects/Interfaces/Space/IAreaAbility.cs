using System.Collections.Generic;

/// <summary>
/// if character is on the Area, the effect will be applied. 
/// </summary>
public interface IAreaAbility : IAbility
{
    float Range { get; set; }
    bool TryCheckArea(Character character, int areaMask);
}
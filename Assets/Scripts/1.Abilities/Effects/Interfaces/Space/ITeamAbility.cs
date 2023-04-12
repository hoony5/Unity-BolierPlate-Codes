using System.Collections.Generic;

/// <summary>
/// if the Team within a count people, the effect will be applied.
/// </summary>
public interface ITeamAbility : IAbility
{
    bool BuffOrDebuff { get; set; }
    void UpdateAbility(Character[] characters);
}
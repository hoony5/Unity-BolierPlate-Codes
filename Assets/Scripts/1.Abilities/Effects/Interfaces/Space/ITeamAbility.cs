﻿using System.Collections.Generic;

/// <summary>
/// if the Team within a count people, the effect will be applied.
/// </summary>
public interface ITeamAbility : IAbility
{
    bool BuffOrDebuff { get; set; }
    void CalculateTeamStatus(Character character, EffectAbilityStat stat);
    void UpdateAbility(Character[] ourTeam, Character[] enemyTeam);
    void UpdateAbility(Character player, Character enemy);
    void UpdateForTeam(Character[] team, EffectAbilityStat stat);
}
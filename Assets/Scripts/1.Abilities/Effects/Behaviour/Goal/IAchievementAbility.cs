﻿using System.Collections.Generic;
using UnityEngine.TextCore.Text;

/// <summary>
/// effect will be applied when the Something is achieved.
/// </summary>
public interface IAchievementAbility : IAbility
{
    List<EffectAbility> Effects { get; set; }
    bool TryCheckAchievement(Character character, bool isAchieved);
}
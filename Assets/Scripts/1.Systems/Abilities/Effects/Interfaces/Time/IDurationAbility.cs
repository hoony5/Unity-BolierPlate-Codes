﻿/// <summary>
/// When the effect is applied to the character, it will be updated every frame.
/// </summary>
public interface IDurationAbility : IAbility
{
    float Duration { get; set; }
    bool HasTimePassed(float currentDuration);
}
﻿/// <summary>
/// effect that is applied when the character is motivated
/// </summary>
public interface IMotivatedAbility : IAbility
{
    MotivationInfo MotivationInfo { get; set; }
    void SetMotivationActive(Character character, Character orOther);
    void ResetStatus(Character character, MotivationStatusInfo motivationStatusInfo);
    void ApplyMotivationStatus(Character character, Character orOther, MotivationStatusInfo motivationStatusInfo);
    bool IsMotivatedWhenGreater(Character character, Character orOther);
    bool IsMotivatedWhenLess(Character character, Character orOther);
    bool IsMotivatedWhenApproximately(Character character, Character orOther, float threshold = 0.01f);
}
using System.Collections.Generic;

/// <summary>
/// effect that is applied when the character is motivated
/// </summary>
public interface IMotivatedAbility : IAbility
{
    float Motivation { get; set; }
    bool IsMotivatedWhenGreater(float motivation);
    bool IsMotivatedWhenLess(float motivation);
    bool IsMotivatedWhenApproximately(float motivation, float threshold = 0.01f);
}
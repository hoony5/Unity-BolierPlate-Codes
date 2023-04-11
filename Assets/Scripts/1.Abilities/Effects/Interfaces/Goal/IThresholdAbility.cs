/// <summary>
/// effect will be applied when the character's threshold is reached.
/// </summary>
public interface IThresholdAbility : IAbility
{
    float Threshold { get; set; }
    bool TryCheckThreshold(float threshold);
}
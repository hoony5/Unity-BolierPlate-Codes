/// <summary>
/// effect will be applied when the character's threshold is reached.
/// </summary>
public interface IThresholdEffect : IEffect
{
    void UpdateEffect(Character character, Character other, float threshold);
}
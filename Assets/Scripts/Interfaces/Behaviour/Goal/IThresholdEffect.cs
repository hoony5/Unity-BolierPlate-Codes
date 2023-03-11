/// <summary>
/// effect will be applied when the character's threshold is reached.
/// </summary>
public interface IThresholdEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, float threshold);
}
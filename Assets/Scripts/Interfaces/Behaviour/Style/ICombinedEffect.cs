/// <summary>
/// effect will be applied when the things are combined.
/// </summary>
public interface ICombinedEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, bool isCombined);
}
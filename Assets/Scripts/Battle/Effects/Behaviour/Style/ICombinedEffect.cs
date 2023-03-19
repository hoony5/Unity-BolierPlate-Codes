/// <summary>
/// effect will be applied when the things are combined.
/// </summary>
public interface ICombinedEffect : IEffect
{
    void UpdateEffect(Character character, Character other, bool isCombined);
}
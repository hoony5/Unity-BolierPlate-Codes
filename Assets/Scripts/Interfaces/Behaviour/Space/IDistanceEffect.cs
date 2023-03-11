/// <summary>
/// if other character is within a certain distance, the effect will be applied.
/// </summary>
public interface IDistanceEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, float distanceThreshold);
}
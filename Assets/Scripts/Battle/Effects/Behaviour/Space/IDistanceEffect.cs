/// <summary>
/// if other character is within a certain distance, the effect will be applied.
/// </summary>
public interface IDistanceEffect : IEffect
{
    public float Range { get; set; }
    void UpdateEffect(Character character, Character other, float distanceThreshold);
}
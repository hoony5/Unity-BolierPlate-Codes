/// <summary>
/// effect that is applied when the character is motivated
/// </summary>
public interface IMotivatedEffect : IEffect
{
    void UpdateEffect(Character character, Character other, bool isMotivated);
}
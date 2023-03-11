/// <summary>
/// effect that is applied when the character is motivated
/// </summary>
public interface IMotivatedEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, bool isMotivated);
}
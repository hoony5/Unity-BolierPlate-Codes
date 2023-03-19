/// <summary>
/// when the effect is applied to the character, it will be applied immediately.
/// </summary>
public interface IImmediateEffect : IEffect
{
    void UpdateEffect(Character character, Character other);
}
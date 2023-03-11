/// <summary>
/// when the effect is applied to the character, it will be applied immediately.
/// </summary>
public interface IConditionEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, bool condition);
}
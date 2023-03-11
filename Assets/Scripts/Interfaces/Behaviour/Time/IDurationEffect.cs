/// <summary>
/// When the effect is applied to the character, it will be updated every frame.
/// </summary>
public interface IDurationEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, float deltaTime);
}
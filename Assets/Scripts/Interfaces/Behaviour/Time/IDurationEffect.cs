/// <summary>
/// When the effect is applied to the character, it will be updated every frame.
/// </summary>
public interface IDurationEffect : IEffect
{
    void UpdateEffect(Character character,Character other, float deltaTime);
}
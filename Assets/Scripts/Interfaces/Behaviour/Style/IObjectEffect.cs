/// <summary>
/// effect will be applied when the character uses the object.
/// </summary>
public interface IObjectEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, bool isUseTheObject);
}
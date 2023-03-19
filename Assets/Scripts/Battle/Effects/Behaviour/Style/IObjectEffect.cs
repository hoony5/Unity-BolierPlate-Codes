/// <summary>
/// effect will be applied when the character uses the object.
/// </summary>
public interface IObjectEffect : IEffect
{
    void UpdateEffect(Character character, Character other, bool isUseTheObject);
}
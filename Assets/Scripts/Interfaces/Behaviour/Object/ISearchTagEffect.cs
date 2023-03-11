/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchTagEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, string tag);
}
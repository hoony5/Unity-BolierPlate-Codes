/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchTagEffect : IEffect
{
    string SearchTag { get; set; }
    void UpdateEffect(Character character, Character other, string tag);
}
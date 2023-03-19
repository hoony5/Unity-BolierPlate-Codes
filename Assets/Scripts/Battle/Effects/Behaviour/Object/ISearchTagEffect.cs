/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchTagEffect : IEffect
{
    string SearchTag { get; set; }
    bool TryCheckTag(Character other, string tag);
}
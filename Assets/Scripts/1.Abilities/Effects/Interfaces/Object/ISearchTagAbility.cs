/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
public interface ISearchTagAbility : IAbility
{
    string SearchTag { get; set; }
    bool FindTag(Character other);
}
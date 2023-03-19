/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
[ToDo("state filter convert to Enum of Behaviour Type of state")]
public interface ISearchStateEffect : IEffect
{
    string SearchState { get; set; }
    void UpdateEffect(Character character, Character other, string state);
}
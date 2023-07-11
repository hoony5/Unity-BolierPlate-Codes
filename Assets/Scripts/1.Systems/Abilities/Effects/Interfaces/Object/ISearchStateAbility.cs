/// <summary>
/// find all objects that match the filter and apply the effect to them.
/// </summary>
[ToDo("state filter convert to Enum of Behaviour Type of state")]
public interface ISearchStateAbility : IAbility
{
    string SearchState { get; set; }
    bool FindCharacterState(Character character);
}
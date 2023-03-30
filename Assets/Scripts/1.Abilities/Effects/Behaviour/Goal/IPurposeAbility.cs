/// <summary>
/// if purpose is matched, the effect will be applied.
/// </summary>
[ToDo("purpose filter convert to Enum of Behaviour Type of purpose")]
public interface IPurposeAbility : IAbility
{
    bool TryCheckPurpose(Character character, string purposeFilter);
}
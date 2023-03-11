/// <summary>
/// if purpose is matched, the effect will be applied.
/// </summary>
[ToDo("purpose filter convert to Enum of Behaviour Type of purpose")]
public interface IPurposeEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, string purposeFilter);
}
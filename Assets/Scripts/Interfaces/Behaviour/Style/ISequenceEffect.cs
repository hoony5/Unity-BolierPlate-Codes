/// <summary>
/// effect will be applied when the character is in the sequence.
/// </summary>
public interface ISequenceEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, bool moveNext);
}
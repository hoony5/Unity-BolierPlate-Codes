/// <summary>
/// if the Team within a count people, the effect will be applied.
/// </summary>
public interface ITeamEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, int count);
}
/// <summary>
/// effect will be applied when the Something is achieved.
/// </summary>
public interface IAchievementEffect : IEffect
{
    void UpdateEffect(CharacterBehaviour character, bool isAchieved);
}
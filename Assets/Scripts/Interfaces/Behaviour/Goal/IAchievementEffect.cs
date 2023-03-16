/// <summary>
/// effect will be applied when the Something is achieved.
/// </summary>
public interface IAchievementEffect : IEffect
{
    void UpdateEffect(Character character, Character other, bool isAchieved);
}
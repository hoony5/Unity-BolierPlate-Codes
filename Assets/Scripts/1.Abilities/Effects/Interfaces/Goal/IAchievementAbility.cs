/// <summary>
/// effect will be applied when the Something is achieved.
/// </summary>
public interface IAchievementAbility : IAbility
{
    bool TryCheckAchievement(Character character, bool isAchieved);
}
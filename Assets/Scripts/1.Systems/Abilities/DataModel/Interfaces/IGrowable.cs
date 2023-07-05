public interface IGrowable
{
    int Level { get; }
    int MaxLevel { get; }
    int CurrentExp { get; }
    int MaxExp { get; }
    Status StatusAbility { get; }
    void ResetLevel();
    void ResetExp();
    void AddLevel(int level);
    bool TryAddExp(int exp);
    void LevelUp();
    public void SetBaseInfo(int maxLevel, int maxExp);
}
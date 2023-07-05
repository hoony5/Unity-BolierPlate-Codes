public interface IEnhancable
{
     bool IsDisabled { get; }
     int Level { get; }
     int MaxLevel { get; }
     int CurrentExp { get; }
     int MaxExp { get; }
     bool Enhance(int enhanceLevel);
     bool Broke();
     bool Repair();
     void SetBaseInfo(int maxLevel, int maxExp);
}
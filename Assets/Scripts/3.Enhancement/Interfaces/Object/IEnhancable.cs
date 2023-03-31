public interface IEnhancable
{
    [ToDo("Add a method to check if the object can be enhancable")]
    bool TryCheckEnhancable(int nextLevel);

    void Updgrade();
    void Downgrade();
    void ResetStatus();
}
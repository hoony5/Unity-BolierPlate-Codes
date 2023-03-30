public interface IEnhancable
{
    [ToDo("Add a method to check if the object can be enhancable")]
    bool TryCheckEnhancable();

    void Updgrade();
    void Downgrade();
    void ResetStatus();
}
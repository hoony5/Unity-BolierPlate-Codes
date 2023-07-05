public interface ITranscendencable
{
    [ToDo("Add a method to check if the object can be transcended")]
    bool TryCheckTranscendencable(int nextLevel);

    void Updgrade();
    void Downgrade();
    void ResetStatus();
}
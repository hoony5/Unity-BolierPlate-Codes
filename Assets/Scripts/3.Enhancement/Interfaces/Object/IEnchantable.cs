public interface IEnchantable
{
    [ToDo("Add a method to check if the object can be enchantable")]
    bool TryCheckEnchantable(int nextLevel);

    void Updgrade();
    void Downgrade();
    void ResetStatus();
}
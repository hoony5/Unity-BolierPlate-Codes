public interface IAbility
{
    string Description { get;}
    bool IsStackable { get; set; }
    int StackCount { get; set; }
}

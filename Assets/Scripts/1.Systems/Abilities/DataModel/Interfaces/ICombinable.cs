public interface ICombinable
{
    [ToDo("Convert ICombinable to IMaterial")]
    bool Assemble(ICombinable[] combinables);
    bool IsCombinableWith(string id);
    void Combine();
}
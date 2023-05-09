public interface ICombinable
{
    bool Assemble(ICombinable[] combinables);
    bool IsCombinableWith(string id);
    void Combine();
}
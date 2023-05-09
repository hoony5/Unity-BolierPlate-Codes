public interface IUsable
{
    bool Use(int count);
    bool Abandon(int count);
    bool Fill(int count);
    void Clear();
}
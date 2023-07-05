using UnityEngine.Events;

public interface IUsable
{
    UnityEvent OnUse { get; }
    bool Use(int count);
    bool Abandon(int count);
    bool Fill(int count);
}

public abstract class Command : ICommand
{
    public abstract bool Execute();
    public abstract bool Undo();
}

public abstract class Command<T> : ICommand<T>
{
    public abstract bool Execute(T input);
    public abstract bool Undo(T input);
}


public abstract class Command : ICommand
{
    public abstract bool Excecute();
    public abstract bool Undo();
}

public abstract class Command<T> : ICommand<T>
{
    public abstract bool Excecute(T input);
    public abstract bool Undo(T input);
}

public interface ICommand
{
    bool Execute();
    bool Undo();
}
public interface ICommand<T>
{
    bool Execute(T input);
    bool Undo(T input);
}
public interface ICommand
{
    bool Excecute();
    bool Undo();
}
public interface ICommand<T>
{
    bool Excecute(T input);
    bool Undo(T input);
}
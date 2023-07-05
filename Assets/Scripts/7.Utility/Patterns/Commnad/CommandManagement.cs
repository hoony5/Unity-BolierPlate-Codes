using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class CommandManagement
{
    private Stack<ICommand> _commands;
#if UNITY_EDITOR
    [ReadOnly, SerializeReference] ICommand[] _commandsBuffer; 
#endif

    public CommandManagement()
    {
        _commands = new Stack<ICommand>();
    }
    
    public void Execute(ICommand command)
    {
        if (!command.Execute()) return;
        _commands.Push(command);
        
#if UNITY_EDITOR
        _commandsBuffer = _commands.ToArray();
#endif
    }
    
    public void Undo()
    {
        if (_commands.Count == 0) return;
        bool exist = _commands.TryPop(out ICommand result);
        
        if (exist) result.Undo();
#if UNITY_EDITOR
        _commandsBuffer = _commands.ToArray();
#endif
    }
}

public class CommandManagement<T>
{
    private Stack<ICommand<T>> _commands;
#if UNITY_EDITOR
    [ReadOnly, SerializeReference] ICommand<T>[] _commandsBuffer; 
#endif
    
    public CommandManagement()
    {
        _commands = new Stack<ICommand<T>>();
    }
    public void Execute(ICommand<T> command, T value)
    {
        if (!command.Execute(value)) return;
        _commands.Push(command);
#if UNITY_EDITOR
        _commandsBuffer = _commands.ToArray();
#endif
    }
    
    public void Undo(T value)
    {
        if (_commands.Count == 0) return;
        bool exist = _commands.TryPop(out ICommand<T> command);
        if(exist) command.Undo(value);
#if UNITY_EDITOR
        _commandsBuffer = _commands.ToArray();
#endif
    }
}

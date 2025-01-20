using System.Windows.Input;

namespace Mffm.Commands;

public class CompositeCommand : ICommand
{
    private readonly ICommand[] _commands;

    public CompositeCommand(params ICommand[] commands)
    {
        _commands = commands ?? [];

        // let's hand over the notification that properties have changed
        foreach (var command in _commands)
            command.CanExecuteChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
    }

    public bool CanExecute(object? parameter)
    {
        return _commands.All(c => c.CanExecute(parameter));
    }

    public void Execute(object? parameter)
    {
        foreach (var command in _commands) command.Execute(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}
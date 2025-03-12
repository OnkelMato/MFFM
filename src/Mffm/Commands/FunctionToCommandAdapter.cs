using System.Windows.Input;

namespace Mffm.Commands;

/// <summary>
///     This adapter does not support the CanExecuteChanged event pass-through and property changes in model
/// </summary>
public class FunctionToCommandAdapter(Action<object> execute, Predicate<object>? canExecute = null)
    : ICommand
{
    private readonly Predicate<object> _canExecute = canExecute ?? (_ => true);
    private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    /// <inheritdoc />
    public bool CanExecute(object? context)
    {
        return _canExecute(context!);
    }

    /// <inheritdoc />
    public void Execute(object? context)
    {
        _execute(context!);
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;
}
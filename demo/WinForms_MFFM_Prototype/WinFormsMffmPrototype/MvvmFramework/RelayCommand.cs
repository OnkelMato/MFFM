using System.Windows.Input;

namespace WinFormsMffmPrototype.MvvmFramework;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
    }

    public bool CanExecute(object? context)
    {
        return _canExecute(context!);
    }

    public void Execute(object? context)
    {
        _execute(context!);
    }

    public event EventHandler? CanExecuteChanged;
}
using System.Windows.Input;

namespace WinFormsMffmPrototype.MvvmFramework;

public class CloseFormCommand : ICommand
{
    private readonly IWindowManager _windowManager;

    public CloseFormCommand(IWindowManager windowManager)
    {
        _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
    }

    public bool CanExecute(object? parameter)
    {
        // maybe a cast is useful. and winman should know which form belongs to a model
        return true; // _windowManager.IsFormOpen(ViewModel!);
    }

    public void Execute(object? parameter)
    {
        var model = parameter as IFormModel;
        if (parameter == null)
            throw new ArgumentNullException(nameof(parameter), "It seems that the CommandParameter in Binding is not set to the model");

        _windowManager.CloseForm(parameter!);
    }

    public event EventHandler? CanExecuteChanged;
}
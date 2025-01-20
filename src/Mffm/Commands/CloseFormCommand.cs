using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Commands;

public class CloseFormCommand(IWindowManager windowManager) : ICommand
{
    private readonly IWindowManager _windowManager =
        windowManager ?? throw new ArgumentNullException(nameof(windowManager));

    public bool CanExecute(object? parameter)
    {
        var model = parameter as IFormModel;
        if (model == null)
            return false;

        return _windowManager.IsFormOpen(model!);
    }

    public void Execute(object? parameter)
    {
        var model = parameter as IFormModel;
        if (model == null)
            throw new ArgumentNullException(nameof(parameter),
                "It seems that the CommandParameter in Binding is not set to the model");

        _windowManager.Close(model);
    }

    public event EventHandler? CanExecuteChanged;
}
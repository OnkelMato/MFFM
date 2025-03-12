using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Commands;

/// <summary>
/// Command to close a form. It checks if the form is open and then closes it.
/// Thw window manager is responsible for the form management.
/// </summary>
/// <param name="windowManager"></param>
public class CloseFormCommand(IWindowManager windowManager) : ICommand
{
    private readonly IWindowManager _windowManager =
        windowManager ?? throw new ArgumentNullException(nameof(windowManager));

    /// <summary>
    /// Checks if the form is open and can be closed.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter)
    {
        var model = parameter as IFormModel;
        if (model == null)
            return false;

        return _windowManager.IsFormOpen(model!);
    }

    /// <summary>
    /// Closes the form.
    /// </summary>
    /// <param name="parameter"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Execute(object? parameter)
    {
        var model = parameter as IFormModel;
        if (model == null)
            throw new ArgumentNullException(nameof(parameter),
                "It seems that the CommandParameter in Binding is not set to the model");

        // Get property result with reflection from the formModel so we set this to the 
        var dialogResultProperty = (DialogResult)(model.GetType().GetProperty(MffmConstants.DialogResultPropertyName)?.GetValue(model) ?? DialogResult.None);

        _windowManager.Close(model, dialogResultProperty);
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Event handler for the CanExecuteChanged event.
    /// </summary>
    public event EventHandler? CanExecuteChanged;
}
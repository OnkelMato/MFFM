using System.Diagnostics;
using System.Windows.Input;

namespace Mffm.Commands;

/// <summary>
/// Decorator to set the DialogResult property of the model before executing the inner command.
/// </summary>
public class SetResultDecorator : ICommand
{
    private readonly ICommand _command;
    private readonly DialogResult _dialogResult;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="dialogResult"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SetResultDecorator(ICommand command, DialogResult dialogResult)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _dialogResult = dialogResult;

        _command.CanExecuteChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter)
    {
        return _command.CanExecute(parameter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object? parameter)
    {
        // get the DialogResult property from the model
        var property = parameter?.GetType().GetProperty(MffmConstants.DialogResultPropertyName);
        if (property != null)
            property.SetValue(parameter, _dialogResult);
        else
            Trace.TraceWarning($"A dialog result is used in '{nameof(SetResultDecorator)}' for parameter '{parameter?.GetType().Name??"<null>"}' but the property '{MffmConstants.DialogResultPropertyName}' could not be found.");

        // let's execute the inner command
        _command.Execute(parameter);
    }

    /// <summary>
    /// 
    /// </summary>
    public event EventHandler? CanExecuteChanged;
}
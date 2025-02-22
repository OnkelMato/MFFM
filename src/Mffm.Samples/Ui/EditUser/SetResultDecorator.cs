using System.Windows.Input;

namespace Mffm.Samples.Ui.EditUser;

public class SetResultDecorator : ICommand
{
    private readonly ICommand _command;
    private readonly DialogResult _dialogResult;

    public SetResultDecorator(ICommand command, DialogResult dialogResult)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _dialogResult = dialogResult;

        _command.CanExecuteChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
    }

    public bool CanExecute(object? parameter)
    {
        return _command.CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        // get the DialogResult property from the model
        var dialogResultProperty = parameter?.GetType().GetProperty("DialogResult");
        if (dialogResultProperty != null)
            dialogResultProperty.SetValue(parameter, _dialogResult);

        if (parameter is not EditFormModel model) return;
        model.DialogResult = _dialogResult;

        _command.Execute(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}
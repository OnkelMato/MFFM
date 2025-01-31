using System.ComponentModel;
using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Commands;

/// <summary>
///     class used for data binding in the MFFM framework. The command parameter is the model itself.
/// </summary>
public class FormModelAsParameterCommandDecorator : ICommand
{
    // todo check of this can interop with the adapter to add "notify" functionality

    private readonly ICommand _command;
    private readonly IFormModel _model;

    public FormModelAsParameterCommandDecorator(ICommand command, IFormModel model)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _model = model ?? throw new ArgumentNullException(nameof(model));

        // let's hand over the notification that properties have changed
        command.CanExecuteChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
        if (model is INotifyPropertyChanged notifyPropertyChanged)
            notifyPropertyChanged.PropertyChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
    }

    public bool CanExecute(object? parameter)
    {
        return _command.CanExecute(_model);
    }

    public void Execute(object? parameter)
    {
        _command.Execute(_model);
    }

    public event EventHandler? CanExecuteChanged;
}
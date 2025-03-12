using System.ComponentModel;
using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Commands;

/// <summary>
///     class used for data binding in the MFFM framework. The command parameter is the model itself.
/// </summary>
public class FormModelDecorator : ICommand
{
    private readonly ICommand _command;
    private readonly IFormModel _model;

    /// <summary>
    ///    Initializes a new instance of the <see cref="FormModelDecorator" /> class.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="model"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public FormModelDecorator(ICommand command, IFormModel model)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _model = model ?? throw new ArgumentNullException(nameof(model));

        // let's hand over the notification that properties have changed
        command.CanExecuteChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
        if (model is INotifyPropertyChanged notifyPropertyChanged)
            notifyPropertyChanged.PropertyChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);

        // execute CanExecuteChanged once to set the initial state
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public bool CanExecute(object? parameter)
    {
        return _command.CanExecute(_model);
    }

    /// <inheritdoc />
    public void Execute(object? parameter)
    {
        _command.Execute(_model);
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;
}
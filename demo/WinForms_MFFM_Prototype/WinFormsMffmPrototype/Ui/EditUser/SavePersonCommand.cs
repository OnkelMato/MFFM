using System.Windows.Input;
using MediatR;

namespace WinFormsMffmPrototype.Ui.EditUser;

public class SavePersonCommand : ICommand
{
    private readonly IMediator _mediator;

    public SavePersonCommand(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not EditFormModel model) return;

        // todo does this makes sense?
        File.AppendAllText(model.Id.ToString() + ".txt", model.Firstname);
        File.AppendAllText(model.Id.ToString() + ".txt", model.Lastname);
    }

    public event EventHandler? CanExecuteChanged;
}
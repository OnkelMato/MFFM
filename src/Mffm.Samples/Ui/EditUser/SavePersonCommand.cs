using System.Windows.Input;

namespace Mffm.Samples.Ui.EditUser;

public class SavePersonCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not EditFormModel model) return;

        // todo does this makes sense?
        File.AppendAllText(model.Id + ".txt", model.Firstname);
        File.AppendAllText(model.Id + ".txt", model.Lastname);
    }

    public event EventHandler? CanExecuteChanged;
}
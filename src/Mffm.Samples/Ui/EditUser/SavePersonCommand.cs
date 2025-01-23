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

        // Logic to save the person
    }

    public event EventHandler? CanExecuteChanged;
}
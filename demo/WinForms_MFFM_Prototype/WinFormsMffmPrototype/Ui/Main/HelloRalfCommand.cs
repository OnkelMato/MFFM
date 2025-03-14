using System.Windows.Input;

namespace WinFormsMffmPrototype.Ui.Main;

public class HelloRalfCommand : ICommand
{
    bool _canExecute = true;

    public bool CanExecute(object? parameter)
    {
        return _canExecute;
    }

    public void Execute(object? parameter)
    {
        MessageBox.Show("Hello Ralf");

        _canExecute = false;
        CanExecuteChanged(this, EventArgs.Empty);
    }

    public event EventHandler? CanExecuteChanged;
}
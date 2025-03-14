using System.ComponentModel;
using System.Windows.Input;

namespace WinFormsMffmPrototype.MvvmFramework
{
    public class FormModelAsParameterCommandDecorator : ICommand
    {
        private readonly ICommand _command;
        private readonly object? _model;

        public FormModelAsParameterCommandDecorator(ICommand command, object model)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _model = model ?? throw new ArgumentNullException(nameof(model));

            // Änderungen am Model oder am Command sollen die Ausführbarkeit des Commands beeinflussen
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
}

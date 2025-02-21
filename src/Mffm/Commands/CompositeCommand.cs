using System.Windows.Input;

namespace Mffm.Commands
{
    /// <inheritdoc />
    public class CompositeCommand : ICommand
    {
        private readonly ICommand[] _commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeCommand"/> class.
        /// </summary>
        /// <param name="commands"></param>
        public CompositeCommand(params ICommand[] commands)
        {
            _commands = commands ?? [];

            // let's hand over the notification that properties have changed
            foreach (var command in _commands)
                command.CanExecuteChanged += (sender, args) => CanExecuteChanged?.Invoke(this, args);
        }

        /// <inheritdoc />
        public bool CanExecute(object? parameter)
        {
            return _commands.All(c => c.CanExecute(parameter));
        }

        /// <inheritdoc />
        public void Execute(object? parameter)
        {
            foreach (var command in _commands) command.Execute(parameter);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;
    }
}
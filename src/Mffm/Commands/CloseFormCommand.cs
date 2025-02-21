using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Commands
{
    /// <inheritdoc />
    public class CloseFormCommand(IWindowManager windowManager) : ICommand
    {
        private readonly IWindowManager _windowManager =
            windowManager ?? throw new ArgumentNullException(nameof(windowManager));

        /// <inheritdoc />
        public bool CanExecute(object? parameter)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (parameter is not IFormModel model)
                return false;

            return _windowManager.IsFormOpen(model!);
        }

        /// <inheritdoc />
        public void Execute(object? parameter)
        {
            var model = parameter as IFormModel;
            if (model == null)
                throw new ArgumentNullException(nameof(parameter),
                    "It seems that the CommandParameter in Binding is not set to the model");

            _windowManager.Close(model);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;
    }
}
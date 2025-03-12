using System;
using System.Diagnostics;
using System.Windows.Input;
using Mffm.Contracts;

namespace LinkManager48.FormModels.Commands
{
    internal class CreateCategoryCommand : ICommand
    {
        private readonly IWindowManager _windowManager;

        public CreateCategoryCommand(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var result = _windowManager.ShowModal<CreateCategoryFormModel>();
            Debug.WriteLine($"Category created result was: {result.ToString()}");
        }

        public event EventHandler CanExecuteChanged;
    }
}
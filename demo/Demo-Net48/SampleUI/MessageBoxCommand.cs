using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace SampleUI
{
    public class MessageBoxCommand : ICommand
    {

        private bool _neverExecuted = true;
        public bool CanExecute(object parameter)
        {
            return _neverExecuted;
        }

        public void Execute(object parameter)
        {
            _neverExecuted = false;
            OnCanExecuteChanged();

            MessageBox.Show(@"Hello World!");
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
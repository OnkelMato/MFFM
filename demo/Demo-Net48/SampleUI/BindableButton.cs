using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace SampleUI
{
    public class BindableButton : Button
    {
        private ICommand _command;

        public BindableButton()
            : base()
        {
            this.Click += BindableButton_Click;
        }

        [Bindable(BindableSupport.Yes)]
        public ICommand Command
        {
            get => _command;
            set => SetCommand(value);
        }

        private EventHandler _canExecuteChanged;
        private void SetCommand(ICommand value)
        {
            if (_canExecuteChanged != null)
            {
                Command.CanExecuteChanged -= _canExecuteChanged;
                _canExecuteChanged = null; // unregistered so it can be removed
            }

            _command = value;

            if (_command == null) return;

            // in case we have a command, we regsiter the event
            _canExecuteChanged = (sender, args) => this.Enabled = _command.CanExecute(this.CommandParameter);
            _command.CanExecuteChanged += _canExecuteChanged;
        }

        [Bindable(BindableSupport.Yes)]
        public object CommandParameter { get; set; }

        private void BindableButton_Click(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
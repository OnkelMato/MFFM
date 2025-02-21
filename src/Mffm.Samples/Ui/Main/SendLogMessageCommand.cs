using System.Windows.Input;
using Mffm.Contracts;
using Mffm.Core;
using Mffm.Samples.Ui.Protocol;

namespace Mffm.Samples.Ui.Main
{
    public class SendLogMessageCommand(IEventAggregator eventAggregator) : ICommand
    {
        private readonly IEventAggregator _eventAggregator =
            eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

        public bool CanExecute(object? parameter)
        {
            var model = parameter as IFormModel;
            if (model == null)
                throw new ArgumentNullException(nameof(parameter),
                    "It seems that the CommandParameter in Binding is not set to the model");

            return true;
        }

        public void Execute(object? parameter)
        {
            var model = parameter as MainFormModel;
            if (model == null)
                throw new ArgumentNullException(nameof(parameter),
                    "It seems that the CommandParameter in Binding is not set to the model");

            var message = new LogMessage { Message = model.LogMessageToSend };
            _eventAggregator.Publish(message);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
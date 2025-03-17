using System.Windows.Input;
using Mffm.Contracts;
using Mffm.Core;
using Mffm.Samples.Ui.Protocol;

namespace Mffm.Samples.Ui.Main;

public class SendLogMessageCommand(IPublish<LogMessage> publisher) : ICommand
{
    private readonly IPublish<LogMessage> _eventAggregator =
        publisher ?? throw new ArgumentNullException(nameof(publisher));

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
        _eventAggregator.PublishAsync(message, CancellationToken.None).Wait();
    }

    public event EventHandler? CanExecuteChanged;
}
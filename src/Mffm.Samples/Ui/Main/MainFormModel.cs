using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Samples.Ui.EditUser;
using Mffm.Samples.Ui.Protocol;

namespace Mffm.Samples.Ui.Main;

public class MainFormModel : IFormModel, INotifyPropertyChanged, IHandle<LogMessage>
{
    private string _logMessages = string.Empty;
    private string _logMessageToSend = string.Empty;
    private string _peopleSelected;

    public MainFormModel(
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
    {
        eventAggregator.Subscribe(this);

        // this happens when you don't have a command and can use window manager directly
        MenuFileClose = new FunctionToCommandAdapter(_ => windowManager.Close(this));
        MenuEditPerson = new FunctionToCommandAdapter(_ => windowManager.Show<EditFormModel>());
        MenuEditProtocol = new FunctionToCommandAdapter(_ => windowManager.Show<ProtocolFormModel>());
        SendLogMessage = new SendLogMessageCommand(eventAggregator);
    }

    #region Handle Incoming Messages

    public Task HandleAsync(LogMessage message, CancellationToken cancellationToken)
    {
        LogMessages = string.Join(Environment.NewLine, message.Message, LogMessages);
        return Task.CompletedTask;
    }

    #endregion

    #region Properties to bind

    public ICommand MenuEditProtocol { get; private set; }
    public ICommand MenuFileClose { get; private set; }
    public ICommand MenuEditPerson { get; private set; }
    public ICommand SendLogMessage { get; private set; }

    public string LogMessages
    {
        get => _logMessages;
        set
        {
            if (value == _logMessages) return;
            _logMessages = value;
            OnPropertyChanged();
        }
    }

    public string LogMessageToSend
    {
        get => _logMessageToSend;
        set
        {
            if (value == _logMessageToSend) return;
            _logMessageToSend = value;
            OnPropertyChanged();
        }
    }

    public IList<string> People { get; } = new List<string> { "Alice", "Bob", "Charlie" };

    public string PeopleSelected
    {
        get => _peopleSelected;
        set
        {
            if (value == _peopleSelected) return;
            _peopleSelected = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinFormsMffmPrototype.MvvmFramework;
using WinFormsMffmPrototype.MvvmFramework.EventAggregators;

namespace WinFormsMffmPrototype.Ui.Protocol;

public class ProtocolFormModel : INotifyPropertyChanged, IHandle<LogMessage>, IHandle<ErrorMessage>
{
    private readonly CloseFormCommand _closeFormCommand;
    private readonly IEventAggregator _eventAggregator;

    [Obsolete("Only for design time", true)]
    public ProtocolFormModel()
    {
        
    }

    public ProtocolFormModel(CloseFormCommand closeFormCommand, IEventAggregator eventAggregator)
    {
        _closeFormCommand = closeFormCommand ?? throw new ArgumentNullException(nameof(closeFormCommand));
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

        _eventAggregator.Subscribe(this);
    }

    private string _log = string.Empty;
    public event PropertyChangedEventHandler? PropertyChanged = (sender, args) => { };

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand CloseWindow => _closeFormCommand;

    public string Log
    {
        get => _log;
        set
        {
            if (value == _log) return;
            _log = value;
            OnPropertyChanged();
        }
    }

    public Task HandleAsync(LogMessage message, CancellationToken cancellationToken)
    {
        Log = string.Join(Environment.NewLine, message.Message, Log);
        return Task.CompletedTask;
    }

    public Task HandleAsync(ErrorMessage message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinFormsMffmPrototype.Core.Services;
using WinFormsMffmPrototype.MvvmFramework;
using WinFormsMffmPrototype.MvvmFramework.EventAggregators;
using WinFormsMffmPrototype.Ui.EditUser;
using WinFormsMffmPrototype.Ui.Protocol;

namespace WinFormsMffmPrototype.Ui.Main;

public class MainFormModel : IFormModel, INotifyPropertyChanged
{
    private readonly IGreetingRepository _greetings;
    private readonly IDateTimeProvider _dateTime;
    private readonly IWindowManager _windowManager;
    private readonly IEventAggregator _eventAggregator;
    private string _name;

    [Obsolete("only for design time")]
    public MainFormModel()
    {
        
    }
    public MainFormModel(
        IGreetingRepository greetings,
        IDateTimeProvider dateTime,
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
    {
        _greetings = greetings ?? throw new ArgumentNullException(nameof(greetings));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

        Name = "Thomas";
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged("Name");
            OnPropertyChanged(nameof(CanDoSomethingElse));
        }
    }




    public void Save()
    {
        _eventAggregator.Publish(
            new LogMessage() { Message = $"{_dateTime.Now}\t{_greetings.GetGreeting(_dateTime)} {this.Name}" }
        );
        _eventAggregator.Publish(
            new LogMessage() { Message = $"{_dateTime.Now}\t{_greetings.GetGreeting(_dateTime)} {this.Name}" }
        );
        MessageBox.Show($"{_greetings.GetGreeting(_dateTime)} {Name}");
    }








    public void DoSomethingElse(object context)
    {
        MessageBox.Show($"Your name is {Name}");
    }

    public bool CanDoSomethingElse(object context)
    {
        return !string.IsNullOrWhiteSpace(Name);
    }


    public void OpenProtocolForm(object context)
    {
        // irgendeine Abhängigkeit wird benötigt ... in der Regel das ViewModel
        _windowManager.Show<ProtocolForm>();
    }

    public void OpenEditForm(object context)
    {
        // irgendeine Abhängigkeit wird benötigt ... in der Regel das ViewModel
        _windowManager.Show<EditForm>();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public IFormModel FormModel => this;
}
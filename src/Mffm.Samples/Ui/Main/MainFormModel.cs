using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core.Bindings;
using Mffm.Samples.Properties;
using Mffm.Samples.Ui.EditUser;
using Mffm.Samples.Ui.Protocol;

namespace Mffm.Samples.Ui.Main;

public class MainFormModel : IFormModel, INotifyPropertyChanged, IHandle<LogMessage>
{
    private readonly IWindowManager _windowManager;
    private const string TitleDefault = "MFFM Sample Application";

    private string _lastLogMessage = string.Empty;
    private string _logMessages = string.Empty;
    private string _logMessageToSend = string.Empty;
    private string _peopleSelected = string.Empty;
    private string _title;
    private TreeViewNodeModel _folderTreeViewSelected;
    private IEnumerable<string> _foldersItems;
    private BindingList<TreeViewNodeModel> _folderTreeView;
    private string _folders;

    public MainFormModel(
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
    {
        _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        eventAggregator.Subscribe(this);

        // this happens when you don't have a command and can use window manager directly
        MenuFileClose = new CloseApplicationCommand(windowManager);
        MenuEditPerson = new FunctionToCommandAdapter(_ => ShowPersonDialog());
        MenuEditProtocol = new FunctionToCommandAdapter(_ => windowManager.Show<ProtocolFormModel>());

        // use the regular command for the menu
        SendLogMessageMenu = new SendLogMessageCommand(eventAggregator);
        SendLogMessageMenuIcon = Image.FromStream(new MemoryStream(Resources.icon_senden));

        SendLogMessage = new SendLogMessageCommand(eventAggregator);

        _title = TitleDefault;

        FolderTreeView = new BindingList<TreeViewNodeModel>();
        FillTreeViewNodes(Environment.CurrentDirectory);

        _foldersItems = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar)
            .Concat(Directory.GetDirectories(Path.GetTempPath())).ToArray();
        _folders = Environment.CurrentDirectory;
    }

    private void FillTreeViewNodes(string directory)
    {
        FolderTreeView.Clear();
        var files = Directory.GetFiles(directory, "*.*", SearchOption.TopDirectoryOnly).Select(x => new FileInfo(x)).ToArray();
        var extensions = files.Select(d => d.Extension).Distinct().ToArray();
        extensions.Select(d => new TreeViewNodeModel() { Text = d }).ToList().ForEach(x => FolderTreeView.Add(x));
        files.ToList().ForEach(f =>
        {
            var node = FolderTreeView.First(x => x.Text == f.Extension);
            node.Children.Add(new TreeViewNodeModel() { Text = f.Name, Data = f });
        });
    }

    private void ShowPersonDialog()
    {
        var ctx = new EditFormModelContext() { Firstname = PeopleSelected };

        var result = _windowManager.ShowModal<EditFormModel>(ctx);
        if (result == DialogResult.OK)
            PeopleSelected = ctx.Firstname;
    }

    public Image SendLogMessageMenuIcon { get; set; }

    #region Handle Incoming Messages

    public Task HandleAsync(LogMessage message, CancellationToken cancellationToken)
    {
        LogMessages = string.Join(Environment.NewLine, message.Message, LogMessages);
        Title = $"{TitleDefault} ({message.Message})";
        LastLogMessage = message.Message;
        return Task.CompletedTask;
    }

    #endregion

    #region Properties to bind

    public IEnumerable<string> FoldersItems
    {
        get => _foldersItems;
        set
        {
            if (Equals(value, _foldersItems)) return;
            _foldersItems = value;
            OnPropertyChanged();
        }
    }

    public string Folders
    {
        get => _folders;
        set
        {
            if (value == _folders) return;
            _folders = value;
            OnPropertyChanged();
            FillTreeViewNodes(value);
        }
    }

    public BindingList<TreeViewNodeModel> FolderTreeView
    {
        get => _folderTreeView;
        set
        {
            if (Equals(value, _folderTreeView)) return;
            _folderTreeView = value;
            OnPropertyChanged();
        }
    }

    public TreeViewNodeModel FolderTreeViewSelected
    {
        get => _folderTreeViewSelected;
        set
        {
            if (Equals(value, _folderTreeViewSelected)) return;
            _folderTreeViewSelected = value;
            OnPropertyChanged();
        }
    }

    public ICommand MenuEditProtocol { get; private set; }
    public ICommand MenuFileClose { get; private set; }
    public ICommand MenuEditPerson { get; private set; }
    public ICommand SendLogMessage { get; private set; }
    public ICommand SendLogMessageMenu { get; set; }

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

    public string LastLogMessage
    {
        get => _lastLogMessage;
        set
        {
            if (value == _lastLogMessage) return;
            _lastLogMessage = value;
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

    #region Form Properties

    public string Title
    {
        get => _title;
        set
        {
            if (value == _title) return;
            _title = value;
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
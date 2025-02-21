using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;
using YxtEditor.Essential.Commands;
using YxtEditor.Essential.Messages;
using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.FormModels
{
    internal sealed class MainFormModel :
        IFormModel, INotifyPropertyChanged,
        IHandle<DocumentLoaded>, IHandle<LogMessage>, IHandle<DocumentSaved>,
        IHaveYxtDocument
    {
        private string _title = YxtConstants.ApplicationName;
        private string _contents = string.Empty;
        private string _lastLogEvent = string.Empty;

        public MainFormModel(IEventAggregator eventAggregator, ICommandResolver commandResolver)
        {
            // hint: for menu items, the formmodel is already the context by default.
            CloseApplication = commandResolver.ResolveCommand<CloseFormCommand>();// todo: this does not work in case there are pending changes.
            OpenDocument = commandResolver.ResolveCommand<OpenDocumentCommand>();
            SaveAsDocument = commandResolver.ResolveCommand<SaveAsDocumentCommand>();
            SaveDocument = commandResolver.ResolveCommand<SaveDocumentCommand>();
            NewDocument = commandResolver.ResolveCommand<NewDocumentCommand>();

            eventAggregator.Subscribe(this);

            eventAggregator.Publish(new LogMessage($"{YxtConstants.ApplicationName} started"));

            Document = null!;
            commandResolver.ResolveCommand<NewDocumentCommand>().Execute(this);
        }

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        public string Contents
        {
            get => _contents;
            set
            {
                SetField(ref _contents, value);
                // maybe this is not the best solution. If there are multiple sources of truth, it is hard to keep them in sync.
                Document.Contents = _contents;
                // todo this should change to the document. Information Hiding Principle
                Document.HasPendingChanges = true;
                var pendingChangesAsterix = Document.HasPendingChanges ? "*" : string.Empty;
                Title = $"{Document.Filename ?? "<new>"}{pendingChangesAsterix} [{YxtConstants.ApplicationName}]";
            }
        }

        public string LastLogEvent
        {
            get => _lastLogEvent;
            set => SetField(ref _lastLogEvent, value);
        }

        public ICommand CloseApplication { get; private set; }
        public ICommand OpenDocument { get; private set; }
        public ICommand SaveDocument { get; private set; }
        public ICommand SaveAsDocument { get; private set; }
        public ICommand NewDocument { get; private set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region Event Handling

        public Task HandleAsync(DocumentLoaded message, CancellationToken cancellationToken)
        {
            Document = message.Document;
            var pendingChangesAsterix = Document.HasPendingChanges ? "*" : string.Empty;
            Title = $"{Document.Filename ?? "<new>"}{pendingChangesAsterix} [{YxtConstants.ApplicationName}]";

            SetField(ref _contents, Document.Contents, nameof(Contents)); // do this to make sure no double roundtrip 

            return Task.CompletedTask;
        }

        public Task HandleAsync(DocumentSaved message, CancellationToken cancellationToken)
        {
            var pendingChangesAsterix = Document.HasPendingChanges ? "*" : string.Empty;
            Title = $"{Document.Filename ?? "<new>"}{pendingChangesAsterix} [{YxtConstants.ApplicationName}]";

            return Task.CompletedTask;
        }

        public Task HandleAsync(LogMessage message, CancellationToken cancellationToken)
        {
            LastLogEvent = message.Message;

            return Task.CompletedTask;
        }

        #endregion

        public YxtDocument Document { get; private set; }
    }
}

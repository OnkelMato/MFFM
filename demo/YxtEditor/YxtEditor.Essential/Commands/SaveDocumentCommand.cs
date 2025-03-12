using System.Windows.Input;
using Mffm.Contracts;
using Mffm.Core;
using YxtEditor.Essential.Messages;

namespace YxtEditor.Essential.Commands;

internal class SaveDocumentCommand : ICommand, IHandle<DocumentLoaded>
{
    private readonly IEventAggregator _eventAggregator;

    public SaveDocumentCommand(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

        _eventAggregator.Subscribe(this);
    }

    public Task HandleAsync(DocumentLoaded message, CancellationToken cancellationToken)
    {
        OnCanExecuteChanged();
        return Task.CompletedTask;
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter is not IHaveYxtDocument model) return false;

        return model.Document?.HasPendingChanges ?? false;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not IHaveYxtDocument model) return;

        File.WriteAllText(model.Document.Filename, model.Document.Contents);

        _eventAggregator.Publish(new DocumentSaved(model.Document));
        _eventAggregator.Publish(new LogMessage("Document saved"));

    }

    public event EventHandler? CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
using System.Globalization;
using System.Windows.Input;
using MediatR;
using Mffm.Contracts;
using Mffm.Core;
using YxtEditor.Essential.Mediator;
using YxtEditor.Essential.Messages;
using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Commands;

internal class NewDocumentCommand(IEventAggregator eventAggregator, IMediator mediator) : ICommand
{
    private readonly IEventAggregator _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        var currentDocument = (parameter as IHaveYxtDocument)?.Document;
        if (currentDocument is not null)
        {
            var response = _mediator.Send(new SaveDocumentRequest(currentDocument)).Result;
            if (!response.Success) return;
        }

        var newDocument = new YxtDocument() { Properties = { { "CreatedAt", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) } } };

        _eventAggregator.Publish(new DocumentLoaded(newDocument));
        _eventAggregator.Publish(new LogMessage("New Document created"));
    }

    public event EventHandler? CanExecuteChanged;
}
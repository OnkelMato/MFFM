using MediatR;
using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Mediator;

internal class SaveDocumentRequest(YxtDocument document) : IRequest<DocumentResponse>
{
    public YxtDocument Document { get; private set; } = document;
}
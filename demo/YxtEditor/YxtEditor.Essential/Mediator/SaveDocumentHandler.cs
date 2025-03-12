using MediatR;

namespace YxtEditor.Essential.Mediator;

internal class SaveDocumentHandler(IMediator mediator) : IRequestHandler<SaveDocumentRequest, DocumentResponse>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<DocumentResponse> Handle(SaveDocumentRequest request, CancellationToken cancellationToken)
    {
        if (!request.Document.HasPendingChanges) return new DocumentResponse(request.Document, true);

        // in case no filename is set, we need to save as
        if (string.IsNullOrWhiteSpace(request.Document.Filename))
        {
            var result = await _mediator.Send(new SaveAsDocumentRequest(request.Document), cancellationToken);
            if (!result.Success)
            {
                var answer = MessageBox.Show(@"Could not save document. Continue and discard changes?", @"Discard document", MessageBoxButtons.YesNo);
                switch (answer)
                {
                    case DialogResult.Yes:
                        return new DocumentResponse(request.Document, true);
                    case DialogResult.No:
                        return new DocumentResponse(request.Document, false);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        request.Document.FileTypeSupport.SaveToFile(request.Document.Filename, request.Document);
        return new DocumentResponse(request.Document, true);
    }
}
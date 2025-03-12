using MediatR;
using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Mediator;

internal class SaveAsDocumentHandler : IRequestHandler<SaveAsDocumentRequest, DocumentResponse>
{
    private readonly IMediator _mediator;
    private IEnumerable<IFileTypeSupport> _fileTypes;

    public SaveAsDocumentHandler(IMediator mediator, IEnumerable<IFileTypeSupport> fileTypes)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _fileTypes = fileTypes ?? throw new ArgumentNullException(nameof(fileTypes));
    }

    public async Task<DocumentResponse> Handle(SaveAsDocumentRequest request, CancellationToken cancellationToken)
    {
        var document = request.Document;

        var dlg = new SaveFileDialog();
        dlg.Filter = _fileTypes
            .Aggregate(
                string.Empty,
                (current, fileType) => $@"{current}|{fileType.Name} (*{fileType.Extension})|*{fileType.Extension}")
            .TrimStart('|');

        var answer = dlg.ShowDialog();
        if (answer == DialogResult.OK)
        {
            request.Document.Filename = dlg.FileName;
            request.Document.FileTypeSupport = _fileTypes.FirstOrDefault(x => dlg.FileName.EndsWith(x.Extension));
            var newRequest = new SaveDocumentRequest(request.Document);

            return await _mediator.Send(newRequest, cancellationToken);
        }

        return new DocumentResponse(document, false);
    }
}
using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Mediator;

internal class DocumentResponse(YxtDocument document, bool success)
{
    public YxtDocument Document { get; private set; } = document;
    public bool Success { get; private set; } = success;
}
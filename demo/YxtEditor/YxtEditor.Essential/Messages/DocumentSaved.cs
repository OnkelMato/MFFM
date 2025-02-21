using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Messages;

internal class DocumentSaved(YxtDocument document)
{
    public YxtDocument Document { get; set; } = document;
}
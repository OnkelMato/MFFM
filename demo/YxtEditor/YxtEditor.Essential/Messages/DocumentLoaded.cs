using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Messages;

internal class DocumentLoaded(YxtDocument document)
{
    public YxtDocument Document { get; set; } = document;
}
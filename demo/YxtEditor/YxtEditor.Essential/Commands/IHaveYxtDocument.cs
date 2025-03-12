using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Commands;

internal interface IHaveYxtDocument
{
    public YxtDocument Document { get; }
}
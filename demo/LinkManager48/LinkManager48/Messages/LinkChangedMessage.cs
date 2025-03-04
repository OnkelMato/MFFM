
using LinkManager48.Models;

namespace LinkManager48
{
    internal class LinkChangedMessage
    {
        public enum TypeOfChange
        {
            Created = 1,
            Changed = 2,
            Deleted = 3
        }

        public LinkChangedMessage(MyLink link, TypeOfChange changeType)
        {
            Link = link;
            ChangeType = changeType;
        }

        public MyLink Link { get; }
        public TypeOfChange ChangeType { get; }
    }
}
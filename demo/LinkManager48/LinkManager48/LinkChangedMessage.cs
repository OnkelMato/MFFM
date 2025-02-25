using LinkManager48.Models;

namespace LinkManager48
{
    internal class LinkChangedMessage
    {
        public LinkChangedMessage(MyLink link, ChangeType changeType)
        {
            Link = link;
            ChangeType = changeType;
        }

        public MyLink Link { get; }
        public ChangeType ChangeType { get; }
    }
}
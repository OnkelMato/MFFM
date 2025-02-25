namespace LinkManager48.Models
{
    internal class MyLink
    {
        public string Link { get; }
        public string Title { get; }

        public MyLink(string link, string title)
        {
            Link = link;
            Title = title;
        }
    }
}
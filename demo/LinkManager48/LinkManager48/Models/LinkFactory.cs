namespace LinkManager48.Models
{
    internal class LinkFactory : ILinkFactory
    {
        private readonly IHttpClient _httpClient;

        public LinkFactory(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public MyLink Create(string link)
        {
            return new MyLink(link, _httpClient.GetTitle(link));
        }
    }
}
using System.Net.Http;
using System.Text.RegularExpressions;

namespace LinkManager48.Models
{
    internal class DefaultHttpClient : IHttpClient
    {
        public string GetTitle(string url)
        {
            var client = new HttpClient();
            var content = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            var title = Regex.Match(content, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
            return title;
        }
    }
}
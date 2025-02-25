using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LinkManager48.Models
{
    internal class MyLink
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }

        public MyLink(string link, string title, string category = null, Guid? id = null)
        {
            Link = link;
            Title = title;
            Category = category;
            Id = id ?? Guid.Empty;
        }

        public MyLink() { }
    }

    internal interface ILinkRepository
    {
        IEnumerable<MyLink> GetLinks();
        MyLink SaveOrUpdate(MyLink link);
        void Delete(MyLink link);
    }

    internal class JsonLinkRepository : ILinkRepository
    {
        private Dictionary<Guid, MyLink> _linkCache;

        public JsonLinkRepository()
        {
            LoadLinks();
        }

        private void LoadLinks()
        {
            if (!File.Exists("links.json"))
            {
                _linkCache = new Dictionary<Guid, MyLink>();
                return;
            }

            var jsonString = File.ReadAllText("links.json");
            var links = JsonSerializer.Deserialize<IEnumerable<MyLink>>(jsonString);
            _linkCache = links.ToDictionary(x => x.Id, x => x);
        }

        public void SaveLinks()
        {
            var jsonString = JsonSerializer.Serialize(_linkCache.Values);
            File.WriteAllText("links.json", jsonString);
        }

        public IEnumerable<MyLink> GetLinks()
        {
            return _linkCache.Values.ToArray();
        }
        public MyLink SaveOrUpdate(MyLink link)
        {
            if (link.Id == Guid.Empty)
            {
                var id = Guid.NewGuid();
                var newLink = new MyLink(link.Link, link.Title, link.Category, id);
                _linkCache.Add(id, newLink);
                SaveLinks();
                return newLink;
            }
            else
            {
                if (_linkCache.ContainsKey(link.Id))
                {
                    _linkCache[link.Id] = link;
                    SaveLinks();
                    return link;
                }
                else
                {
                    _linkCache.Add(link.Id, link);
                    SaveLinks();
                    return link;
                }
            }
        }
        public void Delete(MyLink link)
        {

        }
    }
}
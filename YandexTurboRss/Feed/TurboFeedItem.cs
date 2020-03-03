using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Feed
{
    public class TurboFeedItem : ITurboFeedElement
    {
        private readonly XNamespace _turboYandexNamespace = Namespaces.TurboYandex;

        public TurboFeedItem()
        {
           Turbo = "true";
        }

        public string Link { get; set; }

        public string Source { get; set; }

        public string Topic { get; set; }

        public DateTime PubDate { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string Turbo { get; set; }

        public XElement ToXElement()
        {
            return new XElement(
                "item",
                new XAttribute("turbo", Turbo),
                new XElement("link", Link),
                new XElement(_turboYandexNamespace + "source", Source),
                new XElement(_turboYandexNamespace + "topic", Topic),
                new XElement("pubDate", PubDate.ToString("R")),
                new XElement("author", Author),
                new XElement(_turboYandexNamespace + "content", new XCData(Content)));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Feed
{
    /// Represents a Yandex Turbo RSS feed generator. 
    public class TurboFeed
    {
        private readonly XDocument _feed;
        private readonly XElement _channel;
        private readonly XNamespace _yandexNewsNamespace = Namespaces.YandexNews;
        private readonly XNamespace _turboYandexNamespace = Namespaces.TurboYandex;
        private readonly XNamespace _mediaNamespace = Namespaces.YahooMedia;

        public TurboFeed(TurboChannel channel)
        {
            _channel = channel?.ToXElement() ?? throw new ArgumentNullException(nameof(channel), "Channel cannot be null.");

            _feed = new XDocument(
                new XElement(
                    "rss",
                    new XAttribute(XNamespace.Xmlns + "yandex", _yandexNewsNamespace.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "media", _mediaNamespace.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "turbo", _turboYandexNamespace.NamespaceName),
                    new XAttribute("version", "2.0"),
                    _channel));
        }

        public void AddItem(TurboFeedItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Feed item cannot be null.");
            }

            _channel.Add(item.ToXElement());
        }

        public void AddItems(IEnumerable<TurboFeedItem> feed)
        {
            foreach (TurboFeedItem item in feed)
            {
                AddItem(item);
            }
        }

        public XDocument GetFeed()
        {
            return _feed;
        }

        public void SaveToFile(string path)
        {
            _feed.Save(path);
        }
    }
}
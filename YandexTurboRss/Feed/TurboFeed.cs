using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Related;

namespace YandexTurboRss.Feed
{
    public class TurboFeed
    {
        private readonly XDocument _feed;
        private readonly XElement _channel;
        private readonly XNamespace _yandexNewsNamespace = Namespaces.YandexNews;
        private readonly XNamespace _turboYandexNamespace = Namespaces.TurboYandex;
        private readonly XNamespace _mediaNamespace = Namespaces.YahooMedia;

        public TurboFeed(TurboChannel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel), "Channel cannot be null.");
            }

            _channel = GetChannel(channel);
            _feed = new XDocument(
                new XElement(
                    "rss",
                    new XAttribute(XNamespace.Xmlns + "yandex", _yandexNewsNamespace.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "media", _mediaNamespace.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "turbo", _turboYandexNamespace),
                    new XAttribute("version", "2.0"),
                    _channel));
        }

        public void AddItem(TurboFeedItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Feed item cannot be null.");
            }

            XElement element = new XElement(
                "item", 
                new XAttribute("turbo", item.Turbo),
                new XElement("link", item.Link),
                new XElement(_turboYandexNamespace + "source", item.Source),
                new XElement(_turboYandexNamespace + "topic", item.Topic),
                new XElement("pubDate", item.PubDate.ToString("R")),
                new XElement("author", item.Author),
                new XElement(_turboYandexNamespace + "content", new XCData(item.Content)));

            _channel.Add(element);
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

        private XElement GetChannel(TurboChannel channel)
        {
            List<XElement> analyticsElements = new List<XElement>();

            if (channel.Analytics != null && channel.Analytics.Any())
            {
                analyticsElements.AddRange(channel.Analytics.Select(analytics => analytics.ToXElement()));
            }

            List<XElement> adNetworkElements = new List<XElement>();

            if (channel.AdNetworks != null && channel.AdNetworks.Any())
            {
                analyticsElements.AddRange(channel.AdNetworks.Select(ads => ads.ToXElement()));
            }

            XElement related = channel.Related != null
                ? channel.Related.ToXElement()
                : new YandexRelated().ToXElement();

            return new XElement(
                "channel",
                new XElement("title", channel.Title),
                new XElement("link", channel.Link.ToString()),
                new XElement("description", channel.Description),
                new XElement("language", channel.Language), 
                analyticsElements,
                adNetworkElements, 
                related);
        }
    }
}
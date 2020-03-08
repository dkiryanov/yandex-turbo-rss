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

        /// <summary>
        /// Creates a TurboFeed object
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null</exception>
        /// <param name="channel">See <see cref="TurboChannel"/> to create TurboFeed object</param>
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

        /// <summary>
        /// Adds new <see cref="TurboFeedItem"/> to the feed
        /// </summary>
        /// <param name="item">See <see cref="TurboFeedItem"/> to add an item</param>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null</exception>
        public void AddItem(TurboFeedItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Feed item cannot be null.");
            }

            _channel.Add(item.ToXElement());
        }

        /// <summary>
        /// Adds a collection of <see cref="TurboFeedItem"/> to the feed
        /// </summary>
        /// <param name="feed">See <see cref="TurboFeedItem"/> to add items to the feed</param>
        /// <exception cref="ArgumentNullException">Thrown when feed to add or any feed item is null</exception>
        public void AddItems(IEnumerable<TurboFeedItem> feed)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed), "Feed item cannot be null.");
            }

            foreach (TurboFeedItem item in feed)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Returns a <see cref="XDocument"/> that contains a RSS data for the Turbo pages  
        /// </summary>
        /// <returns>A <see cref="XDocument"/> that contains a RSS data for the Turbo pages  </returns>
        public XDocument GetFeed()
        {
            return _feed;
        }

        /// <summary>
        /// Serializes a RSS data for the Turbo pages to a file, overwriting an existing file, if it exists
        /// </summary>
        /// <param name="path">A path to file</param>
        public void SaveToFile(string path)
        {
            _feed.Save(path);
        }
    }
}
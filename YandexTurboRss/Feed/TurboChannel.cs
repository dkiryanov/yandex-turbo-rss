using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YandexTurboRss.AdNetwork;
using YandexTurboRss.Analytics;

namespace YandexTurboRss.Feed
{
    /// <summary>
    /// Contains information about the source site
    /// </summary>
    public class TurboChannel : ITurboFeedElement
    {
        private const string DefaultLanguage = "ru";

        public TurboChannel()
        {
            Language = DefaultLanguage;
        }

        /// <summary>
        /// The RSS feed title.
        /// If you export the entire site contents, specify the site name.
        /// If you export a site section, enter just the section title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Channel description in one sentence. Do not use HTML markup.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Domain of the site from which the data is passed.
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// The article language according to the ISO 639-1 standard.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Web analytics tag for tracking Turbo pages traffic
        /// </summary>
        public IEnumerable<TurboAnalytics> Analytics { get; set; }

        /// <summary>
        /// Use it to display the Yandex Advertising network blocks and third-party ads connected through ADFOX on Turbo pages. 
        /// Also use it to track the advertising revenue from Turbo pages.
        /// </summary>
        public IEnumerable<TurboAdNetwork> AdNetworks { get; set; }

        /// <summary>
        /// One item element describes one page.
        /// </summary>
        public IEnumerable<TurboFeedItem> Items { get; set; }

        /// <summary>
        /// Creates a channel element with source site info
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> that represents a channel tag in the yandex turbo feed.</returns>
        public XElement ToXElement()
        {
            IEnumerable<XElement> analyticsCollection = Analytics?.Select(analytics => analytics.ToXElement()) ?? new List<XElement>();
            IEnumerable<XElement> adNetworkCollection = AdNetworks?.Select(ads => ads.ToXElement()) ?? new List<XElement>();
            IEnumerable<XElement> items = Items?.Select(item => item.ToXElement()) ?? new List<XElement>();

            return new XElement(
                "channel",
                new XElement("title", Title),
                new XElement("link", Link?.ToString()),
                new XElement("description", Description),
                new XElement("language", Language),
                analyticsCollection,
                adNetworkCollection,
                items);
        }
    }
}
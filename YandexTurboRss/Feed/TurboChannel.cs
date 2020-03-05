using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YandexTurboRss.AdNetwork;
using YandexTurboRss.Analytics;

namespace YandexTurboRss.Feed
{
    public class TurboChannel : ITurboFeedElement
    {
        private const string DefaultLanguage = "ru";

        public TurboChannel()
        {
            Language = DefaultLanguage;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Link { get; set; }

        public string Language { get; set; }

        public IEnumerable<TurboAnalytics> Analytics { get; set; }

        public IEnumerable<TurboAdNetwork> AdNetworks { get; set; }

        public IEnumerable<TurboFeedItem> Items { get; set; }

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
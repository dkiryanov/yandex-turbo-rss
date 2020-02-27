using System;
using System.Collections.Generic;
using YandexTurboRss.AdNetwork;
using YandexTurboRss.Analytics;
using YandexTurboRss.Related;

namespace YandexTurboRss.Feed
{
    public class TurboChannel
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

        public YandexRelated Related { get; set; }

        public IEnumerable<TurboAnalytics> Analytics { get; set; }

        public IEnumerable<TurboAdNetwork> AdNetworks { get; set; }
    }
}
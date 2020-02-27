using System;

namespace YandexTurboRss.Feed
{
    public class TurboFeedItem
    {
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
    }
}
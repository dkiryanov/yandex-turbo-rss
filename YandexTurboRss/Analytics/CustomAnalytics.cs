using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class CustomAnalytics : TurboAnalytics
    {
        public CustomAnalytics(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url), "Parameter cannot be null");
        }

        public override string Type => AnalyticsTypes.Custom;

        public override XElement ToXElement()
        {
            return new XElement(
                TurboYandexNamespace + "analytics",
                new XAttribute("type", Type),
                new XAttribute("url", Url));
        }
    }
}
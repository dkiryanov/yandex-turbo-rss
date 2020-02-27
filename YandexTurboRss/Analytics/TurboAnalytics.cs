using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace YandexTurboRss.Analytics
{
    public abstract class TurboAnalytics : ITurboFeedElement
    {
        public abstract string Type { get; }

        protected string Id { get; set; }

        protected string Url { get; set; }

        protected string Params { get; set; }

        protected virtual XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        public virtual XElement ToXElement()
        {
            return new XElement(
                TurboYandexNamespace + "analytics",
                new XAttribute("type", Type),
                new XAttribute("id", Id));
        }
    }
}
using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace YandexTurboRss.AdNetwork
{
    public abstract class TurboAdNetwork : ITurboFeedElement
    {
        public abstract string Type { get; }

        protected string TurboAdId { get; set; }

        protected virtual XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        public abstract XElement ToXElement();
    }
}
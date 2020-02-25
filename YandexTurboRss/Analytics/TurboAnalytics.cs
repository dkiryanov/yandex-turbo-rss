using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public abstract class TurboAnalytics
    {
        public abstract string Type { get; }

        protected string Id { get; set; }

        protected string Url { get; set; }

        protected string Params { get; set; }

        public virtual XElement ToXElement()
        {
            XNamespace turboYandexNamespace = Namespaces.TurboYandex;

            return new XElement(turboYandexNamespace + "analytics",
                new XAttribute("type", Type),
                new XAttribute("id", Id));
        }
    }
}
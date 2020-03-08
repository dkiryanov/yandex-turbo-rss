using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a base analytics class
    /// </summary>
    public abstract class TurboAnalytics : ITurboFeedElement
    {
        /// <summary>
        /// Represents an analtics sstem type
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Represents an analytics system's account ID
        /// </summary>
        protected string Id { get; set; }

        /// <summary>
        /// Represents an analytics system's URL
        /// </summary>
        protected string Url { get; set; }

        /// <summary>
        /// Represents a special analytics parameters
        /// </summary>
        protected string Params { get; set; }

        /// <summary>
        /// Represents a Turbo Yandex namespace
        /// </summary>
        protected virtual XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        /// <summary>
        /// Represents a web analytics system information in the turbo:analytics element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with a web analytics system information</returns>
        public virtual XElement ToXElement()
        {
            return new XElement(
                TurboYandexNamespace + "analytics",
                new XAttribute("type", Type),
                new XAttribute("id", Id));
        }
    }
}
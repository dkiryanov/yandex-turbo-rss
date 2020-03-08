using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace YandexTurboRss.AdNetwork
{
    /// <summary>
    /// Use it to display the Yandex Advertising network blocks and third-party ads connected through ADFOX on Turbo pages.
    /// </summary>
    public abstract class TurboAdNetwork : ITurboFeedElement
    {
        /// <summary>
        /// Represents an advertising network type
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Represents an advertisement's ID
        /// </summary>
        protected string TurboAdId { get; set; }

        /// <summary>
        /// Represents a Turbo Yandex namespace
        /// </summary>
        protected virtual XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        /// <summary>
        /// Represents an advertising network information in the turbo:adNetwork element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with an advertising network information</returns>
        public abstract XElement ToXElement();
    }
}
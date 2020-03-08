using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.AdNetwork
{
    public class YandexAd : TurboAdNetwork
    {
        private readonly string _id;

        /// <summary>
        /// Creates a <see cref="YandexAd"/>
        /// </summary>
        /// <param name="id">Yandex Advertising netwrks account's ID</param>
        /// <param name="turboAdId">A turbo advertisement's ID</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="turboAdId"/> parameter is null</exception>
        public YandexAd(string id, string turboAdId)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id), "Parameter cannot be null");
            TurboAdId = turboAdId ?? throw new ArgumentNullException(nameof(turboAdId), "Parameter cannot be null");
        }

        /// <summary>
        /// Represents a Yandex Advertising network type
        /// </summary>
        public override string Type => AdNetworkTypes.Yandex;

        /// <summary>
        /// Represents a Yandex advertising network information in the turbo:adNetwork element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with a Yandex advertising network information</returns>
        public override XElement ToXElement()
        {
            return new XElement(
                TurboYandexNamespace + "adNetwork",
                new XAttribute("type", Type),
                new XAttribute("id", _id),
                new XAttribute("turbo-ad-id", TurboAdId));
        }
    }
}
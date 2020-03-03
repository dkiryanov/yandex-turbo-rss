using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.AdNetwork
{
    public class YandexAd : TurboAdNetwork
    {
        private readonly string _id;

        public YandexAd(string id, string turboAdId)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id), "Parameter cannot be null");
            TurboAdId = turboAdId ?? throw new ArgumentNullException(nameof(turboAdId), "Parameter cannot be null");
        }

        public override string Type => AdNetworkTypes.Yandex;

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
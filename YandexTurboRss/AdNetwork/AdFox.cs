using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.AdNetwork
{
    public class AdFox : TurboAdNetwork
    {
        private readonly string _adScript;

        public AdFox(string turboAdId, string adScript)
        {
            TurboAdId = turboAdId ?? throw new ArgumentNullException(nameof(turboAdId), "Parameter cannot be null");
            _adScript = adScript;
        }

        public override string Type => AdNetworkTypes.AdFox;

        public override XElement ToXElement()
        {
            return new XElement(
                TurboYandexNamespace + "adNetwork",
                new XAttribute("type", Type),
                new XAttribute("turbo-ad-id", TurboAdId),
                new XCData(_adScript));
        }
    }
}
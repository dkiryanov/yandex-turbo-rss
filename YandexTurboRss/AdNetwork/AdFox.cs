using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.AdNetwork
{
    public class AdFox : TurboAdNetwork
    {
        private readonly string _adScript;

        public AdFox(string turboAdId, string adScript)
        {
            TurboAdId = turboAdId;
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
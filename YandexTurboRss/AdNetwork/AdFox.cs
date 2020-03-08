using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.AdNetwork
{
    public class AdFox : TurboAdNetwork
    {
        private readonly string _adScript;

        /// <summary>
        /// Creates a <see cref="AdFox"/>
        /// </summary>
        /// <param name="turboAdId">ADFOX account ID</param>
        /// <param name="adScript">ADFOX script</param>
        /// <exception cref="ArgumentNullException"> Thrown when the <paramref name="adScript"/> parameter is null</exception>
        public AdFox(string turboAdId, string adScript)
        {
            TurboAdId = turboAdId ?? throw new ArgumentNullException(nameof(turboAdId), "Parameter cannot be null");
            _adScript = adScript;
        }

        /// <summary>
        /// Represents an ADFOX network type 
        /// </summary>
        public override string Type => AdNetworkTypes.AdFox;

        /// <summary>
        /// Represents an ADFOX advertising network information in the turbo:adNetwork element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with an ADFOX advertising network information</returns>
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
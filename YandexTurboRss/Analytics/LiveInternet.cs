using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a Live internet analytics
    /// </summary>
    public class LiveInternet : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="LiveInternet"/>
        /// </summary>
        /// <param name="paramsValue">A live internet analytics params</param>
        public LiveInternet(string paramsValue = "")
        {
            Params = paramsValue;
        }

        /// <summary>
        /// Represents a Liveinternet analytics type
        /// </summary>
        public override string Type => AnalyticsTypes.LiveInternet;

        /// <summary>
        /// Represents a web analytics system information in the turbo:analytics element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with a web analytics system information</returns>
        public override XElement ToXElement()
        {
            XElement xelement = new XElement(TurboYandexNamespace + "analytics", new XAttribute("type", Type));

            if (!string.IsNullOrEmpty(Params))
            {
                xelement.SetAttributeValue("params", Params);
            }

            return xelement;
        }
    }
}
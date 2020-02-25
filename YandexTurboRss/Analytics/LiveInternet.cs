using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class LiveInternet : TurboAnalytics
    {
        public LiveInternet(string paramsValue = null)
        {
            Params = paramsValue;
        }

        public override string Type => AnalyticsTypes.LiveInternet;

        public override XElement ToXElement()
        {
            XNamespace turboYandexNamespace = Namespaces.TurboYandex;

            XElement xelement = new XElement(turboYandexNamespace + "analytics", new XAttribute("type", Type));

            if (!string.IsNullOrEmpty(Params))
            {
                xelement.SetAttributeValue("params", Params);
            }

            return xelement;
        }
    }
}
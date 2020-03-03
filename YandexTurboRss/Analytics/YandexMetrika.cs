using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class YandexMetrika : TurboAnalytics
    {
        public YandexMetrika(string id, string paramsValue = "")
        {
            Id = id;
            Params = paramsValue;
        }

        public override string Type => AnalyticsTypes.YandexMetrika;

        public override XElement ToXElement()
        {
            XElement xelement = base.ToXElement();

            if (!string.IsNullOrEmpty(Params))
            {
                xelement.SetAttributeValue("params", Params);
            }

            return xelement;
        }
    }
}
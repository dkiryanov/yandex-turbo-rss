using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a Yandex Metrika analytics
    /// </summary>
    public class YandexMetrika : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="YandexMetrika"/>
        /// </summary>
        /// <param name="id">Yandex Metrika's account ID</param>
        /// <param name="paramsValue">andex Metrika's special parameters</param>
        public YandexMetrika(string id, string paramsValue = "")
        {
            Id = id;
            Params = paramsValue;
        }

        /// <summary>
        /// Represents a Yandex Metrika analytics type
        /// </summary>
        public override string Type => AnalyticsTypes.YandexMetrika;

        /// <summary>
        /// Represents a web analytics system information in the turbo:analytics element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with a web analytics system information</returns>
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
using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a custom analytics
    /// </summary>
    public class CustomAnalytics : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="CustomAnalytics"/>
        /// </summary>
        /// <param name="url">A custom analytic URL</param>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null</exception>
        public CustomAnalytics(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url), "Parameter cannot be null");
        }

        /// <summary>
        /// A custom analtics type attribute
        /// </summary>
        public override string Type => AnalyticsTypes.Custom;

        /// <summary>
        /// Represents a web analytics system information in the turbo:analytics element
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> with a web analytics system information</returns>
        public override XElement ToXElement()
        {
            return new XElement(
                TurboYandexNamespace + "analytics",
                new XAttribute("type", Type),
                new XAttribute("url", Url));
        }
    }
}
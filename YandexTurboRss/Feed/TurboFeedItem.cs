using System;
using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Related;

namespace YandexTurboRss.Feed
{
    /// <summary>
    /// Represents an item element that describes an exported page
    /// </summary>
    public class TurboFeedItem : ITurboFeedElement
    {
        private readonly XNamespace _turboYandexNamespace = Namespaces.TurboYandex;

        public TurboFeedItem()
        {
           Turbo = "true";
        }

        /// <summary>
        /// The URL of the site page to generate the Turbo page for.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// The source page URL that can be sent to Yandex.Metrica.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The page heading that can be sent to Yandex.Metrica.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Publication time in the RFC-822 format.
        /// </summary>
        public DateTime PubDate { get; set; }

        /// <summary>
        /// The author of the article published on the page.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Page content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// To display Turbo pages, add the turbo="true" attribute. 
        /// To disable the display of a Turbo page, set the value to false.
        /// </summary>
        public string Turbo { get; set; }

        /// <summary>
        /// You can post links to other resources or customize the display of the infinite scroll.
        /// These links will be placed at the bottom of the Turbo page.
        /// </summary>
        public YandexRelated Related { get; set; }

        /// <summary>
        /// Creates an item element with page info
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> that represents an item tag in the yandex turbo feed.</returns>
        public XElement ToXElement()
        {
            return new XElement(
                "item",
                new XAttribute("turbo", Turbo),
                new XElement("link", Link),
                new XElement(_turboYandexNamespace + "source", Source),
                new XElement(_turboYandexNamespace + "topic", Topic),
                new XElement("pubDate", PubDate.ToString("R")),
                new XElement("author", Author),
                Related?.ToXElement(),
                new XElement(_turboYandexNamespace + "content", new XCData(Content)));
        }
    }
}
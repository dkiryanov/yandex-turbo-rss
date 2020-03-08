using System.Xml.Linq;
using YandexTurboRss.Feed;

namespace YandexTurboRss.Related
{
    /// <summary>
    /// Represents a related link in the Turbo feed
    /// </summary>
    public class YandexRelatedLink : ITurboFeedElement
    {
        /// <summary>
        /// A related page's link URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A related page's link image
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// A related page's link text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Creates a link element with related page's link data
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> that represents a related page's link</returns>
        public XElement ToXElement()
        {
            XElement linkElement = new XElement("link", new XAttribute("url", Url ?? string.Empty), Text);

            if (!string.IsNullOrEmpty(Img))
            {
                linkElement.SetAttributeValue("img", Img);
            }

            return linkElement;
        }
    }
}
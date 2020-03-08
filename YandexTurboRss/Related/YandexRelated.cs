using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace YandexTurboRss.Related
{
    /// <summary>
    /// Represents a related links in the Turbo RSS feed
    /// </summary>
    public class YandexRelated : ITurboFeedElement
    {
        /// <summary>
        /// Represents an infinite feed flag
        /// </summary>
        public bool IsInfinite { get; set; }

        /// <summary>
        /// A collection of the related links
        /// </summary>
        public IEnumerable<YandexRelatedLink> RelatedLinks { get; set; }

        /// <summary>
        /// Represents a Yandex News namespace
        /// </summary>
        protected XNamespace YandexNamespace => Namespaces.YandexNews;

        /// <summary>
        /// Creates a yandex:related element with the related links in it
        /// </summary>
        /// <returns>Returns <see cref="XElement"/> that represents a yandex:related element with the related links in it</returns>
        public virtual XElement ToXElement()
        {
            IEnumerable<XElement> relatedLinks = RelatedLinks?.Select(link => link.ToXElement());

            XElement relatedXelement = relatedLinks == null
                ? new XElement(YandexNamespace + "related")
                : new XElement(YandexNamespace + "related", relatedLinks);

            if (IsInfinite)
            {
                relatedXelement.SetAttributeValue("type", "infinity");
            }

            return relatedXelement;
        }
    }
}
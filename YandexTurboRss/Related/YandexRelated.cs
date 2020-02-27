using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace YandexTurboRss.Related
{
    public class YandexRelated : ITurboFeedElement
    {
        public bool IsInfinite { get; set; }

        public IEnumerable<YandexRelatedLink> RelatedLinks { get; set; }

        protected XNamespace YandexNamespace => Namespaces.YandexNews;

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
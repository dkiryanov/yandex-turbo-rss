using System.Xml.Linq;
using YandexTurboRss.Feed;

namespace YandexTurboRss.Related
{
    public class YandexRelatedLink : ITurboFeedElement
    {
        public string Url { get; set; }

        public string Img { get; set; }

        public string Text { get; set; }

        public XElement ToXElement()
        {
            return new XElement("link", new XAttribute("url", Url), new XAttribute("img", Img), Text);
        }
    }
}
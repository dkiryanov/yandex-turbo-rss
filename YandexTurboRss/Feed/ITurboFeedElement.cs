using System.Xml.Linq;

namespace YandexTurboRss.Feed
{
    public interface ITurboFeedElement
    {
        XElement ToXElement();
    }
}
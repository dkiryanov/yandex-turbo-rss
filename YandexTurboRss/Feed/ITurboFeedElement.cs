using System.Xml.Linq;

namespace YandexTurboRss.Feed
{
    public interface ITurboFeedElement
    {
        /// <summary>
        /// Creates a <see cref="XElement"/> from the current object
        /// </summary>
        /// <returns>A <see cref="XElement"/></returns>
        XElement ToXElement();
    }
}
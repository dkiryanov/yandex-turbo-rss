using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a Mail.ru analytics
    /// </summary>
    public class MailRu : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="MailRu"/>
        /// </summary>
        /// <param name="id">A Mail.ru analytics account ID</param>
        public MailRu(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Represents a Mail.ru analytics type
        /// </summary>
        public override string Type => AnalyticsTypes.MailRu;
    }
}
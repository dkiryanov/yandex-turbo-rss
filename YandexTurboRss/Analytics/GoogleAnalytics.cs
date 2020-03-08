using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a Google analytics
    /// </summary>
    public class GoogleAnalytics : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="GoogleAnalytics"/>
        /// </summary>
        /// <param name="id">A Google analytics account ID</param>
        public GoogleAnalytics(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Represents a Google analytics type
        /// </summary>
        public override string Type => AnalyticsTypes.GoogleAnalytics;
    }
}
using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a Rambler top 100 analytics
    /// </summary>
    public class RamblerTop100 : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="RamblerTop100"/>
        /// </summary>
        /// <param name="id">A Rambler top 100 analytics account ID</param>
        public RamblerTop100(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Represents a Rambler top 100 analtics type
        /// </summary>
        public override string Type => AnalyticsTypes.Rambler;
    }
}
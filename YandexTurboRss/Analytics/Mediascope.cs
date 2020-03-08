using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    /// <summary>
    /// Represents a Madiascope analytics
    /// </summary>
    public class Mediascope : TurboAnalytics
    {
        /// <summary>
        /// Creates a <see cref="Mediascope"/>
        /// </summary>
        /// <param name="id">A Mediascope analytics ID</param>
        public Mediascope(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Represents a Madiascope analytics type
        /// </summary>
        public override string Type => AnalyticsTypes.Mediascope;
    }
}
﻿using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class GoogleAnalytics : TurboAnalytics
    {
        public override string Type => AnalyticsTypes.GoogleAnalytics;

        public GoogleAnalytics(string id)
        {
            Id = id;
        }
    }
}
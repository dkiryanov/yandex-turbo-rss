﻿using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class Mediascope : TurboAnalytics
    {
        public override string Type => AnalyticsTypes.Mediascope;

        public Mediascope(string id)
        {
            Id = id;
        }
    }
}
﻿using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class RamblerTop100 : TurboAnalytics
    {
        public override string Type => AnalyticsTypes.Rambler;

        public RamblerTop100(string id)
        {
            Id = id;
        }
    }
}
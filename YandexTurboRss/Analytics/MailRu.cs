﻿using YandexTurboRss.Constants;

namespace YandexTurboRss.Analytics
{
    public class MailRu : TurboAnalytics
    {
        public override string Type => AnalyticsTypes.MailRu;

        public MailRu(string id)
        {
            Id = id;
        }
    }
}
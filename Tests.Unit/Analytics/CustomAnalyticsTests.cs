using System;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class CustomAnalyticsTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void CustomAnalytics_UrlIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            const string CustomAnalticsUrl = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => new CustomAnalytics(CustomAnalticsUrl));
        }

        [Test]
        public void CustomAnalytics_HasCustomAnalyticsType()
        {
            // Act
            CustomAnalytics ca = new CustomAnalytics("https://analytics.com");

            // Assert
            ca.Type.Should().BeEquivalentTo(AnalyticsTypes.Custom);
        }

        [Test]
        public void ToXElement_ReturnsCorrectXElement()
        {
            // Arrange
            const string CustomAnalyticsUrl = "https://analytics.com";
            CustomAnalytics ca = new CustomAnalytics(CustomAnalyticsUrl);

            // Act
            XElement result = ca.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.Custom)
                .And.HaveAttribute("url", CustomAnalyticsUrl);
        }
    }
}

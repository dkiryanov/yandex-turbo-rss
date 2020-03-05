using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class GoogleAnalyticsTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void GoogleAnalytics_GoogleAnalyticsType()
        {
            // Act
            GoogleAnalytics ga = new GoogleAnalytics("12345");

            // Assert
            ga.Type.Should().BeEquivalentTo(AnalyticsTypes.GoogleAnalytics);
        }

        [Test]
        public void ToXElement_ReturnsCorrectXElement()
        {
            // Arrange
            const string GaId = "123456";
            GoogleAnalytics ga = new GoogleAnalytics(GaId);

            // Act
            XElement result = ga.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.GoogleAnalytics)
                .And.HaveAttribute("id", GaId);
        }
    }
}

using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class LiveInternetTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void LiveInternet_HasLiveInternetType()
        {
            // Act
            LiveInternet liveInternet = new LiveInternet();

            // Assert
            liveInternet.Type.Should().BeEquivalentTo(AnalyticsTypes.LiveInternet);
        }

        [Test]
        public void ToXElement_NoLiveInternetParams_ReturnsXElementWithoutParamsAttribute()
        {
            // Arrange
            LiveInternet liveInternet = new LiveInternet();

            // Act
            XElement result = liveInternet.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.LiveInternet);
            result.Attribute("params").Should().BeNull();
        }

        [Test]
        public void ToXElement_HasLiveInternetParams_ReturnsXElementWithParamsAttribute()
        {
            // Arrange
            const string liveInternetParams = "LiveInternet params";
            LiveInternet liveInternet = new LiveInternet(liveInternetParams);

            // Act
            XElement result = liveInternet.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.LiveInternet)
                .And.HaveAttribute("params", liveInternetParams);
        }
    }
}

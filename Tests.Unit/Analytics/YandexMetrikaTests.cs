using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class YandexMetrikaTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void YandexMetrika_HasYandexMetrikaType()
        {
            // Act
            YandexMetrika metrika = new YandexMetrika("123456");

            // Assert
            metrika.Type.Should().BeEquivalentTo(AnalyticsTypes.YandexMetrika);
        }

        [Test]
        public void ToXElement_NoYandexMetrikaParams_ReturnsXElementWithoutParamsAttribute()
        {
            // Arrange
            const string MetrikaId = "123456";
            YandexMetrika metrika = new YandexMetrika(MetrikaId);

            // Act
            XElement result = metrika.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.YandexMetrika)
                  .And.HaveAttribute("id", MetrikaId);
            result.Attribute("params").Should().BeNull();
        }

        [Test]
        public void ToXElement_HasYandexMetrikaParams_ReturnsXElementWithParamsAttribute()
        {
            // Arrange
            const string MetrikaId = "123456";
            const string MetrikaParams = "yandex metrika params";
            YandexMetrika metrika = new YandexMetrika(MetrikaId, MetrikaParams);

            // Act
            XElement result = metrika.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.YandexMetrika)
                .And.HaveAttribute("id", MetrikaId)
                .And.HaveAttribute("params", MetrikaParams);
        }
    }
}

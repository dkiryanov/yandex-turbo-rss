using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class RamblerTop100Tests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void RamblerTop100_HasRamblerTop100Type()
        {
            // Act
            RamblerTop100 ramblerTop = new RamblerTop100("12345");

            // Assert
            ramblerTop.Type.Should().BeEquivalentTo(AnalyticsTypes.Rambler);
        }

        [Test]
        public void ToXElement_ReturnsCorrectXElement()
        {
            // Arrange
            const string RamblerId = "123456";
            RamblerTop100 ramblerTop = new RamblerTop100(RamblerId);

            // Act
            XElement result = ramblerTop.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.Rambler)
                .And.HaveAttribute("id", RamblerId);
        }
    }
}

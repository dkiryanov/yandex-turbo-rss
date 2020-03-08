using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class MediascopeTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void Mediascope_HasMediascopeType()
        {
            // Act
            Mediascope mediascope = new Mediascope("12345");

            // Assert
            mediascope.Type.Should().BeEquivalentTo(AnalyticsTypes.Mediascope);
        }

        [Test]
        public void ToXElement_ReturnsCorrectXElement()
        {
            // Arrange
            const string MediascopeId = "123456";
            Mediascope mediascope = new Mediascope(MediascopeId);

            // Act
            XElement result = mediascope.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.Mediascope)
                .And.HaveAttribute("id", MediascopeId);
        }
    }
}

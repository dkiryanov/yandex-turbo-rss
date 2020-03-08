using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;

namespace Tests.Unit.Analytics
{
    [TestFixture]
    public class MailRuTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;

        [Test]
        public void MailRu_HasMailRuType()
        {
            // Act
            MailRu mailRu = new MailRu("12345");

            // Assert
            mailRu.Type.Should().BeEquivalentTo(AnalyticsTypes.MailRu);
        }

        [Test]
        public void ToXElement_ReturnsCorrectXElement()
        {
            // Arrange
            const string MailRuId = "123456";
            MailRu mailRu = new MailRu(MailRuId);

            // Act
            XElement result = mailRu.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("analytics");
            result.Name.Namespace.Should().BeEquivalentTo(TurboYandexNamespace);
            result.Should().HaveAttribute("type", AnalyticsTypes.MailRu)
                .And.HaveAttribute("id", MailRuId);
        }
    }
}

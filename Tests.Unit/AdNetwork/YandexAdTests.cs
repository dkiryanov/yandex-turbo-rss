using System;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.AdNetwork;
using YandexTurboRss.Constants;

namespace Tests.Unit.AdNetwork
{
    [TestFixture]
    public class YandexAdTests
    {
        [Test]
        public void YandexAdCreated_HasYandexAdNetworkType()
        {
            // Act
            YandexAd result = new YandexAd("123", "1234");

            // Assert
            result.Type.Should().BeEquivalentTo(AdNetworkTypes.Yandex);
        }

        public void YandexAdCreated_IdOrTurboAdIdIsNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new YandexAd(null, "123"));
            Assert.Throws<ArgumentNullException>(() => new YandexAd("123", null));
        }

        [TestCase("id-123", "turbo-ad-id-321")]
        [TestCase("", "")]
        public void ToXElement_ReturnsNotNull(string id, string turboAdId)
        {
            // Act
            XElement result = new YandexAd(id, turboAdId).ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveAttribute("type", AdNetworkTypes.Yandex);
            result.Name.NamespaceName.Should().BeEquivalentTo(Namespaces.TurboYandex);
            result.Should().HaveAttribute("turbo-ad-id", turboAdId);
            result.Should().HaveAttribute("id", id);
        }
    }
}
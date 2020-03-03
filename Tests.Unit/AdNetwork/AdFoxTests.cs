using System;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.AdNetwork;
using YandexTurboRss.Constants;

namespace Tests.Unit.AdNetwork
{
    [TestFixture]
    public class AdFoxTests
    {
        [Test]
        public void AdFoxCreated_HasAdFoxNetworkType()
        {
            // Act
            AdFox result = new AdFox(string.Empty, string.Empty);

            // Assert
            result.Type.Should().BeEquivalentTo(AdNetworkTypes.AdFox);
        }

        [Test]
        public void AdFoxCreated_TurboAdIdIsNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new AdFox(null, string.Empty));
        }

        [TestCase("1234", "<div id='container-id'></div><script>window.Ya.adfoxCode.create({})</script>")]
        [TestCase("", "")]
        public void ToXElement_ReturnsNotNull(string turboAdId, string adScript)
        {
            // Act
            XElement result = new AdFox(turboAdId, adScript).ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveAttribute("type", AdNetworkTypes.AdFox);
            result.Name.NamespaceName.Should().BeEquivalentTo(Namespaces.TurboYandex);
            result.Should().HaveAttribute("turbo-ad-id", turboAdId);
            result.FirstNode.NodeType.Should().BeEquivalentTo(XmlNodeType.CDATA);
            result.Should().HaveValue(adScript);
        }
    }
}

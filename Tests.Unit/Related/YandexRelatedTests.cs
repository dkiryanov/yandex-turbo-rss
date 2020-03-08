using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Constants;
using YandexTurboRss.Related;

namespace Tests.Unit.Related
{
    [TestFixture]
    public class YandexRelatedTests
    {
        private static XNamespace YandexNamespace => Namespaces.YandexNews;

        [Test]
        public void ToXElement_ReturnsXElementNamedAsRelatedAndWithYandexNewNamespace()
        {
            // Arrange
            YandexRelated related = new YandexRelated();

            // Act
            XElement result = related.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.Namespace.Should().BeEquivalentTo(YandexNamespace);
            result.Name.LocalName.Should().BeEquivalentTo("related");
        }

        [Test]
        public void ToXElement_IsInfiniteTrue_ReturnsXElementWithTypeAttributeSetToInfinity()
        {
            // Arrange
            YandexRelated related = new YandexRelated { IsInfinite = true };

            // Act
            XElement result = related.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveAttribute("type", "infinity");
        }

        [Test]
        public void ToXElement_IsInfiniteFalse_ReturnsXElementWithoutTypeAttribute()
        {
            // Arrange
            YandexRelated related = new YandexRelated { IsInfinite = false };

            // Act
            XElement result = related.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Attribute("type").Should().BeNull();
        }

        [Test]
        public void ToXElement_RelatedLinksIsNull_ReturnsXElementWithoutRelatedLinks()
        {
            // Arrange
            YandexRelated related = new YandexRelated { RelatedLinks = null };

            // Act
            XElement result = related.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Elements().Should().BeEmpty();
        }

        [Test]
        public void ToXElement_HasRelatedLinks_ReturnsXElementWithRelatedLinks()
        {
            // Arrange
            YandexRelated related = new YandexRelated
            {
                RelatedLinks = new List<YandexRelatedLink>()
                {
                    new YandexRelatedLink()
                    {
                        Url = "https://link.com/link1",
                        Text = "Link 1 text"
                    },
                    new YandexRelatedLink()
                    {
                        Url = "https://link.com/link2",
                        Text = "Link 2 text"
                    }
                }
            };

            // Act
            XElement result = related.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Elements().Count().Should().Be(2);
            result.Elements().All(e => e.Name.LocalName.Equals("link")).Should().BeTrue();
        }
    }
}
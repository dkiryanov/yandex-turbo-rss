using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Related;

namespace Tests.Unit.Related
{
    [TestFixture]
    public class YandexRelatedLinkTests
    {
        [Test]
        public void ToXElement_ReturnsXELementWithLinkName()
        {
            // Arrange
            YandexRelatedLink link = new YandexRelatedLink();

            // Act
            XElement result = link.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Name.LocalName.Should().BeEquivalentTo("link");
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("some text", "some text")]
        [TestCase(" ", " ")]
        public void ToXElement_ReturnsXELementWithTextIfTextIsNotNullOrEmpty(string text, string expectedText)
        {
            // Arrange
            YandexRelatedLink link = new YandexRelatedLink()
            {
                Text = text
            };

            // Act
            XElement result = link.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expectedText);
        }

        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("https://someurl.com/", "https://someurl.com/")]
        [TestCase("  ", "  ")]
        public void ToXElement_ReturnsLinkWithUrlAttributeAndText(string url, string urlExpected)
        {
            // Arrange
            YandexRelatedLink link = new YandexRelatedLink()
            {
                Url = url
            };

            // Act
            XElement result = link.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveAttribute("url", urlExpected);
        }

        [Test]
        public void ToXElement_ImgIsNotNullOrEmpty_ReturnsXElementWithImgAttribute()
        {
            // Arrange
            const string ImgUrl = "https://img.com/img.jpeg";
            YandexRelatedLink link = new YandexRelatedLink()
            {
                Img = ImgUrl
            };

            // Act
            XElement result = link.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveAttribute("img", ImgUrl);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ToXElement_ImgIsNullOrEmpty_ReturnsXElementWithoutImgAttribute(string img)
        {
            // Arrange
            YandexRelatedLink link = new YandexRelatedLink()
            {
                Img = img
            };

            // Act
            XElement result = link.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Element("img").Should().BeNull();
        }
    }
}
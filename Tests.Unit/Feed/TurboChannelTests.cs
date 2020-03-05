using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.AdNetwork;
using YandexTurboRss.Analytics;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace Tests.Unit.Feed
{
    [TestFixture]
    public class TurboChannelTests
    {
        private XNamespace TurboYandexNamespace => Namespaces.TurboYandex;
        
        [Test]
        public void TurboChannel_DefaultLanguageIsRu()
        {
            // Act
            TurboChannel channel = new TurboChannel();

            // Assert
            channel.Language.Should().BeEquivalentTo("ru");
        }

        [Test]
        public void ToXElement_ReturnsXElement()
        {
            // Arrange
            TurboChannel channel = new TurboChannel()
            {
                Title = "Channel title",
                Link = new Uri("https://website.com"),
                Description = "Channel' description",
                Language = "en"
            };

            // Act
            XElement result = channel.ToXElement();

            // Assert
            result.Should().HaveElement("link", channel.Link.ToString());
            result.Should().HaveElement("language", channel.Language);
            result.Should().HaveElement("title", channel.Title);
            result.Should().HaveElement("description", channel.Description);

            result.Element(TurboYandexNamespace + "analytics").Should().BeNull();
            result.Element(TurboYandexNamespace + "adNetwork").Should().BeNull();
            result.Element("item").Should().BeNull();
        }

        [Test]
        public void ToXElement_HasAnaytics_ReturnsXElementWithAnalytics()
        {
            // Arrange
            TurboChannel channel = new TurboChannel()
            {
                Analytics = new List<TurboAnalytics>()
                {
                    new GoogleAnalytics("ga-12345")
                }
            };

            // Act
            XElement result = channel.ToXElement();

            // Assert
            result.Should().HaveElement(TurboYandexNamespace + "analytics");
        }

        [Test]
        public void ToXElement_HasAdNetwork_ReturnsXElementWithAdNetwork()
        {
            // Arrange
            TurboChannel channel = new TurboChannel()
            {
                AdNetworks = new List<TurboAdNetwork>()
                {
                    new AdFox("turbo-ad-id", "<script>ad script</script>")
                }
            };

            // Act
            XElement result = channel.ToXElement();

            // Assert
            result.Should().HaveElement(TurboYandexNamespace + "adNetwork");
        }

        [Test]
        public void ToXElement_HasItem_ReturnsXElementWithItem()
        {
            // Arrange
            TurboChannel channel = new TurboChannel()
            {
                Items = new List<TurboFeedItem>()
                {
                    new TurboFeedItem()
                    {
                        Link = "https://sample.ru",
                        Author = "Author 1",
                        Content = "Content 1",
                        PubDate = DateTime.Now,
                        Source = "https://source2.ru",
                        Topic = "Item 1",
                        Turbo = "true"
                    }
                }
            };

            // Act
            XElement result = channel.ToXElement();

            // Assert
            result.Should().HaveElement("item");
        }
    }
}

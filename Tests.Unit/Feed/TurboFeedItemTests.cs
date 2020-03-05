using System;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexTurboRss.Constants;
using YandexTurboRss.Feed;

namespace Tests.Unit.Feed
{
    [TestFixture()]
    public class TurboFeedItemTests
    {
        private readonly XNamespace _turboYandexNamespace = Namespaces.TurboYandex;

        [Test]
        public void ToXElement_ReturnsCorrectXElement()
        {
            // Arrange
            DateTime pubDate = DateTime.Now;
            TurboFeedItem item = new TurboFeedItem()
            {
                Link = "https://sample.ru",
                Author = "Author 1",
                Content = "Content 1",
                PubDate = pubDate,
                Source = "https://source2.ru",
                Topic = "Item 1",
                Turbo = "true"
            };

            // Act
            XElement result = item.ToXElement();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveElement("link", item.Link);
            result.Should().HaveElement(_turboYandexNamespace + "source", item.Source);
            result.Should().HaveElement(_turboYandexNamespace + "topic").Which.Value.Should().BeEquivalentTo("Item 1");
            result.Should().HaveElement("pubDate", item.PubDate.ToString("R"));
            result.Should().HaveElement("author", item.Author);
            result.Should().HaveElement(_turboYandexNamespace + "content", item.Content);
            result.Should().HaveAttribute("turbo", item.Turbo);
        }

        [Test]
        public void TurboFeedItem_HasDefaultTurboPropertySetToTrue()
        {
            // Act
            TurboFeedItem result = new TurboFeedItem();

            // Assert
            result.Turbo.Should().BeEquivalentTo("true");
        }
    }
}

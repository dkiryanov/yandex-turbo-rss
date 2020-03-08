using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TurboFeedTests
    {
        private const string ChannelElementName = "channel";
        private const string AdFoxScript = @"<div id=""ad-fox-block-id""></div>
                <script>
                    window.Ya.adfoxCode.create({
                        ownerId: 123456,
                        containerId: 'ad-fox-block-id',
                        params: {
                            pp: 'g',
                            ps: 'cmic',
                            p2: 'fqem'
                        }
                    });
                </script>";

        private readonly XNamespace _yandexNewsNamespace = Namespaces.YandexNews;
        private readonly XNamespace _turboYandexNamespace = Namespaces.TurboYandex;
        private readonly XNamespace _mediaNamespace = Namespaces.YahooMedia;

        private TurboChannel _channel;
        private TurboFeed _turboFeed;

        [SetUp]
        public void SetUp()
        {
            _channel = new TurboChannel()
            {
                Title = "RSS channel name",
                Link = new Uri("https://turbo-feed.com"),
                Description = "Short RSS channel description",
                Analytics = new List<TurboAnalytics>()
                {
                    new YandexMetrika("12345")
                },
                AdNetworks = new List<TurboAdNetwork>()
                {
                    new YandexAd("yandex-block-id", "yandex_ad_place"),
                    new AdFox("adfox-bock-id", AdFoxScript)
                }
            };
            _turboFeed = new TurboFeed(_channel);
        }

        [Test]
        public void GetFeed_ReturnsTurboFeedWithRootRssElement()
        {
            // Act
            XDocument result = _turboFeed.GetFeed();

            // Assert
            result.Should().NotBeNull();
            result.Root.Should().NotBeNull();
            result.Root.Name.LocalName.Should().BeEquivalentTo("rss");
            result.Root.Should().HaveAttribute(XNamespace.Xmlns + "yandex", _yandexNewsNamespace.NamespaceName);
            result.Root.Should().HaveAttribute(XNamespace.Xmlns + "media", _mediaNamespace.NamespaceName);
            result.Root.Should().HaveAttribute(XNamespace.Xmlns + "turbo", _turboYandexNamespace.NamespaceName);
            result.Root.Should().HaveAttribute("version", "2.0");
        }

        [Test]
        public void GetFeed_HasNoItems_ReturnsTurboFeedWithEmptyChannelElement()
        {
            // Act
            XDocument result = _turboFeed.GetFeed();

            // Assert
            result.Should().NotBeNull();

            XElement root = result.Root;
            root.Should().NotBeNull();
    
            root.Should().HaveElement(ChannelElementName);
            root.Element(ChannelElementName).HasAttributes.Should().BeFalse();
            root.Element(ChannelElementName).HasElements.Should().BeTrue();
            root.Element(ChannelElementName).Elements("item").Should().BeEmpty();
        }

        [Test]
        public void GetFeed_HasItems_ReturnsTurboFeedWithFeedItems()
        {
            // Arrange
            _channel = new TurboChannel()
            {
                Title = "RSS channel name",
                Link = new Uri("https://turbo-feed.com"),
                Description = "Short RSS channel description",
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
                    },
                    new TurboFeedItem()
                    {
                        Link = "https://sample2.ru",
                        Author = "Author 2",
                        Content = "Content 2",
                        PubDate = DateTime.Now,
                        Source = "https://source2.ru",
                        Topic = "Item 2",
                        Turbo = "false"
                    }
                }
            };
            _turboFeed = new TurboFeed(_channel);

            // Act
            XDocument result = _turboFeed.GetFeed();

            // Assert
            result.Should().NotBeNull();

            XElement root = result.Root;
            root.Should().NotBeNull();

            root.Should().HaveElement(ChannelElementName);
            root.Element(ChannelElementName).HasAttributes.Should().BeFalse();
            root.Element(ChannelElementName).HasElements.Should().BeTrue();
            root.Element(ChannelElementName).Elements("item").Count().Should().Equals(2);
        }

        [Test]
        public void AddItem_ItemIsNotNull_AddsFeedItem()
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
                Topic = "Item 1"
            };

            // Act
            _turboFeed.AddItem(item);
            XDocument result = _turboFeed.GetFeed();

            // Assert
            result.Should().NotBeNull();

            XElement root = result.Root;
            root.Should().NotBeNull();

            root.Element(ChannelElementName).Elements("item").Count().Should().Equals(1);

            XElement addedItem = root.Element(ChannelElementName).Element("item");

            addedItem.Should().NotBeNull();
            addedItem.Should().HaveAttribute("turbo", "true");
            addedItem.Should().HaveElement("link").Which.Value.Should().BeEquivalentTo("https://sample.ru");
            addedItem.Should().HaveElement(_turboYandexNamespace + "source").Which.Value.Should().BeEquivalentTo("https://source2.ru");
            addedItem.Should().HaveElement(_turboYandexNamespace + "topic").Which.Value.Should().BeEquivalentTo("Item 1");
            addedItem.Should().HaveElement("pubDate").Which.Value.Should().BeEquivalentTo(pubDate.ToString("R"));
            addedItem.Should().HaveElement("author").Which.Value.Should().BeEquivalentTo("Author 1");
            addedItem.Should().HaveElement(_turboYandexNamespace + "content").Which.Value.Should().BeEquivalentTo("Content 1");
        }

        [Test]
        public void AddItem_ItemIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            TurboFeedItem item = null;
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => _turboFeed.AddItem(item));
        }

        [Test]
        public void AddItems_HasItemThatIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            List<TurboFeedItem> items = new List<TurboFeedItem>()
            {
                null
            };

            // Assert
            Assert.Throws<ArgumentNullException>(() => _turboFeed.AddItems(items));
        }

        [Test]
        public void AddItems_ItemsCollectionToAddIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            List<TurboFeedItem> items = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _turboFeed.AddItems(items));
        }

        [Test]
        public void AddItems_HasItemsToAdd_AddsItemsToFeed()
        {
            // Arrange
            List<TurboFeedItem> itemsToAdd = new List<TurboFeedItem>()
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
                },
                new TurboFeedItem()
                {
                    Link = "https://sample2.ru",
                    Author = "Author 2",
                    Content = "Content 2",
                    PubDate = DateTime.Now,
                    Source = "https://source2.ru",
                    Topic = "Item 2",
                    Turbo = "false"
                }
            };

            // Act
            _turboFeed.AddItems(itemsToAdd);
            XDocument result = _turboFeed.GetFeed();

            // Assert
            result.Should().NotBeNull();
             
            XElement root = result.Root;
            root.Should().NotBeNull();
            root.Element(ChannelElementName).Elements("item").Count().Should().Equals(2);

            IEnumerable<XElement> addedItems = root.Element(ChannelElementName).Elements("item");

            addedItems.Should().NotBeNull();

            for (int i = 0; i < itemsToAdd.Count; i++)
            {
                TurboFeedItem itemToAdd = itemsToAdd[i];
                XElement addedItem = addedItems.ElementAt(i);

                addedItem.Should().HaveAttribute("turbo", itemToAdd.Turbo);
                addedItem.Should().HaveElement("link").Which.Value.Should().BeEquivalentTo(itemToAdd.Link);
                addedItem.Should().HaveElement(_turboYandexNamespace + "source").Which.Value.Should().BeEquivalentTo(itemToAdd.Source);
                addedItem.Should().HaveElement(_turboYandexNamespace + "topic").Which.Value.Should().BeEquivalentTo(itemToAdd.Topic);
                addedItem.Should().HaveElement("pubDate").Which.Value.Should().BeEquivalentTo(itemToAdd.PubDate.ToString("R"));
                addedItem.Should().HaveElement("author").Which.Value.Should().BeEquivalentTo(itemToAdd.Author);
                addedItem.Should().HaveElement(_turboYandexNamespace + "content").Which.Value.Should().BeEquivalentTo(itemToAdd.Content);
            }
        }
    }
}

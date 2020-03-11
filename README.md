# YandexTurboRss library
The YandexTurboRss library is the RSS feed markup tool written in C# programming language for the Yandex Turbo pages. Turbo pages feed data is exported in the XML-based RSS 2.0 format. 

Please see more about the technology and the data format at the official website: [https://tech.yandex.com/turbo/doc/rss/markup-docpage/](https://tech.yandex.com/turbo/doc/rss/markup-docpage/) and [https://tech.yandex.ru/turbo/doc/rss/markup-docpage/](https://tech.yandex.com/turbo/doc/rss/markup-docpage/) 

## Installation

To install this package use package manager console in your Visual Studio and type:
```
PM> Install-Package YandexTurboRss -Version 1.0.1
```

Please visit our official page at nuget.org website and see more info about installation: [https://www.nuget.org/packages/YandexTurboRss/](https://www.nuget.org/packages/YandexTurboRss/).

## Usage

The library provides you with a bunch of classes that represent a Yandex Turbo Pages feed structure. One of the main classes is ```TurboChannel``` that contains information about the source site and maps to the ```<channel>``` tag in the feed. And the crucial class is the ```TurboFeed``` that contains channel with items and allows you to add feed items represented by ```TurboFeedItem```, save the feed data to disk or to get the feed collection.

Let's code some examples...

First of all, we need to create a channel:

```c#
TurboChannel channel = new TurboChannel()
{
    Description = "Short description of the feed",
    Title = "Feed title",
    Link = new Uri("http://www.example.com/")
};
```
All right! We've just set up the source information! Now let's create a feed and initialize it with the channel created previously:

```c#
TurboFeed feed = new TurboFeed(channel);
```

So, now we have an empty feed. It's time to add some items there:

```c#
TurboFeedItem item = new TurboFeedItem()
 {
    Author = "John Doe",
    Link = "http://www.example.com/category/sub-category/page1.html",
    PubDate = DateTime.Now,
    Topic = "Page title goes here...",
    Content = "Page content goes here..."
 };
feed.AddItem(item);
```
...or you can add a collection of items:

```c#
IEnumerable<TurboFeedItem> items = GetTurboFeedItems();
feed.AddItems(items);
```
Also, you can pass the feed items when you initialize the channel:
```c#
TurboChannel channel = new TurboChannel()
{
    Description = "Short description of the feed",
    Title = "Feed title",
    Link = new Uri("http://www.example.com/"),
    Items = GetTurboFeedItems()
};
TurboFeed feed = new TurboFeed(channel);
```

Please pay attention to the Yandex requirements about the page's header: [https://tech.yandex.com/turbo/doc/rss/markup-docpage/#turbo-content](https://tech.yandex.com/turbo/doc/rss/markup-docpage/#turbo-content). You'll need to create the header according to the requirements and concatenate it with the page's content. After this, initialize the ``` TurboChannel.Description ``` property with your content. Our library wraps it in ``` <![CDATA[...]]> ``` so you do not need to care about this. Perhaps the page header functionality will be included in the future versions.

Also, the date passed to the ``` PubDate ``` property will be formatted automatically to the RFC-822 format.

At the end, you can save your feed to disk:

```c#
feed.SaveToFile("C:\\rss\\turbo.xml");
```

Or get the feed collection:

```c#
XDocument feedDocument = feed.GetFeed();
```

## Adding analytics to the yandex turbo feed

It is easy to add your counters to the feed ussing the [https://webmaster.yandex.ru](https://webmaster.yandex.ru) interface. You can also add analytics using our library:

```c#
TurboChannel channel = new TurboChannel()
{
    Analytics = new List<TurboAnalytics>()
    {
        new YandexMetrika("12345"),
        new GoogleAnalytics("ga-12345"),
        new LiveInternet(),
        new RamblerTop100("345678")
    }
};
TurboFeed feed = new TurboFeed(channel);
```

## Adding advertisements to the yandex turbo feed

Here is an example of how you can add some ads to the feed:

```c#
TurboChannel channel = new TurboChannel()
{
    AdNetworks = new List<TurboAdNetwork>()
    {
        new AdFox("turbo-ad-id", "<script>ad script</script>"),
        new YandexAd("yandex-block-id", "yandex_ad_place")
    }
};
```

Please note that Yandex turbo pages only allows you to display Yandex Direct or AdFox ads.

## Adding related pages

You can post links to other resources or customize the display of the infinite scroll.
These links will be placed at the bottom of the Turbo page. To add links anywhere on the page, use ```YandexRelated``` class:

```c#
TurboFeed feed = new TurboFeed(new TurboChannel()
{
    Description = "Short description",
    Title = "Feed title",
    Link = new Uri("https://sourcewebsite.com"),
});
            
TurboFeedItem item = new TurboFeedItem()
 {
    Related = new YandexRelated
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
     }
};

feed.AddItem(item);
```

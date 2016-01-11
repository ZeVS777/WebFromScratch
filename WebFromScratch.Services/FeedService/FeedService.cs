using System;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using WebFromScratch.Resources.Constants;

namespace WebFromScratch.Services.FeedService
{
    public class FeedService
    {
        /* Пример ленты последних обновлений блога в формате Atom с одной записью:
        <?xml version="1.0" encoding="utf-8"?>
        <feed xmlns="http://www.w3.org/2005/Atom">
          <title>Мой блог</title>
          <subtitle>Самый лучший блог на свете</subtitle>
          <link href="http://example.org/"/>
          <updated>2003-12-13T18:30:02Z</updated>
          <author>
            <name>Иван Петров</name>
            <email>petrov@example.com</email>
          </author>
          <id>urn:uuid:60a76c80-d399-11d9-b91C-0003939e0af6</id>
          <entry>
            <title>Фотографии из Африки</title>
            <link href="http://example.org/2003/12/13/atom03"/>
            <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
            <updated>2003-12-13T18:30:02Z</updated>
            <summary>Я вернулся из Африки и выложил свои фотографии...</summary>
          </entry>
        </feed>
        */

        // Идентификатор новостной ленты
        private const string FeedId = "FBEEA072-6F15-491A-9790-FD9C99A99328";
        // Адрес сервера для обновления новостей подписанных клиентов
        //private const string PubSubHubbubHubUrl = "https://pubsubhubbub.appspot.com/";

        private readonly UrlHelper _urlHelper;
        private readonly Uri _baseUri;

        public FeedService(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
            _baseUri = _urlHelper.RequestContext.HttpContext.Request.Url;
        }

        public async Task<SyndicationFeed> GetWindowsTileFeed(CancellationToken cancellationToken)
        {

            var feed = new SyndicationFeed
            {
                //Требуется
                Id = FeedId,
                //Требуется
                Title = new TextSyndicationContent("web.example.com"),
                //Желательно
                Description = new TextSyndicationContent("Лента для обновления плиток в Windows"),
                //Опционально
                ImageUrl = new Uri(_baseUri, _urlHelper.Content("~/img/icons/atom/atom-logo-96x48.png")),
                //Опционально
                Copyright =
                    new TextSyndicationContent(string.Format("© {0} - {1}", DateTime.Now.Year, Application.Name)),
                //Опционально
                Language = "ru-RU",
                //LastUpdatedTime = DateTimeOffset.Now
            };
            //Требуется
            feed.Links.Add(SyndicationLink.CreateSelfLink(new Uri(_baseUri, _urlHelper.RouteUrl(HomeControllerRoute.GetFeed))));
            //Желательно
            feed.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(_baseUri, _urlHelper.RouteUrl(HomeControllerRoute.GetIndex))));
            //Желательно
            feed.Authors.Add(new SyndicationPerson("zevs7772006@gmail.com", "ZeVS7772006", "http://zevs7772006.azurewebsites.net"));
            //Опционально
            feed.Categories.Add(new SyndicationCategory("Новости для плиток"));
            //Опционально
            //feed.Contributors
            //Опционально
            feed.ElementExtensions.Add(new SyndicationElementExtension("icon", null, new Uri(_baseUri, _urlHelper.Content("~/img/icons/atom/atom-icon-48x48.png"))));
            // Добавить Yahoo Media пространство имён (xmlns:media="http://search.yahoo.com/mrss/"), чтоб можно было добавлять изображения к новостям
            // Смотри: http://www.rssboard.org/media-rss
            feed.AttributeExtensions.Add(new XmlQualifiedName("media", XNamespace.Xmlns.ToString()), "http://search.yahoo.com/mrss/");

            //feed.Items = await GetItems(cancellationToken);

            return feed;
        }
    }
}
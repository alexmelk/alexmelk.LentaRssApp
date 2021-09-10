using alexmelk.LentaRss.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace alexmelk.LentaRss.Services
{
    public class RssReader : IRssReader
    {
        private string _url;
        private IHttpClientProxy _httpClient;

        public RssServiceEnum RssName { get; }

        public RssReader(string url, RssServiceEnum rssName)
        {
            RssName = rssName;
            _url = url;
            _httpClient = new HttpClientProxy(url, this.GetType().ToString());
        }

        private async Task<ResultModel> Read()
        {
            return await _httpClient.SendGet("/", false);
        }
        public async Task<List<SyndicationItem>> GetRssItems()
        {
            var result = await Read();

            SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(new StringReader(result.Data.ToString())));
            var items = feed.Items.ToList();
            return items;
        }
    }
}

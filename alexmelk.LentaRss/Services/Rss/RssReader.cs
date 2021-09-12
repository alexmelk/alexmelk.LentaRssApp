using alexmelk.LentaRss.Models;
using Newtonsoft.Json;
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

        public RssReader(string url)
        {
            _url = url;
            _httpClient = new HttpClientProxy(url, this.GetType().ToString());
        }

        private async Task<ResultModel> Read()
        {
            return await _httpClient.SendGet("/", false);
        }

        public async Task<List<SyndicationItem>> GetSyndicationItems(string xml)
        {
            var feed = SyndicationFeed.Load(XmlReader.Create(new StringReader(xml)));
            var items = feed.Items.ToList();
            return items;
        }

        public List<RssItem> Map(List<SyndicationItem> syndications)
        {
            var rssItems = new List<RssItem>();

            foreach(var item in syndications)
            {
                var temp = new RssItem
                {
                    Author = string.Join(" ", item.Authors.Select(x => x.Name).ToArray()),
                    Category = string.Join(" ", item.Categories.Select(x => x.Name).ToArray()),
                    PubDate = item.PublishDate.DateTime,
                    Title = item.Title.Text,
                    Link = item.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? "",
                    Image = item.Links.LastOrDefault()?.Uri.AbsoluteUri ?? "",
                    Description = item.Summary.Text,      
                };

                rssItems.Add(temp);
            }

            return rssItems;
        }

        public async Task<string> GetItemsSerialize()
        {
            var result = await Read();
            if (result.Result)
            {
                var syndicationItems = await GetSyndicationItems(result.Data.ToString());
                var map = Map(syndicationItems);
                var str = JsonConvert.SerializeObject(map);
                return str;
            }
            else
            {
                return JsonConvert.SerializeObject(result);
            }
 
        }
    }
}

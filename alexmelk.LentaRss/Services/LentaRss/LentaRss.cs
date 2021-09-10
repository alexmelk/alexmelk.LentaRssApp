using alexmelk.LentaRss.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace alexmelk.LentaRss.Services
{
    public class LentaRss
    {
        private IRssReader _rssReader;
        public LentaRss(string url)
        {
            _rssReader = new RssReader(url, RssServiceEnum.LentaRss);
        }

        public async Task<string> GetItemsSerialize()
        {
            var result = await _rssReader.GetRssItems();
            var str = JsonConvert.SerializeObject(result);
            return str;
        }
    }
}

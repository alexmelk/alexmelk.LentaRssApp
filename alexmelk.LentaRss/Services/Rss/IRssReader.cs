using alexmelk.LentaRss.Models;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace alexmelk.LentaRss.Services
{
    public interface IRssReader
    {
        Task<List<SyndicationItem>> GetRssItems();
    }
}
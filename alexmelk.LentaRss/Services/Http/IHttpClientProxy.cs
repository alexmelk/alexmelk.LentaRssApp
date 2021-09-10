using System.Threading.Tasks;
using alexmelk.LentaRss.Models;

namespace alexmelk.LentaRss.Services
{
    interface IHttpClientProxy
    {
        public Task<ResultModel> SendPost(string content, string path);
        public Task<ResultModel> SendGet(string path, bool deserialize = true);
    }
}

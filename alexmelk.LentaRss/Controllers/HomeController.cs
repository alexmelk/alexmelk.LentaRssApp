using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using alexmelk.LentaRss.Models;
using alexmelk.LentaRss.Services;

namespace alexmelk.LentaRss.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private LentaRss.Services.LentaRss _rss;
        public HomeController(LentaRss.Services.LentaRss rss)
        {
            _rss = rss;
        }
        public async Task<string> Get()
        {
            return await _rss.GetItemsSerialize();
        }
    }
}

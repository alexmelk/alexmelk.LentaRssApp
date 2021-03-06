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
        private IRssReader _rss;
        public HomeController(IRssReader rss)
        {
            _rss = rss;
        }
        [Route("getLentaRss")]
        public async Task<string> GetLentaRss()
        {
            return await _rss.GetItemsSerialize();
        }
    }
}

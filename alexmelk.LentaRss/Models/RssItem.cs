using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexmelk.LentaRss.Models
{
    public class RssItem
    {
        public string Author { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription => string.Join("",Description?.Take(80)) + "...";
        public DateTime PubDate { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}

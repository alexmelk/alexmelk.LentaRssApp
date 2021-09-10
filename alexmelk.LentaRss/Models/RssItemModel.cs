using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexmelk.LentaRss.Models
{
    public class RssItemModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
        public string Enclosure { get; set; }
        public string Category { get; set; }
    }
}

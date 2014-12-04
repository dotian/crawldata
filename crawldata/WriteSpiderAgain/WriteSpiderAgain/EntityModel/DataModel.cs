using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WriteSpiderAgain.EntityModel
{
    public class DataModel
    {
        public string Title { get; set; }
        public string SiteUrl { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string SiteId { get; set; }
        public DateTime? ContentDate { get; set; }
        public string ProjectID { get; set; }
        public int Reply { get; set; }
        public int PageView { get; set; }
    }
}

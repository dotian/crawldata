using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class Forum
    {
        public int Cid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ContentDate { get; set; }
        public string Author { get; set; }
        public string SrcUrl { get; set; }
        public int PageView { get; set; }
        public int Reply { get; set; }
        public int SiteId { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

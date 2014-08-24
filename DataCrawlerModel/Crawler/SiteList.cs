using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class SiteList
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string PlateName { get; set; }
        public string SiteUrl { get; set; }

        public int SiteType { get; set; }
        public int Tid { get; set; }
        public string SiteEncoding { get; set; }
        public int SiteRank { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Remark { get; set; }
    }
}

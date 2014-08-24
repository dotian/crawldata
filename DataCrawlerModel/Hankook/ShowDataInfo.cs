using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    public class ShowDataInfo
    {

        public int Id { get; set; }
        public string DataName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string SrcUrl { get; set; }
        public string Tag { get; set; }
        public string SiteName { get; set; }
        public string Time { get; set; }
        public string Analysis { get; set; }

        public string Suggest { get; set; }

    }
}

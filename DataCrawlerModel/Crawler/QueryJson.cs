using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
    [Serializable]
    public class QueryJson
    {
        public QueryJson() { }
        public int pid { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public int analysis { get; set; }
        public int dataType { get; set; }
        public string matchKey { get; set; }
        public int matchRule { get; set; }
        public int attention { get; set; }
        public int hot { get; set; }
        public int showStatus { get; set; }

        public int pageIndex { get; set; }
    }
}

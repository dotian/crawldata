using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class TagList
    {
        public int Id { get; set; }
        public int Tid { get; set; }
        public string TagName { get; set; }
        public string KoreanTranslate { get; set; }
    }
}

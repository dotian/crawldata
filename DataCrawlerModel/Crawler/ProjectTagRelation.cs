using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
   public class ProjectTagRelation
    {
       public int Id { get; set; }
       public int ProjectId { get; set; }
       public string ProjectName { get; set; }

       //public TagInfo TagId { get; set; }

       public TagList TagList { get; set; }
    }
}

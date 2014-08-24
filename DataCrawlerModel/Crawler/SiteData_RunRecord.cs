using System;
using System.Collections.Generic;

using System.Text;


namespace DataCrawler.Model.Crawler
{
    /// <summary>
    /// 更新数据的时候使用的Id的值
    /// </summary>
    public class SiteData_RunRecord
    {
        public int min_runId { get; set; }
        public int min_runId_next { get; set; }
        public int max_runId { get; set; }

        public int min_forumId { get; set; }
        public int min_forumId_next { get; set; }

        public int min_newsId { get; set; }
        public int min_newsId_next { get; set; }

        public int min_blogId { get; set; }
        public int min_blogId_next { get; set; }

        public int min_microblogId { get; set; }
        public int min_microblogId_next { get; set; }
      

    }
}

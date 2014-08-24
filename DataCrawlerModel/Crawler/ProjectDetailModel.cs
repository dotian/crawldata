using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class ProjectDetailModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
      
        public string MatchingTypeName { get; set; }
        public string MatchingRule { get; set; }
        public string EmpId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ForumNum { get; set; }
        public int NewsNum { get; set; }
        public int BlogNum { get; set; }
        public int MicroBlogNum { get; set; }
        public int SamplingValue { get; set; }

        /// <summary>
        /// 项目类型 0常抓项目 1实体项目
        /// </summary>
        public int ProjectType { get; set; }
        /// <summary>
        /// 数据来源 1论坛,5微博
        /// </summary>
        public int ClassId { get; set; }

        public int MatchingRuleType { get; set; }        
    }
}

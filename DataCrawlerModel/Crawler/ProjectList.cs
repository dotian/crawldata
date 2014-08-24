using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
   
    public class ProjectList
    {
        //说明 一个论坛项目 可以 有 新闻,论坛等站点
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
        public int MatchingRuleType { get; set; }
        public string MatchingRuleTypeName { get; set; }
        public string MatchingRule { get; set; }
        public string RssKey { get; set; }
        public string EmpId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClassId { get; set; }
        public int ForumNum { get; set; }
        public int NewsNum { get; set; }
        public int BlogNum { get; set; }
        public int MicroBlogNum { get; set; }
        public int PList_RunStatus { get; set; }
        

        //ProjectId,ProjectName,MatchingRuleType,MatchingRule,CreateDate,EndDate,ApprovedStatus


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    public class ProjectInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string MatchingRule { get; set; }
        public string EmpId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RunStatus { get; set; }

    }
}

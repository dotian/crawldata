using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class CategoryInfo
    {

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CateId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 员工登录名
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int ClassId { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 站点总数
        /// </summary>
        public int SiteCount { get; set; }

    }
}

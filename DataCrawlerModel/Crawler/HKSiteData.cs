using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class HKSiteData
    {
        /// <summary>
        /// 序号Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 序号字符串
        /// </summary>
        public string  SerilStr { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 标题链接
        /// </summary>
        public string SiteUrl { get; set; }
        /// <summary>
        /// 显示的标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 媒体名
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 显示的时间
        /// </summary>
        public DateTime ContentDate { get; set; }

    }
}

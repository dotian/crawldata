using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WriteSpiderAgain.EntityModel
{
    public class CrawlTarget
    {
        /// <summary>
        /// 站点Url
        /// </summary>
        public string SiteUrl { get; set; }
        private string siteEncoding;
        public string SiteEncoding
        {
            get { return siteEncoding; }
            set { siteEncoding = value; }
        }

        /// <summary>
        /// 模板
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int RunStatus { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 站点Id
        /// </summary>
        public string SiteId { get; set; }

        public int Tid { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 下一页数量
        /// </summary>
        public int NextPageCount { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }

        public int LevelIndex { get; set; }

    }
}

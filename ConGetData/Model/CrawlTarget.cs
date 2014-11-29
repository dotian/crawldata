using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConGetData.Model
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
        
        public string PostContent { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        public string TemplateContent { get; set; }

        /// <summary>
        /// 状态, 1 运行, 99 删除, 0 暂停
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

        public string Tid { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 翻页指针, 初始值为-1
        /// </summary>
        private int nextPageCount = -1;
        public int NextPageCount
        {
            get { return nextPageCount; }
            set { nextPageCount = value; }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }

        public int LevelIndex { get; set; }

        public XmlTemplate XmlTemplate { get; set; }

        public SiteUseTypeEnum SiteUseType { get; set; }
    }
}

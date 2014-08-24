using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConPullSiteData
{
    /// <summary>
    /// 韩泰 新闻Rss
    /// </summary>
    public class RssDataInfo
    {
        public int ProjectId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string Published { get; set; }
        /// <summary>
        /// 描述
        /// </summary>

        public string description { get; set; }
        /// <summary>
        /// 链接
        /// </summary>

        public string link { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
 
        public string type { get; set; }
        /// <summary>
        /// 标签
        /// </summary>

        public string tags { get; set; }
        /// <summary>
        /// 内容
        /// </summary>

        public string contend { get; set; }
        /// <summary>
        /// 调性
        /// </summary>

        public int analysis { get; set; }

        /// <summary>
        /// 媒体名,在 标题的后面
        /// </summary>
        public string sitename { get; set; }
    }
}

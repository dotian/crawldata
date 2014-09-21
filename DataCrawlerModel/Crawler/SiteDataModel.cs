using System;
using System.Collections.Generic;

using System.Text;

namespace DataCrawler.Model.Crawler
{
    public class SiteDataModel
    {
        /// <summary>
        ///  数据集来源Cid
        /// </summary>
        public int SD_Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容Id
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 内容抓取时间
        /// </summary>
        public DateTime ContentDate { get; set; }

        /// <summary>
        /// 站点Url
        /// </summary>
        public string SrcUrl { get; set; }

        /// <summary>
        /// 站点ID
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// 版块名，如阿尔法罗密欧论坛
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 媒体名，如汽车之家
        /// </summary>
        public string PlateName { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 抓取时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 调性, 1:正, 2:中，3：负，4：争
        /// </summary>
        public int Analysis { get; set; }

        /// <summary>
        /// 关注
        /// </summary>
        public int Attention { get; set; }

        /// <summary>
        /// 第一个标签
        /// </summary>
        public string Tag1 { get; set; }

        /// <summary>
        /// 第二个标签
        /// </summary>
        public string Tag2 { get; set; }

        /// <summary>
        /// 第三个标签
        /// </summary>
        public string Tag3 { get; set; }

        /// <summary>
        /// 第四个标签
        /// </summary>
        public string Tag4 { get; set; }

        /// <summary>
        /// 第五个标签
        /// </summary>
        public string Tag5 { get; set; }

        /// <summary>
        /// 第六个标签
        /// </summary>
        public string Tag6 { get; set; }

        /// <summary>
        /// 热帖
        /// </summary>
        public int Hot { get; set; }

        /// <summary>
        /// 状态: 0未审核,1已删除,2预审核,3已审核
        /// </summary>
        public int ShowStatus { get; set; }

        /// <summary>
        /// 数据类型 论坛 1,新闻 2,博客 3,微博 5
        /// </summary>
        public int SiteType { get; set; }
    }
}

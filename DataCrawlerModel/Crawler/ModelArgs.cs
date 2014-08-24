using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataCrawler.Model.Crawler
{
    /// <summary>
    /// 配置文件 参数类
    /// </summary>
    public static class ModelArgs
    {

        /// <summary>
        /// 接收 论坛常抓站点的数据 分类ID
        /// </summary>
        public static int RC_Forum_CateId { get; set;}
           

        /// <summary>
        /// 接收 新闻常抓站点的数据 分类ID
        /// </summary>
        public static int RC_News_CateId { get; set; }

        /// <summary>
        /// 接收 博客常抓站点的数据 分类ID
        /// </summary>
        public static int RC_Blog_CateId { get; set; }

    }
}

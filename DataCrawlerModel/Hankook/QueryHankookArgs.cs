using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    public class QueryHankookArgs
    {
        public QueryHankookArgs() { }
        /// <summary>
        /// QueryHankookArgs 构造函数
        /// </summary>
        /// <param name="type">数据来源</param>
        public QueryHankookArgs(string type)
        {
          
            if (type=="news")
            {
                //来源是 rss  新闻
                datatype = "news";

            }
            else if (type == "sitedata")
            {
                //数据 来源是  爬虫
                datatype = "sitedata";
                start = "";
                end = "";
                file1 = 1;
                file2 = 0;
                file3 = "";
                pageIndex = 1;
            }

        }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string datatype { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string start { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string end { get; set; }

        /// <summary>
        ///  1 标题+内容 ;  2 媒体名 ; 3;标签
        /// </summary>
        public int  file1 { get; set; }

        /// <summary>
        /// 目前 作用未知
        /// </summary>
        public int file2 { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string file3 { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 竞争社Id
        /// </summary>
        public int contendId { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int pagesize { get; set; }
        /// <summary>
        /// 数据类型 1论坛  2 新闻 3博客和微博(这里合并的)
        /// </summary>
        public int sitetype { get; set; }
        /// <summary>
        /// 调性 0 所有, 1正 2中 3负
        /// </summary>
        public int analysis { get; set; }

        /// <summary>
        /// 关注 attention=1
        /// </summary>
        public int attention { get; set; }

        /// <summary>
        /// 显示的状态 1处理中 2处理完成, 这里的 0,表示查询处理中和处理完成的
        /// </summary>
        public int showstatus { get; set; }

    }
}

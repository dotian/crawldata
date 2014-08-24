using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Crawler
{

    public class QueryDataModelParms
    {
        public QueryDataModelParms()
        {

            pagesize = 13;
            pageindex = 1;
            sitetype = 1;
            searchType = 0;
            searchKey = "";
            startTime = "";
            endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            showstatus = -1;
            attention = -1;
            analysis = -1;
            hot = -1;
        }

        /// <summary>
        /// 初始化查询参数
        /// </summary>
        /// <param name="pid">项目Id</param>
        /// <param name="siteType">项目类型 1 论坛, 2新闻, 3博客,5 新浪微博</param>
        public QueryDataModelParms(int pid, int siteType)
        {
            this.projectId = pid;
            pagesize = 13;
            pageindex = 1;
            this.sitetype = siteType;
            searchType = 0;
            searchKey = "";
            startTime = "";
            endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            showstatus = -1;
            attention = -1;
            analysis = -1;
            hot = -1;
        }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        ///  页大小
        /// </summary>
        public int pagesize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageindex { get; set; }
        /// <summary>
        ///  1 论坛, 2新闻, 3博客,5 新浪微博
        /// </summary>
        public int sitetype { get; set; }
        /// <summary>
        /// 0 标题及内容, 1 标题, 2 内容
        /// </summary>
        public int searchType { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string searchKey { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 0 未审核 1预审核 2已审核 99 已删除
        /// </summary>
        public int showstatus { get; set; }
        /// <summary>
        /// 关注 1关注
        /// </summary>
        public int attention { get; set; }
        /// <summary>
        /// 热帖
        /// </summary>
        public int hot { get; set; }
        /// <summary>
        /// 调性 1234 正 中 负 争, 默认 0 显示同
        /// </summary>
        public int analysis { get; set; }

    }


}

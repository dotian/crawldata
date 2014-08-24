using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    public class PieReport
    {
        /// <summary>
        /// 正 | 论坛
        /// </summary>
        public int Data1 { get; set; }
        /// <summary>
        /// 中 | 新闻
        /// </summary>
        public int Data2 { get; set; }
        /// <summary>
        /// 负 | 博客
        /// </summary>
        public int Data3 { get; set; }
        /// <summary>
        /// 无 | 微博
        /// </summary>
        public int Data4 { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public double AppearRate { get; set; }
    }
}

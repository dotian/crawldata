using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    public class TopicReport
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  显示的Title
        /// </summary>
        public string MessTitle { get; set; }
        /// <summary>
        /// 出现的次数
        /// </summary>
        public int AppearCount { get; set; }

        /// <summary>
        /// 比例 29.40%
        /// </summary>
        public string AppearRate { get; set; }
    }
}

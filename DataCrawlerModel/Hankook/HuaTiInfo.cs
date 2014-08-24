using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    /// <summary>
    /// 话题
    /// </summary>
    public class HuaTiInfo
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 统计出来的数据的值
        /// </summary>
        public int value { get; set; }
    }
}

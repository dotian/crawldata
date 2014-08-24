using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.Model.Hankook
{
    /// <summary>
    /// 饼图数据
    /// </summary>
    public class PieData
    {
        /// <summary>
        /// 来源
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WriteSpiderAgain
{
    /// <summary>
    /// 静态参数 全局的
    /// </summary>
    public class ModelArgs
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        public static int MaxThreadNum=1;

        /// <summary>
        /// 连接数
        /// </summary>
        public static int ConnectionPoolSize=200;
        /// <summary>
        /// 抓取数量
        /// </summary>
        public static int QueryDataSize=1000;

        /// <summary>
        /// 超时时间10秒
        /// </summary>
        public static int SiteTimeOut = 10000;

        /// <summary>
        /// 设置是否继续运行
        /// </summary>
        public static bool RunStatus = false;

        public static int ProjectType {
            get { return int.Parse(ConfigurationManager.AppSettings["ProjectType"].ToString()); }
        
        }
    }
}

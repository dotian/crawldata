using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ConGetData.Model
{
    public class ModelArgs
    {
        private static int maxThreadNum;
        /// <summary>
        /// 最大线程数
        /// </summary>
        public static int MaxThreadNum
        {
            get
            {
                if (maxThreadNum <= 0)
                {
                    maxThreadNum = int.Parse(ConfigurationManager.AppSettings["MaxThreadNum"]);
                }
                return maxThreadNum;
            }

        }

        /// <summary>
        /// 抓取数量
        /// </summary>
        public static int QueryDataSize = 20000;

        /// <summary>
        /// 超时时间10秒
        /// </summary>
        public static int SiteTimeOut = 10000;

        /// <summary>
        /// 设置是否继续运行
        /// </summary>
        public static bool RunStatus = false;

        public static int projectType = -1;
        public static int ProjectType
        {
            get
            {
                if (projectType < 0)
                {
                    projectType = int.Parse(ConfigurationManager.AppSettings["ProjectType"].ToString());
                }
                return projectType;
            }
            set { projectType = value; }

        }

        private static string xmlTempPath;
        public static string XmlTempPath
        {
            get
            {
                if (string.IsNullOrEmpty(xmlTempPath))
                {
                    xmlTempPath = ConfigurationManager.AppSettings["XmlTempPath"].ToString();
                }
                return xmlTempPath;
            }
        }


    }
}

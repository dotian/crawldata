using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace ConGetData.ConMicroblog
{
    public class ModelArgs
    {

        /// <summary>
        ///  待开启的程序的 运行目录
        /// </summary>
        public static string ProcessWorkingDirectory { get; set; }
        /// <summary>
        ///  待开启的 程序名称
        /// </summary>
        public static string ProcessFileName { get; set; }
        /// <summary>
        /// 指定在系统启动进程时新窗口应如何显示。
        /// </summary>
        public static ProcessWindowStyle ProcessWindowStyle { get; set; }
        /// <summary>
        /// cookie 文件存放的位置
        /// </summary>
        public static string WriteFilePath { get; set; }
        /// <summary>
        /// 停顿时间
        /// </summary>
        public static int SleepTime { get; set; }

        public static string CookieStr { get; set; }
        public static void InitModeArgs()
        {
            ProcessWorkingDirectory = ConfigurationManager.AppSettings["ProcessWorkingDirectory"].ToString();
            ProcessFileName = ConfigurationManager.AppSettings["ProcessFileName"].ToString();
            ProcessWindowStyle = (ProcessWindowStyle)Convert.ToInt32(ConfigurationManager.AppSettings["ProcessWindowStyle"]);
            WriteFilePath = ConfigurationManager.AppSettings["WriteFilePath"];
            SleepTime = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTime"]);

        }

    }
}

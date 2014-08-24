using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogNet;
using log4net;
using log4net.Config;
namespace ConPullSiteData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LogNet.LogBLL.InitLog();
                LogNet.LogBLL.Info("开启相关服务,程序正在运行中...");
                Console.WriteLine("开启相关服务,程序正在运行中...");
                new DataCrawler.BLL.Crawler.PullDataBLL().PullRunManagerAction();
                //new ProcTextBLL().ProcWorkManager();
                // new ViewTextBLL().ViewWorkManager();
               // new FuncTextBLL().FuncWorkManager();
                //new TriggerTextBLL().TriggerWorkManager();
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Fatal("Main", ex);
            }

            Console.WriteLine("程序运行完毕!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using WriteSpiderAgain.ManagerBLL;
using log4net;
using log4net.Config;
namespace WriteSpiderAgain
{
    class Program
    {
        static Program program = new Program();
        static void Main()
        {
            Console.WriteLine("--开始--");
            Page.InitLog();
            Page.log.Info("Main 开始");
            program.InitCheck();
            try
            {
                if (ModelArgs.RunStatus == true)
                {
                    //继续运行
                    ModelArgs.RunStatus = false;

                    ClrowBLL clrowSpider = new ClrowBLL();
                    clrowSpider.ClrowSpiderMain();
                }
            }
            catch (Exception ex)
            {
                Page.log.Error("ClrowSpiderMain 抓取主方法异常", ex);
            }
           
            Console.WriteLine("--完毕--");
        }

        public void InitCheck()
        {
            //初始化线程数,和检查配置文件

            try
            {
                ModelArgs.QueryDataSize = int.Parse(ConfigurationManager.AppSettings["QueryDataSize"].ToString());
                ModelArgs.SiteTimeOut = int.Parse(ConfigurationManager.AppSettings["SiteTimeOut"].ToString());

                ModelArgs.MaxThreadNum = int.Parse(ConfigurationManager.AppSettings["MaxThreadNum"].ToString());
                ModelArgs.RunStatus = true;
            }
            catch (Exception ex)
            {

                Page.log.Error("InitChec配置文件解析异常", ex);
            }
                //读取配置文件
          
            //ModelArgs.ProjectType = int.Parse(ConfigurationManager.AppSettings["ProjectType"].ToString());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.BLL;
using ConGetData.ConMicroblog;

namespace ConGetData
{
    public class Program
    {
        static void Main()
        {
            LogNet.LogBLL.Info("程序开始");
            MainWork mainWork = new MainWork();
            try
            {
                string workMethord = System.Configuration.ConfigurationManager.AppSettings["WorkMethod"].ToString().ToUpper();
                if (workMethord=="TEST")
                {
                    mainWork.CrawlStartSinger();
                }
                else if (workMethord == "WORK")
                {
                    ModelArgs.InitModeArgs();
                    mainWork.TaskCrawlStart();
                }
                else if (workMethord == "CK_SITE")
                {
                    new CK_SiteManager().WorkStart();
                }
                else if (workMethord == "MICROBLOG")
                {
                    new MicroblogWork().OpenWork();
                }
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Fatal("Main", ex);
            }
            Console.WriteLine("从我这里结束!");
        }
    }
}

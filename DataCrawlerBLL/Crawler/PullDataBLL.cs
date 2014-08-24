using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;

using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
namespace DataCrawler.BLL.Crawler
{
    /// <summary>
    /// 拉数据
    /// </summary>
    public class PullDataBLL
    {
        public void PullRunManagerAction()
        {
            Thread.Sleep(2000);
            PullRunManagerThread();
            lock (objRun)
            {
                objRun = new object();
            }
            
            //.Connection.ConnectionString;
        }
        public void PullRunManagerThread()
        {
            int sleepPullTime = int.Parse(ConfigurationManager.AppSettings["SleepPullTime"].ToString());
            
            lock (objRun)
            {
                while (true)
                {
                    Thread t = new Thread(PullRunManager);
                    t.Start();
                    t.Join();
                    objRun = new object();
                    Thread.Sleep(sleepPullTime*1000);
                    LogNet.LogBLL.Info("PullRunManagerThread 轮循拉数据成功");
                }
            }
        }

        
       
         private static object objRun = new object();
         public void PullRunManager()
         {
             try
             {
                 PullDataDAL sqlDalAction = new PullDataDAL();
                 sqlDalAction.PullDataFirstService();
             }
             catch (Exception ex)
             {
                 LogNet.LogBLL.Error("PullRunManager", ex);
                
             }
            
         }
    }
}

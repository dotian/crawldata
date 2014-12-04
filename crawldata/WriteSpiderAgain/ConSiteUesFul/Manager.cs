using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WriteSpiderAgain;
using WriteSpiderAgain.ServiceDAL;

using System.Data.SqlClient;
using System.Threading;
using WriteSpiderAgain.ManagerBLL;


namespace ConSiteUesFul
{
    public class Manager
    {
        public ThreadCounter threadCounter = new ThreadCounter();
        public static int CrawlListCount = 0;
        public static IList<SiteList> CrawlListRun = new List<SiteList>();
        public void TaskCrawlStart()
        {
            int runningMark = 0;
            char[] runMark = { '-', '/', '|', '\\' };
            int MaxThreadNum = 30;

            CrawlListRun = GetSiteList();

            if (CrawlListRun.Count<=0)
            {
                return;  
            }
            while (true)
            {
                Thread.Sleep(1000);
                #region 工作信息输出
                int left = Console.CursorLeft;
                int top = Console.CursorTop;
                Console.SetCursorPosition(0, top);
                Console.WriteLine(new String(' ', 200));
                Console.SetCursorPosition(left, top);
                if (runningMark > 3)
                {
                    runningMark = 0;
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("-- [ ");
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write(runMark[runningMark]);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(" ]");
                Console.Write(" ThreadNum:" + threadCounter.getThreadNum("tc"));
                Console.Write(" Queue:" + CrawlListRun.Count);
                Console.Write(" Pending:" + threadCounter.getThreadNum("pc"));
                Console.Write(" Error:" + threadCounter.getThreadNum("ec"));
                Console.Write(" Finished:" + threadCounter.getThreadNum("fc"));

                Console.Write('\n');
                Console.SetCursorPosition(left, top);
                runningMark++;
                #endregion

              
                #region  处理工作队列
                while ((threadCounter.getThreadNum("tc") < MaxThreadNum) && CrawlListRun.Count > 0)
                {
                    //没有 批次,不应该开启新的队列,而要等到所有队列处理结束
                    threadCounter.writerCounter("pcAdd");
                    threadCounter.writerCounter("tcAdd"); //放在线程外调整线程数。前防止主线程提前更新该计数器。
                    CrawlTargetArgs CrawlTargetArgs = new CrawlTargetArgs(threadCounter, CrawlListRun[0]);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(RunSpider), CrawlTargetArgs);
                    CrawlListRun.RemoveAt(0);
                }

                if (CrawlListRun.Count <= 0)
                {


                    break;
                }
                #endregion
            }
        }
        public List<SiteList> GetSiteList()
        {
           
            List<SiteList> list = new List<SiteList>();

            string sql = "select SiteId,SiteUrl from  siteslist_ConfirmUseful where UsefulStatus is null";

            try
            {
                SqlDataReader reader = HelperSQL.ExecuteReader(sql);

                while (reader.Read())
                {
                    SiteList site = new SiteList();
                    site.SiteId = Field.GetInt32(reader, "SiteId");
                    site.SiteUrl = Field.GetString(reader, "SiteUrl");
                    list.Add(site);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(100000);
            }
              return list;
        }
        HttpHelper _hh = new HttpHelper();
        public void RunSpider(object obj)
        {
            CrawlTargetArgs  clawtarget = obj as CrawlTargetArgs;
            SiteList site = clawtarget.CrawlTarget;
            try
            {
                string mess = "";
                bool b = _hh.Open(site.SiteUrl, Encoding.Default, ref mess);
                //进入这里直接返回0
                //超时, 连接被意外关闭, 远程主机强迫, time out, 403, 500
                if (b == false || string.IsNullOrEmpty(mess))
                {
                    UpdateSiteList(site.SiteId, 1, "访问无数据");
                }
                //else if (mess.Contains("404") || mess.ToLower().Contains("not found") || mess.ToLower().Contains("time out") || mess.Contains("403") || mess.Contains("500") ||

                //    (mess.Contains("超时") || mess.Contains("连接被意外关闭") || mess.Contains("远程主机强迫")))
                //{
                //    UpdateSiteList(site.SiteId, 2, "网页包含 404,403,500,访问超时等");
                //}
                else if (mess.Length > 20000)
                {
                    UpdateSiteList(site.SiteId, 0, "正常");
                }
                else
                {
                    UpdateSiteList(site.SiteId, 3, "文本长度少于2万");
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine("多线程内部异常:\t" + ex);
                threadCounter.writerCounter("ecAdd");//错误
               
            }
            finally
            {
                threadCounter.writerCounter("tcSub");//释放
            }
           

        }

        public int UpdateSiteList(int sitsid,int status,string remark)
        {

            string sql = "update siteslist_ConfirmUseful set UsefulStatus = " + status + " ,Remark = '" + remark + "' where SiteId = " + sitsid;

            return HelperSQL.ExecNonQuery(sql);

        }
    }



    /// <summary>
    ///  生成SongModel的参数包装器
    /// </summary>
    public class CrawlTargetArgs
    {
        public CrawlTargetArgs() { }
        public CrawlTargetArgs(ThreadCounter treadCounter, SiteList CrawlTarget)
        {
            this.ThreadCounter = treadCounter;
            this.CrawlTarget = CrawlTarget;
        }
        public ThreadCounter ThreadCounter { get; set; }
        public SiteList CrawlTarget { get; set; }

    }

    public  class SiteList
    {

        public int SiteId { get; set; }
        public string SiteUrl { get; set; }


    }
}

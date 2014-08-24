using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;
using ConGetData.DAL;
using System.Threading;

namespace ConGetData.BLL
{
    public class CK_SiteManager
    {
        LogicBLL logicAction = new LogicBLL();
        public ThreadCounter threadCounter = new ThreadCounter();
        public static int CrawlListCount = 0;
        public static IList<CK_SiteList> CrawlListRun = new List<CK_SiteList>();
        public void WorkStart()
        {
            CrawlListRun = ServiceLocator.GetServiceDAL().GetCK_listService();
            CrawlListCount = CrawlListRun.Count;

            if (CrawlListCount <= 0)
            {
                Console.WriteLine("可运行的 任务为0");
                return;
            }
            int runningMark = 0;
            char[] runMark = { '-', '/', '|', '\\' };
            int MaxThreadNum = ModelArgs.MaxThreadNum;

            ModelArgs.RunStatus = true;

            while (ModelArgs.RunStatus == true)
            {
                Thread.Sleep(1000);
                #region 工作信息输出
                int left = Console.CursorLeft;
                int top = Console.CursorTop;
                Console.SetCursorPosition(0, top);
                Console.SetCursorPosition(left, top);
                if (runningMark > 3)
                {
                    runningMark = 0;
                }

                string outPutMess = string.Format("-- [{0}] ThreadNum:{1} Queue:{2} Pending:{3} Error:{4} Finished:{5}",
                    runMark[runningMark],
                    threadCounter.getThreadNum("tc"),
                    CrawlListRun.Count,
                    threadCounter.getThreadNum("pc"),
                    threadCounter.getThreadNum("ec"),
                    threadCounter.getThreadNum("fc"));
                Console.WriteLine(outPutMess);
                Console.SetCursorPosition(left, top);
                runningMark++;
                #endregion

                #region  填充队列
                if (CrawlListRun.Count == 0)
                {
                    Thread.Sleep(120 * 1000);//每次任务 结束之后,停2分钟
                    return;
                }
                #endregion
                #region  处理工作队列
                while ((threadCounter.getThreadNum("tc") < MaxThreadNum) && CrawlListRun.Count > 0)
                {
                    //没有 批次,不应该开启新的队列,而要等到所有队列处理结束
                    threadCounter.writerCounter("pcAdd");
                    threadCounter.writerCounter("tcAdd"); //放在线程外调整线程数。前防止主线程提前更新该计数器。
                    CrawlSiteArgs CrawlTargetArgs = new CrawlSiteArgs(threadCounter, CrawlListRun[0]);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CrawlWorkAction), CrawlTargetArgs);
                    CrawlListRun.RemoveAt(0);
                }
                #endregion
            }
        }

        public void CrawlWorkAction(object obj)
        {
            Thread.Sleep(2000);
            CrawlSiteArgs CrawlTargetArgs = obj as CrawlSiteArgs;
            CK_SiteList target = CrawlTargetArgs.CrawlTarget;
            ThreadCounter threadCounter = CrawlTargetArgs.ThreadCounter;
            IServiceDAL dalAction = ServiceLocator.GetServiceDAL();
            try
            {

                string html = new HttpHelper().Open(target.SiteUrl, "gb2312");

                if (html.Length > 20000)
                {
                    dalAction.UpdateCKStatusService(target.SiteId, 1);
                }
                else if (html.Length > 2000)
                {
                    dalAction.UpdateCKStatusService(target.SiteId, 2);
                }
                else
                {
                    dalAction.UpdateCKStatusService(target.SiteId, 3);
                }

                threadCounter.writerCounter("fcAdd");//完成
            }
            catch
            {
                dalAction.UpdateCKStatusService(target.SiteId, 3);
                threadCounter.writerCounter("ecAdd");//错误
            }
            finally
            {
                threadCounter.writerCounter("tcSub");//释放
            }
        }

    }
}

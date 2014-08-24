using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.DAL;
using ConGetData.Model;
using System.Threading;

namespace ConGetData.BLL
{
    public class MainWork
    {
        LogicBLL logicAction = new LogicBLL();
        public ThreadCounter threadCounter = new ThreadCounter();
        public static int CrawlListCount = 0;
        public static IList<CrawlTarget> CrawlListRun = new List<CrawlTarget>();
        public void TaskCrawlStart()
        {
            //首次加速 开启任务列表
            new MicroblogGetCookie().GetCookieWork();
            CrawlListRun = logicAction.GetCrawtarget();
            CrawlListCount = CrawlListRun.Count;

            if (CrawlListCount<=0)
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

                    //更新新浪微博cookie
                    new MicroblogGetCookie().GetCookieWork();

                    CrawlListRun = logicAction.GetCrawtarget();
                    CrawlListCount = CrawlListRun.Count;
                    threadCounter = new ThreadCounter();  //重新计数
                    // dalAction.InsertRunRecord();

                }
                #endregion
                #region  处理工作队列
                while ((threadCounter.getThreadNum("tc") < MaxThreadNum) && CrawlListRun.Count > 0)
                {
                    //没有 批次,不应该开启新的队列,而要等到所有队列处理结束
                    threadCounter.writerCounter("pcAdd");
                    threadCounter.writerCounter("tcAdd"); //放在线程外调整线程数。前防止主线程提前更新该计数器。
                    CrawlTargetArgs CrawlTargetArgs = new CrawlTargetArgs(threadCounter, CrawlListRun[0]);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CrawlWorkAction), CrawlTargetArgs);
                    CrawlListRun.RemoveAt(0);
                }
                #endregion
            }
        }


        /// <summary>
        /// 单线程
        /// </summary>
        public void CrawlStartSinger()
        {
            CrawlListRun = logicAction.GetCrawtarget();
            CrawlListCount = CrawlListRun.Count;
            for (int i = 0; i < CrawlListCount; i++)
            {
                //if (CrawlListRun[i].ClassName == "microblog" || CrawlListRun[i].SiteId == "99463")
                //{
                    CrawlTargetArgs CrawlTargetArgs = new CrawlTargetArgs(threadCounter, CrawlListRun[i]);
                    CrawlWorkAction(CrawlTargetArgs);
	           // }
                        
              
            }
        }

        //实际处理函数
        public static void CrawlWorkAction(object obj)
        {
            Thread.Sleep(2000);
            CrawlTargetArgs CrawlTargetArgs = obj as CrawlTargetArgs;
            CrawlTarget target = CrawlTargetArgs.CrawlTarget;
            ThreadCounter threadCounter = CrawlTargetArgs.ThreadCounter;
            try
            {
                #region
                switch (target.XmlTemplate.SiteName.ToLower())
                {
                    case "forum":
                        new ForumWork().RunWork(target);
                        break;
                    case "news":
                         new NewsWork().RunWork(target);
                        break;
                    case "blog":
                        new BlogWork().RunWork(target);
                        break;
                    case "microblog":
                        RunMicroblogPages(target);
                        break;
                    default:
                        break;
                }
                #endregion
                Console.WriteLine("ProjectId:{0} SiteId:{1} Tid:{2} 抓取成功", target.ProjectId, target.SiteId,target.Tid);
                threadCounter.writerCounter("fcAdd");//完成
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error(target.SiteId, ex);
                Console.WriteLine("ProjectId:{0} SiteId:{1} Tid:{2} 抓取失败", target.ProjectId, target.SiteId, target.Tid);
                threadCounter.writerCounter("ecAdd");//错误
            }
            finally
            {
                threadCounter.writerCounter("tcSub");//释放
            }
        }

        public static void RunMicroblogPages(CrawlTarget target)
        {
            CrawlTarget t = target;

            for (int i = 0; i < 10; i++)
            {
                t.SiteUrl = t.SiteUrl + "&b=1&page=" + i;
                new MicroblogWork().RunWork(t);
            }
            
        }
    }
}

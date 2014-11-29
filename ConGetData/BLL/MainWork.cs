using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.DAL;
using ConGetData.Model;
using System.Threading;

namespace ConGetData.BLL
{
    public class CrawlTask
    {
        public CrawlTask(string name, int maxThreadNumber)
        {
            this.ThreadCounter = new ThreadCounter();
            this.CrawlTargetList = new List<CrawlTarget>();
            this.MaxThreadNumber = maxThreadNumber;
            this.TaskName = name;
        }

        public int MaxThreadNumber { get; set; }
        public ThreadCounter ThreadCounter { get; set; }
        public IList<CrawlTarget> CrawlTargetList { get; set; }
        public string TaskName { get; set; }
    }

    public class MainWork
    {
        private LogicBLL logicAction = new LogicBLL();

        public void TaskCrawlStart()
        {
            ModelArgs.RunStatus = true;

            var commonSitesTask = new CrawlTask("Common Sites Tasks", ModelArgs.MaxCommonThreadNum);
            var projectRelatedSitesTask = new CrawlTask("Search Sites Tasks", ModelArgs.MaxProjectRelatedThreadNum);

            while (ModelArgs.RunStatus == true)
            {
                Thread.Sleep(1000);

                ProcessTask(projectRelatedSitesTask);
                ProcessTask(commonSitesTask);
            }
        }

        private void ProcessTask(CrawlTask commonSitesTask)
        {
            WorkInfoOutput(commonSitesTask);

            #region  填充队列

            if (commonSitesTask.CrawlTargetList.Count == 0)
            {
                Thread.Sleep(120 * 1000);//每次任务 结束之后,停2分钟

                //更新新浪微博cookie
                new MicroblogGetCookie().GetSinaCookieWork();

                ResetCrawlWorkTask(commonSitesTask);  //重新读取列表，重设计数

                if (commonSitesTask.CrawlTargetList.Count <= 0)
                {
                    Console.WriteLine("可运行的 任务为0");
                    throw new Exception();
                }
            }

            #endregion

            #region  处理工作队列

            ProcessWorkList(commonSitesTask);

            #endregion
        }

        private static void ProcessWorkList(CrawlTask task)
        {
            while ((task.ThreadCounter.getThreadNum("tc") < task.MaxThreadNumber) && task.CrawlTargetList.Count > 0)
            {
                //没有 批次,不应该开启新的队列,而要等到所有队列处理结束
                task.ThreadCounter.writerCounter("pcAdd");
                task.ThreadCounter.writerCounter("tcAdd"); //放在线程外调整线程数。前防止主线程提前更新该计数器。
                CrawlTargetArgs CrawlTargetArgs = new CrawlTargetArgs(task.ThreadCounter, task.CrawlTargetList[0]);
                ThreadPool.QueueUserWorkItem(new WaitCallback(CrawlWorkAction), CrawlTargetArgs);
                task.CrawlTargetList.RemoveAt(0);
            }
        }

        private static void WorkInfoOutput(CrawlTask task)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.SetCursorPosition(0, top);
            Console.SetCursorPosition(left, top);

            string outPutMess = string.Format("-- Task Name:{5}, ThreadNum:{0} Queue:{1} Pending:{2} Error:{3} Finished:{4}",
                task.ThreadCounter.getThreadNum("tc"),
                task.CrawlTargetList.Count,
                task.ThreadCounter.getThreadNum("pc"),
                task.ThreadCounter.getThreadNum("ec"),
                task.ThreadCounter.getThreadNum("fc"),
                task.TaskName);

            Console.WriteLine(outPutMess);
            Console.SetCursorPosition(left, top);
        }

        private void ResetCrawlWorkTask(CrawlTask task)
        {
            task.CrawlTargetList = logicAction.GetCrawtarget().Where(c => (c.SiteUseType & SiteUseTypeEnum.Search) == 0).ToList();
            task.ThreadCounter = new ThreadCounter();
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
                Console.WriteLine("ProjectId:{0} SiteId:{1} Tid:{2} 抓取成功", target.ProjectId, target.SiteId, target.Tid);
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
                if (t.SiteUrl.Contains("sina"))
                {
                    t.SiteUrl = t.SiteUrl + "&b=1&page=" + i;
                }
                new MicroblogWork().RunWork(t);
            }
        }
    }
}

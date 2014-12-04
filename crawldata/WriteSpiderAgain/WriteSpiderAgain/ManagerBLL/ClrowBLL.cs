using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WriteSpiderAgain.ServiceDAL;
using WriteSpiderAgain.EntityModel;
using System.Diagnostics;
using System.Threading;
using log4net;
using log4net.Config;

namespace WriteSpiderAgain.ManagerBLL
{
   public  class ClrowBLL:Page
   {
       ServiceDALAction dalAction = new ServiceDALAction();
       /// <summary>
       /// 业务逻辑层的 入口
       /// </summary>
       public void ClrowSpiderMain()
       {
           //先执行更新

           string sqlUpdate = "update ProjectDetail set RunStatus = 0 where RunStatus = 1 and  ProjectType = " + ModelArgs.ProjectType;
           try
           {
               HelperSQL.ExecNonQuery(sqlUpdate);
           }
           catch (Exception ex)
           {
               log.Error("ClrowSpiderMain 执行sql语句" + sqlUpdate + "异常", ex);
               return;
           }
           
           //查询所有需要开启的任务数,执行存储过程
           try
           {
               CrawlListRun = dalAction.GetTargetList();
               PrepareTemplate.FormatXMl(CrawlListRun); 
           }
           catch (Exception ex)
           {
               log.Error("ClrowSpiderMain 获取任务结果集 dalAction.GetTargetList() 异常", ex);
               return;
           }
         
           CrawlListCount = CrawlListRun.Count;
           log.Info("CrawlListRun.Count:\t" + CrawlListCount);
           if (CrawlListCount>0)
           {
               try
               {
                   dalAction.InsertRunRecord();
               }
               catch (Exception ex)
               {

                   log.Error("ClrowSpiderMain 记录运行任务 dalAction.InsertRunRecord() 异常", ex);
               }
              
               ModelArgs.RunStatus = true;

               //try
               //{
               //    TaskCrawlStart();
               //}
               //catch (Exception ex)
               //{

               //    log.Error("ClrowSpiderMain 多线程任务开启 TaskCrawlStart 异常", ex);
               //}
               
               //测试单个
               //new SinaWork().GetSinaPage(CrawlListRun[0]);
               //while (true)
              // {
                      for (int i = 0; i < CrawlListCount; i++)
                       {
                           OriginalWork workBll = new OriginalWork();
                           if (int.Parse(CrawlListRun[i].SiteId) >= 0)
                           {
                               workBll.GetWorkPage(CrawlListRun[i]);
                               Console.WriteLine("当前运行第" + i);
                           }
                       }

                   //去重
                   // dalAction.DeleteRepeat();
                   //写记录
                     // dalAction.InsertRunRecord();
             //  }

            
               //开启线程,先更新状态为1,然后 进行抓取
               //然后更改 抓取时间
               //再继续下一个
           }
           else
           {
               ModelArgs.RunStatus = false ;
               return;
           }
          
       }
       public ThreadCounter threadCounter = new ThreadCounter();
       public static int CrawlListCount =0;
       public static IList<CrawlTarget>CrawlListRun = new List<CrawlTarget>();

       public void TaskCrawlStart()
       {
           int runningMark = 0;
           char[] runMark = { '-', '/', '|', '\\' };
           int MaxThreadNum = ModelArgs.MaxThreadNum;
           while (ModelArgs.RunStatus==true)
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

               #region  填充队列
               if (CrawlListRun.Count == 0)
               {
                   Thread.Sleep(120 * 1000);//每次任务 结束之后,停2分钟
                 
                   //dalAction.DeleteRepeat();

                   CrawlListRun = dalAction.GetTargetList();
                   CrawlListCount = CrawlListRun.Count;
                   //重新计数
                   threadCounter = new ThreadCounter();
                   dalAction.InsertRunRecord();
                 
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


       //直接运行线程
       public void CrawlWorkAction(object o)
       {
           Thread.Sleep(2000);
           CrawlTargetArgs CrawlTargetArgs = o as CrawlTargetArgs;
           CrawlTarget CrawlTarget = CrawlTargetArgs.CrawlTarget;
           ThreadCounter threadCounter = CrawlTargetArgs.ThreadCounter;

           try
           {
               //这里运行真正的任务
               string sqlUpdateStatus = "Update ProjectDetail set RunStatus = 1 Where ProjectId = " + CrawlTarget.ProjectId + " and Siteid = " + CrawlTarget.SiteId;
               HelperSQL.ExecNonQuery(sqlUpdateStatus);
               int result=0;

               result = new OriginalWork().GetWorkPage(CrawlTarget);
             
               if (result > 0)
               {
                   Console.WriteLine(string.Format("抓取成功: ProjectId={0}\tSiteId={1}",CrawlTarget.ProjectId,CrawlTarget.SiteId));
               }
               else
               {
                   Console.WriteLine(string.Format("抓取失败: ProjectId={0}\tSiteId={1}", CrawlTarget.ProjectId, CrawlTarget.SiteId));
                   string sqlInsertError = "insert into ErrorList(ProjectId,SiteId,CreateDate) values(" + CrawlTarget.ProjectId + "," + CrawlTarget.SiteId + ",'"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";

                   HelperSQL.ExecNonQuery(sqlInsertError);

               }
             threadCounter.writerCounter("fcAdd");//完成
              
           }
           catch (Exception ex)
           {
               log.Error("\n\n\n ClrowSpiderMain 多线程任务开启 CrawlWorkAction 异常", ex);
               Thread.Sleep(10000);
               //释放线程锁
               threadCounter.writerCounter("ecAdd");//错误
           }
           finally
           {
                //更新最后抓取时间
               string sqlUpdateStatus = "Update ProjectDetail set RunStatus = 0 Where ProjectId = " + CrawlTarget.ProjectId + " and Siteid = " + CrawlTarget.SiteId;
               HelperSQL.ExecNonQuery(sqlUpdateStatus);

               string sqlUpdateCrawlTime = "Update ProjectDetail set CrawlFinishDate = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' Where ProjectId = " + CrawlTarget.ProjectId + " and Siteid = " + CrawlTarget.SiteId;
               HelperSQL.ExecNonQuery(sqlUpdateCrawlTime);

               threadCounter.writerCounter("tcSub");//释放
           }
       }



    }

   /// <summary>
   ///  生成SongModel的参数包装器
   /// </summary>
   public class CrawlTargetArgs
   {
       public CrawlTargetArgs() { }
       public CrawlTargetArgs(ThreadCounter treadCounter, CrawlTarget CrawlTarget)
       {
           this.ThreadCounter = treadCounter;
           this.CrawlTarget = CrawlTarget;
       }
       public ThreadCounter ThreadCounter { get; set; }
       public CrawlTarget  CrawlTarget{ get; set; }

   }
   
   
   
   
   
}

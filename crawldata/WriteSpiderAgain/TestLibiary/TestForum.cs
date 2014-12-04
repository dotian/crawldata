using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WriteSpiderAgain.ManagerBLL;
using WriteSpiderAgain.EntityModel;
using WriteSpiderAgain.ServiceDAL;
using System.Diagnostics;
using WriteSpiderAgain;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Threading;
namespace ZTestLibiary
{
    public class TestForum
    {
        /*首先测试新浪新闻*/
        ServiceDALAction listDal = new ServiceDALAction();
        OriginalWork originalWork = new OriginalWork();
        HttpHelper _hh = new HttpHelper();
        CommonHelper _ch = new CommonHelper();
        IList<CrawlTarget> crawlListRun;


        /// <summary>
        /// 测试从数据库 取一条新浪数据 进行测试
        /// </summary>
        public void TestActionForumsTask()
        {
            Trace.WriteLine("--TestGetTask_1st开始--");

            /* 功能说明:     测试从数据库获取 抓取任务 IList<CrawlTarget>  
               需要准备参数: 无
            * */

            // CrawlListRun = listDal.GetTargetList();
            // int CrawlListCount = CrawlListRun.Count;

            //CrawlTarget ct = CrawlListRun[0];

            TemplateModel temp = new TemplateModel(crawlTarget.TemplateContent);

            string strHTML = File.ReadAllText("../../TsetFile/newsContent.txt");

            Trace.WriteLine("输出temp.TitleRegex:\t" + temp.TitleRegex);

            string resultTitle = _ch.GetMatchRegex(temp.TitleRegex, strHTML);
            resultTitle = Regex.Replace(resultTitle.Trim(), "<.+?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase).Trim();
            Trace.WriteLine("输出resultTitle:\t" + resultTitle);

            Trace.WriteLine("输出temp.ContentDateRegex:\t" + temp.ContentDateRegex);
            string resultContentDate = _ch.GetMatchRegex(temp.ContentDateRegex, strHTML);
            Trace.WriteLine("输出resultContentDate:\t" + resultContentDate);

            Trace.WriteLine("输出temp.ContentRegex:\t" + temp.ContentRegex);
            string resultContent = _ch.NoHTML(_ch.GetMatchRegex(temp.ContentRegex, strHTML));
            Trace.WriteLine("输出resultContentDate:\t" + resultContent);

            Trace.WriteLine("输出temp.SrcUrlRegex:\t" + temp.SrcUrlRegex);
            string resultSrcUrlRegex = _ch.GetMatchRegex(temp.SrcUrlRegex, strHTML);
            Trace.WriteLine("输出resultSrcUrlRegex:\t" + resultSrcUrlRegex);
            Trace.WriteLine("--TestGetTask_1st完毕--");
        }

        public void TestMain_Forum()
        {
            Trace.WriteLine("--TestMain_news开始--");
            TestGetTask_1st();
            TestWork_2nd();
            TestWork_3rd();
            TestWork_4th();
            TestWork_5th();
            Trace.WriteLine("--TestMain_news结束--");
        }

        #region  中间数据库操作

        /// <summary>
        /// 测试从数据库 取一条新浪数据 进行测试
        /// </summary>
        public void TestGetTask_1st()
        {
            Trace.WriteLine("--TestGetNewsTask_1st开始--");

            /* 功能说明:     测试从数据库获取 抓取任务 IList<CrawlTarget>  
               需要准备参数: 无
            * */

            crawlListRun = listDal.GetTargetList();
            int CrawlListCount = crawlListRun.Count;
            originalWork.GetWorkPage(crawlListRun[0]);

            Trace.Assert(CrawlListCount > 0, "TestGetNewsTask_1st 获取到新闻任务数量为0");
            Trace.WriteLine("--TestGetNewsTask_1st完毕--");
        }

        public void TestWork_2nd()
        {
            //开始初始化任务
            Trace.WriteLine("--TestWork_2nd开始--");

            /* 功能说明:    任务开始前更新项目状态,和任务开始后更最后新抓取时间 
              需要准备参数: 一个CrawlTarget
            * */

            string sqlUpdateStatus = "update ProjectDetail set RunStatus = 1 where ProjectId = " + crawlTarget.ProjectId + " and siteid = " + crawlTarget.SiteId + " and ProjectType = " + ModelArgs.ProjectType;
            int updateStatusResult = HelperSQL.ExecNonQuery(sqlUpdateStatus);
            Trace.Assert(updateStatusResult == 1, "更新状态发生错误");

            string sqlUpdateCrawlTime = "Update ProjectDetail set CrawlFinishDate = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' Where ProjectId = " + crawlTarget.ProjectId + " and Siteid = " + crawlTarget.SiteId + " and ProjectType = " + ModelArgs.ProjectType;
            int updateCrawlTimeResult = HelperSQL.ExecNonQuery(sqlUpdateCrawlTime);
            Trace.Assert(updateCrawlTimeResult == 1, "更新抓取时间发生错误");
            Trace.WriteLine("--TestWork_2nd完毕--");


        }

        public void TestWork_3rd()
        {
            Trace.WriteLine("--TestWork_3rd开始--");

            /* 功能说明:     一个 Target 得到一个模板 
               需要准备参数: 一个CrawlTarget
            * */
            TemplateModel templateModel = new TemplateModel(crawlTarget.TemplateContent);

            Trace.Assert(templateModel != null, "模板解析错误");
            Trace.Assert(templateModel.Node.Length > 0, "模板 模块节点Node无内容");

            Trace.WriteLine("--TestWork_3rd完毕--");
        }

        #endregion

      
        public void TestWork_4th()
        {
            Trace.WriteLine("--TestWork_4th开始--");

            #region 获取页面源代码 ,并且写入文件
            /* 功能说明:     得到页面文本信息(源代码) 
                需要准备参数: 一个CrawlTarget,一个模板 TemplateModel
             * */

            string strKeyWordEncode = System.Web.HttpUtility.UrlEncode(crawlTarget.KeyWords, Encoding.GetEncoding(crawlTarget.SiteEncoding));
            crawlTarget.SiteUrl = crawlTarget.SiteUrl.Replace("<>", strKeyWordEncode);
            Trace.WriteLine("输出SiteUrl:\t" + crawlTarget.SiteUrl);
           // return;
            string strHTML = "";
            bool rev = _hh.Open(crawlTarget.SiteUrl, Encoding.GetEncoding(crawlTarget.SiteEncoding), ref strHTML);

            Trace.Assert(rev == true, @"获取网页源代码出错,出错原因,可能是 超时, 连接被意外关闭, 远程主机强迫, time out, 403, 500 等");

            //因为 有些关键的地方,被加密了,所以要解码
            if (crawlTarget.ClassName == "microbloggings" || crawlTarget.SiteUrl.Contains("http://beta.club.sohu.com"))
            {
                strHTML = originalWork.StrDecode(strHTML);
            }

            //将文件留在后面测试使用

            strHTML = Regex.Replace(strHTML, @"\r\n|\r|\n|\t", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
              
            File.WriteAllText("../../TsetFile/TestWorkforum_4th_html.txt", strHTML);
            //这个才是最终我们需要的页面信息
            Trace.WriteLine("输出 strHTML:\t" + strHTML);
            #endregion

            Trace.WriteLine("--TestWork_4th完毕--");
        }

        public void TestWork_5th()
        {

            /* 说明 : tid = 968 跟新浪微博一样需要转码*/

            Trace.WriteLine("--TestWork_5th开始--");

            /* 功能说明:     准备工作做好以后,开始匹配模板 
                需要准备参数: 一个CrawlTarget,一个模板 TemplateModel,以及页面源代码
             * */
            string strHTML = File.ReadAllText("../../TsetFile/TestWorkforum_4th_html.txt");

            int ipos = crawlTarget.SiteUrl.LastIndexOf('/');
            string strSiteEntry = ipos == 6 ? crawlTarget.SiteUrl + "/" : crawlTarget.SiteUrl.Substring(0, ipos) + "/";

            Trace.WriteLine("输出ipos\t:" + ipos);
            Trace.WriteLine("输出strSiteEntry\t:" + strSiteEntry);

            TemplateModel templateModel = new TemplateModel(crawlTarget.TemplateContent);
            Regex reg = new Regex(templateModel.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection matchCollection = reg.Matches(strHTML);

            Trace.Assert(matchCollection.Count > 0, "没有匹配上模块Node信息");
           

            int resultCount = 0;
            int i =0;
            //这里进行每一次的判断
            foreach (Match match in matchCollection)
            {

                //得到需要展示的数据 title content contyentdate url 最主要的就是这4个了   数据的准确性,及时性
                DataModel dataModel = new OriginalWork().GetDataModel(templateModel, match.Groups[0].Value, strSiteEntry, crawlTarget.SiteEncoding);
                if (dataModel == null)
                {
                    //resultCount++;
                    i++;
                    continue;
                }

                Trace.WriteLine("----------------------------第"+i+++"项-------------------------------------------------------------------------------------------------------------");

                Trace.WriteLine("输出dataModel.Title:\t" + dataModel.Title);

                Trace.WriteLine("输出dataModel.Content:\t" + dataModel.Content);
                Trace.Assert(!string.IsNullOrEmpty(dataModel.Content), "匹配错误,没有匹配到内容");

                Trace.WriteLine("输出dataModel.ContentDate:\t" + dataModel.ContentDate);
                Trace.WriteLine("输出dataModel.Url:\t" + dataModel.SiteUrl);


                if (crawlTarget.ClassName != "news")
                {
                    //新闻没有作者,也不需要
                    Trace.WriteLine("输出dataModel.Author:\t" + dataModel.Author);
                }
                if (crawlTarget.ClassName == "forum")
                {
                    Trace.WriteLine("输出dataModel.Reply:\t" + dataModel.Reply);
                    Trace.WriteLine("输出dataModel.PageView:\t" + dataModel.PageView);
                }
                Trace.WriteLine("\r\n");

                dataModel.ProjectID = crawlTarget.ProjectId;
                dataModel.SiteId = crawlTarget.SiteId;




                SqlParameter[] parms;
                switch (crawlTarget.ClassName.ToLower())
                {
                    case "forum":
                        parms = originalWork.GetForumParmsSQL(dataModel);
                        resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_Forum", parms);
                        break;
                    case "news":
                        parms = originalWork.GetNewsParmsSQL(dataModel);
                        resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_News", parms);
                        break;
                    case "blog":
                        parms = originalWork.GetBlogsParmsSQL(dataModel);
                        resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_Blog", parms);
                        break;
                    default:
                        parms = originalWork.GetMicroblogSQL(dataModel);
                        resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_MicroBlog", parms);
                        break;
                }


            }
            Trace.WriteLine("输出 InsertResultCount/MatchCollection.Count:\t" + resultCount + " / " + matchCollection.Count + "\r\n");
            Trace.Assert(resultCount >= matchCollection.Count-3, "匹配到的数据,和插入数据库的数据数量不一致");
           
            Trace.WriteLine("--TestWork_5th完毕--");
        }



         private static string path = @"H:\2014Year\服务器应用程序\模板识别工具\WinTemplate\WinTemplate\bin\Debug\Template\";

         public static int tid = 768;
        public static CrawlTarget crawlTarget = new CrawlTarget()
        {
            ClassName = "forum",
            TemplateContent = File.ReadAllText(path + tid+".xml"),
              KeyWords = "苹果", NextPageCount = 1, ProjectId = "1009",SiteId = "1",

            SiteEncoding = "utf-8",
            SiteUrl = "http://blog.sina.com.cn/geek08",
            RunStatus = 0
        };

      
        public  void TestOutPut()
        {
            Trace.WriteLine("--TestOutPut开始--");

             TestWork_4th();
             Thread.Sleep(1000);
             TestWork_5th();
             Trace.WriteLine("--TestOutPut完毕--");

        }
       
        public void Update()
        {
            CollectMethod col = new CollectMethod();
            col.TestUpdateTemplate(tid);
            Trace.WriteLine("--Update完毕--");
        }

     
      
    }
}

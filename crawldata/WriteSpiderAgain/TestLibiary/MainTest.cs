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
namespace ZTestLibiary
{
    public class MainTest
    {
       
        ServiceDALAction listDal = new ServiceDALAction();
        OriginalWork  originalWork = new OriginalWork();
        HttpHelper _hh = new HttpHelper();
        IList<CrawlTarget> crawlListRun;

        TemplateModel templateModel = new TemplateModel();

        /// <summary>
        /// 一个等待运行的任务
        /// </summary>
        public static CrawlTarget crawlTarget = new CrawlTarget()
        {
            ClassName = "microbloggings",
            TemplateContent = File.ReadAllText("../../TsetFile/microblogs_1097.xml"),
            //"<template><MainPage site=\"\"></MainPage><Node>&lt;dd class=\"content\"&gt;(.+?)&lt;a(.+?)class=\"date\"(.+?)&gt;</Node><Title></Title><Author>&lt;a nick-name=(.+?)&gt;</Author><Content>：&lt;em&gt;(.+?)&lt;/em&gt;</Content><CreateDate></CreateDate><ContentDate>&lt;/span&gt; &lt;a(.+?)class=\"date\"(.+?)&gt;</ContentDate><Views></Views><Replies></Replies><SrcUrl>&lt;a nick-name=(.+?)&gt;</SrcUrl><Url></Url><NextPage /></template>",
            SiteEncoding = "UTF-8",
            KeyWords = "视康+视康睛彩",
            NextPageCount = 1,
            ProjectId = "217",
            SiteId = "99463",
            SiteUrl = "http://s.weibo.com/weibo/<>",
            RunStatus = 0
        };


        public void TestMain_Sina()
        {
            Trace.WriteLine("--TestMain_Sina开始--");
            TestGetTask_1st();
            TestWork_2nd();
            TestWork_3rd();
            TestWork_4th();
            TestWork_5th();
            Trace.WriteLine("--TestMain_Sina结束--");
        }


        /// <summary>
        /// 测试从数据库 取一条新浪数据 进行测试
        /// </summary>
        public void TestGetTask_1st()
        {
            Trace.WriteLine("--TestGetTask_1st开始--");

           /* 功能说明:     测试从数据库获取 抓取任务 IList<CrawlTarget>  
              需要准备参数: 无
           * */

            crawlListRun = listDal.GetTargetList();
            int CrawlListCount = crawlListRun.Count;
            originalWork.GetWorkPage(crawlListRun[0]);

            Trace.Assert(CrawlListCount > 0, "TestGetSinaTask_1st 获取到新浪微博任务数量为0");
            Trace.WriteLine("--TestGetTask_1st完毕--");
        }

        public void TestWork_2nd()
        {
            //开始初始化任务
            Trace.WriteLine("--TestWork_2nd开始--");

            /* 功能说明:    任务开始前更新项目状态,和任务开始后更最后新抓取时间 
              需要准备参数: 一个CrawlTarget
            * */

            string sqlUpdateStatus = "update ProjectDetail set RunStatus = 1 where ProjectId = " + crawlTarget.ProjectId + " and siteid = " + crawlTarget.SiteId + " and ProjectType = " + ModelArgs.ProjectType;
            int updateStatusResult = HelperSQL.ExecuteSql(sqlUpdateStatus);
            Trace.Assert(updateStatusResult == 1, "更新状态发生错误");


            string sqlUpdateCrawlTime = "Update ProjectDetail set CrawlFinishDate = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' Where ProjectId = " + crawlTarget.ProjectId + " and Siteid = " + crawlTarget.SiteId + " and ProjectType = " + ModelArgs.ProjectType;
            int updateCrawlTimeResult = HelperSQL.ExecuteSql(sqlUpdateCrawlTime);
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
            Trace.Assert(templateModel.Node.Length>0,"模板 模块节点Node无内容");
                
            Trace.WriteLine("--TestWork_3rd完毕--");
        }

        public void TestWork_4th()
        {
            Trace.WriteLine("--TestWork_4th开始--");

            /* 功能说明:     得到页面文本信息(源代码) 
                需要准备参数: 一个CrawlTarget,一个模板 TemplateModel
             * */

            string strKeyWordEncode = System.Web.HttpUtility.UrlEncode(crawlTarget.KeyWords, Encoding.GetEncoding(crawlTarget.SiteEncoding));
            crawlTarget.SiteUrl = crawlTarget.SiteUrl.Replace("<>", strKeyWordEncode);
            Trace.WriteLine("输出SiteUrl:\t" + crawlTarget.SiteUrl);
             string strHTML = "";
             bool rev = _hh.Open(crawlTarget.SiteUrl, Encoding.GetEncoding(crawlTarget.SiteEncoding), ref strHTML);
            
            Trace.Assert(rev==true,@"获取新浪网页源代码出错,出错原因,可能是 超时, 连接被意外关闭, 远程主机强迫, time out, 403, 500 等");

            //Trace.WriteLine("输出 strHTML:\r\n\t" + strHTML);

           //因为 有些关键的地方,被加密了,所以要解码
            if (crawlTarget.ClassName == "microbloggings")
            {
                strHTML = originalWork.StrDecode(strHTML);
            }
           
            //将文件留在后面测试使用
            File.WriteAllText("../../TsetFile/TestWorkSina_4th_html.txt", strHTML);
            //这个才是最终我们需要的页面信息
            Trace.WriteLine("输出 strHTML:\t" + strHTML);

            Trace.WriteLine("--TestWork_4th完毕--");
        }

        public void TestWork_5th()
        {
            Trace.WriteLine("--TestWork_5th开始--");

            /* 功能说明:     准备工作做好以后,开始匹配模板 
                需要准备参数: 一个CrawlTarget,一个模板 TemplateModel,以及页面源代码
             * */
            string strHTML = File.ReadAllText("../../TsetFile/TestWorkSina_4th_html.txt");

            
           // 得到最后Url所在的Url结构 判断是相对路径还是绝对路径 http://s.weibo.com/weibo/
            int ipos = crawlTarget.SiteUrl.LastIndexOf('/');
            string strSiteEntry = ipos == 6 ? crawlTarget.SiteUrl + "/" : crawlTarget.SiteUrl.Substring(0, ipos) + "/";

            Trace.WriteLine("输出ipos\t:" + ipos);
            Trace.WriteLine("输出strSiteEntry\t:" + strSiteEntry);
          

            TemplateModel templateModel = new TemplateModel(crawlTarget.TemplateContent);
            Regex reg = new Regex(templateModel.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection matchCollection = reg.Matches(strHTML);
          
            Trace.Assert(matchCollection.Count > 0, "没有匹配上模块Node信息");
            Trace.WriteLine("输出 MatchCollection.Count:\t" + matchCollection.Count);

            int resultCount = 0;
            //这里进行每一次的判断
            foreach (Match match in matchCollection)
            {
                //得到 微博的东西
                DataModel dataModel = new OriginalWork().GetDataModel(templateModel, match.Groups[0].Value, strSiteEntry,"utf-8");
                    //sinaWork.GetMicrobloggings(templateModel, match.Groups[0].Value);

                //Trace.WriteLine("输出dataModel.Content:\t" + dataModel.Content);
                Trace.Assert(!string.IsNullOrEmpty(dataModel.Content), "匹配错误,没有匹配到内容");

                Trace.WriteLine("输出dataModel.Author:\t" + dataModel.Author);
                Trace.WriteLine("输出dataModel.Content:\t" + dataModel.Content);
                Trace.WriteLine("输出dataModel.ContentDate:\t" + dataModel.ContentDate);
                Trace.WriteLine("输出dataModel.Url:\t" + dataModel.Url + "\n\n");

                dataModel.ProjectID = crawlTarget.ProjectId;
                dataModel.SiteId = crawlTarget.SiteId;

                //得到 参数列表 sql语句
                SqlParameter[] parms = originalWork.GetMicroblogSQL(dataModel);
                //插入到数据库
                resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_MicroBlog", parms);
            }
            Trace.Assert(resultCount == matchCollection.Count,"匹配到的数据,和插入数据库的数据数量不一致");
            Trace.WriteLine("--TestWork_5th完毕--");
        }

    }
}


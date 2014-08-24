using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System.Threading;


namespace DataCrawler.BLL.Crawler
{
    public class SitsDataRetrieveBLL 
    {
       
    
        protected void RetrieveData()
        {  //ServerDaLAction sqlDalAction = new ServerDaLAction();

            DataRetrieveDalAction sqlDalAction = new DataRetrieveDalAction();
            //项目,关键字,匹配规则, 数据来源
            //得到所有 在运行的项目列表
            List<ProjectDetailModel> list = sqlDalAction.GetMatchTargetService();

            SiteData_RunRecord ss_rr = sqlDalAction.Select_RunRecord_MinUse();

            if (ss_rr.min_runId >= ss_rr.max_runId - 3)
            {
                Console.WriteLine("这里该停止了!");

                Thread.Sleep(60000);//停60秒
                RetrieveData();
            }


            for (int i = 0; i < list.Count; i++)
            {
                //针对于某一个项目
                ProjectDetailModel project = list[i];
                if (project.ProjectId < 1000)
                {
                    continue;
                }
                string str_Or = "";
                string str_And = "";
                string str_Not = "";
                //得到了where 条件
                try
                {
                    Getresult(project.MatchingRule, project.MatchingRuleType, ref str_Or, ref str_And, ref str_Not);
                    //检索 Forum表里面的所有数据  
                    /*#xiang 这里应该检索出一个范围,而不是全部*/
                    if (project.ClassId == 1)
                    {
                        //论坛
                        sqlDalAction.Insert_SiteData_ForumService(project.ProjectId, str_Or, str_And, str_Not, ss_rr.min_forumId, ss_rr.min_forumId_next);
                    }
                    else if (project.ClassId == 2)
                    {
                        //新闻
                        if (ss_rr.min_newsId < ss_rr.min_newsId_next)
                        {
                            sqlDalAction.Insert_SiteData_NewsService(project.ProjectId, str_Or, str_And, str_Not, ss_rr.min_newsId, ss_rr.min_newsId_next);
                        }
                    }
                    else if (project.ClassId == 3)
                    {
                        //新闻
                        if (ss_rr.min_blogId < ss_rr.min_blogId_next)
                        {
                            sqlDalAction.Insert_SiteData_BlogService(project.ProjectId, str_Or, str_And, str_Not, ss_rr.min_blogId, ss_rr.min_blogId_next);
                        }
                    }
                    else if (project.ClassId == 5)
                    {
                        //新浪微博
                        //Console.WriteLine(project.ProjectId);

                        if (ss_rr.min_microblogId < ss_rr.min_microblogId_next)
                        {
                            //两个相等,暂时不需要更新
                            Console.WriteLine(ss_rr.min_microblogId);
                            Console.WriteLine(ss_rr.min_microblogId_next);
                            sqlDalAction.Insert_SiteData_MicroBlogService(project.ProjectId, str_Or, str_And, str_Not, ss_rr.min_microblogId, ss_rr.min_microblogId_next);
                        }

                    }
                    //更新完 论坛,新闻,博客,微博



                    //然后继续下一个循环,重复调用
                }
                catch 
                {
                   // base.Log(LogLevel.Error, ex.Message);
                    //log.Error("RetrieveData -for 异常", ex);
                }
            }
            //更新掉,新闻论坛,博客,微博里面没用的数据
            sqlDalAction.Update_SteData_RunRecord_MinNotUse();


            Console.WriteLine("测试完毕");
            RunCount++;
            Thread.Sleep(10000);
            if (RunCount < 100)
            {
                RetrieveData();
            }
        }
        int RunCount = 0;

        string matchRule_test = "A+B+C-D-E-F&M&S"; //顺序 +-&

        protected void TestRule()
        {
            matchRule_test = "小米+手机";

            string str_Or = "";
            string str_And = "";
            string str_Not = "";
            Getresult(matchRule_test, 1, ref str_Or, ref str_And, ref str_Not);
            Console.WriteLine(str_Or);
            Console.WriteLine(str_And);
            Console.WriteLine(str_Not);
        }

        protected void Getresult(string matchRule, int matchType, ref string str_Or, ref string str_And, ref string str_Not)
        {
            string[] strOrArr = { };//+
            string[] strNotArr = { };//-
            string[] strAndArr = { };//&

            strOrArr = matchRule.Split('+');
            for (int i = 0; i < strOrArr.Length; i++)
            {
                if (i == strOrArr.Length - 1 && strOrArr[i].Contains("-"))
                {
                    strNotArr = strOrArr[i].Substring(strOrArr[i].IndexOf('-') + 1).Split('-');
                    strOrArr[i] = strOrArr[i].Substring(0, strOrArr[i].IndexOf('-'));
                }
            }

            for (int i = 0; i < strNotArr.Length; i++)
            {
                if (i == strNotArr.Length - 1 && strNotArr[i].Contains("&"))
                {
                    strAndArr = strNotArr[i].Substring(strNotArr[i].IndexOf('&') + 1).Split('&');
                    strNotArr[i] = strNotArr[i].Substring(0, strNotArr[i].IndexOf('&'));
                }
            }
            str_Or = GetMatchOr_Not(strOrArr, matchType);
            str_And = GetMatchAnd(strAndArr, matchType);
            str_Not = GetMatchOr_Not(strNotArr, matchType);
        }

        protected string GetMatchOr_Not(string[] array, int type)
        {
            string sqlWhere = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (type == 0)
                {
                    if (i == 0)
                    {
                        sqlWhere += " Title like '%" + array[i] + "%' or Content like '%" + array[i] + "%'";
                    }
                    else
                    {
                        sqlWhere += " or Title like '%" + array[i] + "%' or Content like '%" + array[i] + "%'";
                    }
                }
                else if (type == 1)
                {
                    if (i == 0)
                    {
                        sqlWhere += " Title like '%" + array[i] + "%'";
                    }
                    else
                    {
                        sqlWhere += " or Title like '%" + array[i] + "%'";
                    }
                }
                else if (type == 2)
                {
                    if (i == 0)
                    {
                        sqlWhere += " Content like '%" + array[i] + "%'";
                    }
                    else
                    {
                        sqlWhere += " or Content like '%" + array[i] + "%'";
                    }
                }
            }
            return sqlWhere;
        }

        protected string GetMatchAnd(string[] array, int type)
        {
            string sqlWhere = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (type == 0)
                {
                    sqlWhere += "and Title like '%" + array[i] + "%' and Content like '%" + array[i] + "%'";
                }
                else if (type == 1)
                {
                    sqlWhere += " and Title like '%" + array[i] + "%'";
                }
                else if (type == 2)
                {
                    sqlWhere += " and Content like '%" + array[i] + "%'";
                }
            }
            return sqlWhere;
        }


        //更新数据的使用状态
        protected void UpdateFromDataUseStatus()
        {
            //更新一个批次,执行之后间隔 5秒钟 执行下一次
            // usp_mining_Update_UseStatusByRunId

        }


    }
}

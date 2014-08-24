using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using RionSoft.IBRS.Business.DAL;

using System.Data.SqlClient;
using DataCrawler.Model.Crawler;
using LogNet;

namespace DataCrawler.DAL.Crawler
{
    public class DataRetrieveDalAction
    {
        public DataRetrieveDalAction() { }

        public override string ToString()
        {
            try
            {
                return base.ToString();
            }
            catch
            {
                return "";
            }
        }


        public List<ProjectDetailModel> GetMatchTargetService()
        {
           
            List<ProjectDetailModel> list = new List<ProjectDetailModel>();
            //得到还在运行的数据

            try
            {

            IBRSCommonDAL dal = new IBRSCommonDAL();
            LogBLL.Log("usp_mining_select_project_run");
            DataTable dt = dal.SelectData("usp_mining_select_project_run", null);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ProjectDetailModel p = new ProjectDetailModel();
                    p.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    p.MatchingRuleType = Convert.ToInt32(row["MatchingRuleType"]);
                    p.MatchingRule = row["MatchingRule"].ToString();
                    p.ClassId = Convert.ToInt32(row["ClassId"]);
                    list.Add(p);
                }
            }

            }
            catch (Exception ex)
            {
 LogBLL.Error("InsertCategoryInfoService", ex);
               
            }
            return list;
        }
        //select cid from forum where 1=1 and 
        public int Insert(string whereStr)
        {
          
            string sql = "select Cid,Title,Content,ContentDate,SrcUrl,SiteId,ProjectId,CreateDate from forum where 1=1 and UseStatus is null and " + whereStr;

            IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log(sql);
            DataTable dt = dal.SelectData(sql);
            //HelperSQL.GetTable(sql);

            if (dt != null)
            {
                List<Forum> listCid = new List<Forum>();

                foreach (DataRow row in dt.Rows)
                {
                    Forum f = new Forum();

                    f.Cid = Convert.ToInt32(row["Cid"]);
                    f.Title = row["Title"].ToString();
                    f.Content = row["Content"].ToString();
                    f.ContentDate = Convert.ToDateTime(row["ContentDate"]);
                    f.SrcUrl = row["SrcUrl"].ToString();
                    f.SiteId = Convert.ToInt32(row["SiteId"]);
                    f.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    f.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    listCid.Add(f);
                }

                foreach (Forum item in listCid)
                {

                    //insert


                    //Update

                }


                //查出来一条数据
                //插入SiteData

            }

            return 0;

        }

        public int Insert_SiteData_ForumService(int projectId, string whereStr_or, string whereStr_and, string whereStr_not, int min_forumId, int min_forumId_next)
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            // Forum ClassId = 1
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
            new SqlParameter("projectId",projectId),
             new SqlParameter("whereStr_or",whereStr_or),
              new SqlParameter("whereStr_and",whereStr_and),
               new SqlParameter("whereStr_not",whereStr_not),
                new SqlParameter("min_forumId",min_forumId),
                 new SqlParameter("min_forumId_next",min_forumId_next)
               
            };
                LogBLL.Log("usp_mining_insert_SiteData_Forum", parms);
                result = dal.ExecuteNonQuery("usp_mining_insert_SiteData_Forum", parms);
                
            }
            catch (Exception ex)
            {
                LogBLL.Error("Insert_SiteData_ForumService", ex);
             
            }
            return result;
        }

        public int Insert_SiteData_NewsService(int projectId, string whereStr_or, string whereStr_and, string whereStr_not, int min_newsId, int min_newsId_next)
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            //News ClassId = 2
            int result = 0;
            try
            {
                object[] obj = new object[] { projectId, whereStr_or, whereStr_and, whereStr_not, min_newsId, min_newsId_next };
                SqlParameter[] parms = new SqlParameter[] {
            new SqlParameter("projectId", projectId),
                 new SqlParameter("whereStr_or",whereStr_or ),
                      new SqlParameter("whereStr_and",whereStr_and ),
                           new SqlParameter("whereStr_not", whereStr_not),
                                new SqlParameter("min_newsId", min_newsId),
                                 new SqlParameter("min_newsId_next", min_newsId_next)
            };
                LogBLL.Log("usp_mining_insert_SiteData_News", parms);
                result = dal.ExecuteNonQuery("usp_mining_insert_SiteData_News", parms);
              
            }
            catch (Exception ex)
            {
                LogBLL.Error("Insert_SiteData_NewsService", ex);
              
            }
            return result;
        }
        public int Insert_SiteData_BlogService(int projectId, string whereStr_or, string whereStr_and, string whereStr_not, int min_blogId, int min_blogId_next)
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            //Blog ClassId = 3
            int result = 0;
            try
            {
                ;
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId",projectId ),
                 new SqlParameter("whereStr_or", whereStr_or),
                  new SqlParameter("whereStr_and", whereStr_and),
                   new SqlParameter("whereStr_not", whereStr_not),
                    new SqlParameter("min_blogId", min_blogId),
                    new SqlParameter("min_blogId_next", min_blogId_next)
                    
            };
                LogBLL.Log("usp_mining_insert_SiteData_Blog", parms);
                result = dal.ExecuteNonQuery("usp_mining_insert_SiteData_Blog", parms);
              
            }
            catch (Exception ex)
            {

                LogBLL.Error("Insert_SiteData_BlogService ", ex);
            }
            return result;
        }


        public int Insert_SiteData_MicroBlogService(int projectId, string whereStr_or, string whereStr_and, string whereStr_not, int min_microblogId, int min_microblogId_next)
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            //MicroBlog ClassId = 5
            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId",projectId ),
                 new SqlParameter("whereStr_or", whereStr_or),
                  new SqlParameter("whereStr_and", whereStr_and),
                   new SqlParameter("whereStr_not", whereStr_not),
                    new SqlParameter("min_blogId", min_microblogId),
                    new SqlParameter("min_blogId_next", min_microblogId_next)
                    
            };
                LogBLL.Log("usp_mining_insert_SiteData_MicroBlog", parms);
                result = dal.ExecuteNonQuery("usp_mining_insert_SiteData_MicroBlog", parms);
              
            }
            catch (Exception ex)
            {

                 LogBLL.Error("Insert_SiteData_MicroBlogService ", ex);
            }
            return result;
        }

        /// <summary>
        /// 最小的 使用的 抓取记录
        /// </summary>
        /// <returns></returns>
        public SiteData_RunRecord Select_RunRecord_MinUse()
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            SiteData_RunRecord sd_rr = new SiteData_RunRecord();

            DataTable dt = new DataTable();

            try
            {

                LogBLL.Log("usp_mining_Select_RunRecord_MinUse");
                dt = dal.SelectData("usp_mining_Select_RunRecord_MinUse", null);
                //HelperSQL.GetTable("usp_mining_Select_RunRecord_MinUse", null, CommandType.StoredProcedure);

                if (dt != null)
                {
                    sd_rr.min_runId = Convert.ToInt32(dt.Rows[0]["min_runId"]);
                    sd_rr.min_runId_next = Convert.ToInt32(dt.Rows[0]["min_runId_next"]);
                    sd_rr.max_runId = Convert.ToInt32(dt.Rows[0]["max_runId"]);

                    sd_rr.min_forumId = Convert.ToInt32(dt.Rows[0]["min_forumId"]);
                    sd_rr.min_forumId_next = Convert.ToInt32(dt.Rows[0]["min_forumId_next"]);

                    sd_rr.min_newsId = Convert.ToInt32(dt.Rows[0]["min_newsId"]);
                    sd_rr.min_newsId_next = Convert.ToInt32(dt.Rows[0]["min_newsId_next"]);

                    sd_rr.min_blogId = Convert.ToInt32(dt.Rows[0]["min_blogId"]);
                    sd_rr.min_blogId_next = Convert.ToInt32(dt.Rows[0]["min_blogId_next"]);

                    sd_rr.min_microblogId = Convert.ToInt32(dt.Rows[0]["min_microblogId"]);
                    sd_rr.min_microblogId_next = Convert.ToInt32(dt.Rows[0]["min_microblogId_next"]);
                }

            }
            catch (Exception ex)
            {


                LogBLL.Error("Select_RunRecord_MinUse 异常", ex);

            }
            return sd_rr;

        }


        public int Update_SteData_RunRecord_MinNotUse()
        {
            IBRSCommonDAL dal = new IBRSCommonDAL();
            int result = 0;
            try
            {
                LogBLL.Log("usp_mining_Update_UseStatusByRunId");
                result = dal.ExecuteNonQuery("usp_mining_Update_UseStatusByRunId");
                //HelperSQL.ExecNonQuery("usp_mining_Update_UseStatusByRunId", null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                LogBLL.Error("Update_SteData_RunRecord_MinNotUse ", ex);
            }
            return result;
        }


    }
}

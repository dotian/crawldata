using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;
using System.Data.SqlClient;
using System.Data;

namespace ConGetData.DAL
{
    public class ServiceDAL : IServiceDAL
    {
        public List<CrawlTarget> GetTargetService()
        {
            List<CrawlTarget> list = new List<CrawlTarget>();
            SqlDataReader reader = null;

            try
            {
                SqlParameter[] parms = new SqlParameter[] { 
                   new SqlParameter("projecttype",ModelArgs.ProjectType)
                };

                reader = HelperSQL.ExecuteReader("usp_Spider_Select_TaskTarget", parms, CommandType.StoredProcedure);
                while (reader.Read())
                {
                    CrawlTarget crawlTarget = new CrawlTarget();
                    crawlTarget.SiteUrl = reader["SiteUrl"].ToString();
                    crawlTarget.SiteEncoding = reader["siteEncoding"].ToString();
                    crawlTarget.TemplateContent = reader["TemplateContent"].ToString();                    
                    crawlTarget.RunStatus = Convert.ToInt32(reader["runstatus"]);
                    crawlTarget.ProjectId = reader["ProjectId"].ToString();
                    crawlTarget.SiteId =reader["SiteId"].ToString();
                    crawlTarget.ClassName = reader["ClassName"].ToString();
                    crawlTarget.KeyWords = reader["KeyWords"].ToString();
                    crawlTarget.Tid = reader["Tid"].ToString();
                    crawlTarget.PostContent = reader["PostContent"].ToString();
                    list.Add(crawlTarget);
                }
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("GetTargetService",ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return list;

        }

        public List<CK_SiteList> GetCK_listService()
        {

            string sql = "select SiteId,SiteUrl from SiteList_ck where Status is null";
            List<CK_SiteList> list = new List<CK_SiteList>();
            SqlDataReader reader = null;

            try
            {
               
                reader = HelperSQL.ExecuteReader(sql, null, CommandType.Text);
                while (reader.Read())
                {
                    CK_SiteList ck = new CK_SiteList();
                    ck.SiteId = Convert.ToInt32(reader["SiteId"]);
                    ck.SiteUrl = reader["SiteUrl"].ToString();
                    list.Add(ck);
                }
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("GetCK_listService", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return list;

        }

        public int UpdateCKStatusService(int siteId,int status)
        {
            int result = 0;
            string sql = "update SiteList_ck set Status = '" + status + "' where SiteId = '" + siteId + "'";
            try
            {
               result = HelperSQL.ExecNonQuery(sql,null, CommandType.Text);
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("UpdateCKStatusService", ex);
            }

            return result;
        }
    }
}

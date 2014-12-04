using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WriteSpiderAgain.EntityModel;
using System.Diagnostics;
namespace WriteSpiderAgain.ServiceDAL
{
    /// <summary>
    /// 数据访问 实体数据处理
    /// </summary>
    public class ServiceDALAction:Page
    {
        public IList<CrawlTarget> GetTargetList()
        {
            IList<CrawlTarget> list = new List<CrawlTarget>();
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
                    crawlTarget.SiteUrl = Field.GetString(reader, "SiteUrl");
                    crawlTarget.SiteEncoding = Field.GetString(reader, "SiteEncoding");
                    crawlTarget.TemplateContent = Field.GetString(reader, "TemplateContent");
                    //runstatus 等于1 才可以运行,99表示删除 0表示默认
                    crawlTarget.RunStatus = Field.GetInt32(reader, "runstatus");
                    crawlTarget.ProjectId = Field.GetInt32(reader, "ProjectId").ToString();
                    crawlTarget.SiteId = Field.GetInt32(reader, "SiteId").ToString();
                    crawlTarget.ClassName = Field.GetString(reader, "ClassName");
                    crawlTarget.NextPageCount = Field.GetInt32(reader, "CrawlPageCount");
                    crawlTarget.KeyWords = Field.GetString(reader, "KeyWords");
                    crawlTarget.Tid = Field.GetInt32(reader, "Tid");
                    list.Add(crawlTarget);
                }
            }
            catch (Exception ex)
            {
                log.Error("GetTargetList 异常",ex);
            }
            finally
            {
                if (reader!=null)
                {
                    reader.Close();
                }
            }
            return list;
        }

        public void InsertErrorList(string strSiteID, string strSite, string error_messgae)
        {
            try
            {

                SqlParameter[] parms = new SqlParameter[] { 
                 new SqlParameter("siteid",strSiteID),
                  new SqlParameter("siteurl",strSite),
                    new SqlParameter("errormessage",error_messgae),
                    new SqlParameter("crawlfinishdate",DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"))
                };

                HelperSQL.ExecNonQuery("usp_Spider_Insert_errorsite", parms);
            }
            catch(Exception ex)
            {
                log.Error("InsertErrorList 异常", ex);
            }
        }

        /// <summary>
        /// 插入运行的记录
        /// </summary>
       public void InsertRunRecord()
       {
            SqlParameter[] parms = new SqlParameter[] { 
                   new SqlParameter("projecttypeId",ModelArgs.ProjectType),
                   new SqlParameter("createdate",DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"))
                };
            HelperSQL.ExecNonQuery("usp_Spider_Insert_RunRecord", parms, System.Data.CommandType.StoredProcedure);
       }

        /// <summary>
       /// 删除重复数据  4张表
        /// </summary>
       public void DeleteRepeat()
       {
           //已使用触发器
           //HelperSQL.ExecNonQuery("usp_Spider_DeleteRepeat", null, System.Data.CommandType.StoredProcedure);
           
       }
    }
}

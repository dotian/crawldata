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
    /// <summary>
    /// 竞争社管理
    /// </summary>
    public class ContendDAL
    {
        public ContendDAL() { }

        /// <summary>
        /// 插入一条数据,返回插入的Id
        /// </summary>
        /// <param name="catename"></param>
        /// <param name="empname"></param>
        /// <param name="classid"></param>
        /// <returns></returns>
        public List<ProjectList> GetProjectInfoService(string projectname)
        {
            List<ProjectList> list = new List<ProjectList>();
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("projectname",projectname)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_contendProjectByName", parms);
                DataTable dt = dal.SelectData("usp_mining_select_contendProjectByName", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {
                    ProjectList projectList = new ProjectList();
                    projectList.ProjectId = int.Parse(row["ProjectId"].ToString());
                    projectList.ProjectName = row["ProjectName"].ToString();
                    projectList.RssKey = row["RssKey"]==DBNull.Value?"": row["RssKey"].ToString();
                    projectList.PList_RunStatus = int.Parse(row["RunStatus"].ToString());
                    list.Add(projectList);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("InsertCategoryInfoService", ex);
            }
            return list;
        }


        public int InsertContendByProjectService(int projectId, int contendId,string empId)
        {
            int result = 0;

            try 
	        {	        
                //@projectId,@contendId,@contendName,@contendKey
                SqlParameter[]parms = new SqlParameter[]{
                   new SqlParameter("projectId",projectId),
                   new SqlParameter("contendId",contendId),
                   new SqlParameter("empId",empId)
                };

		         IBRSCommonDAL dal = new IBRSCommonDAL();
                 LogBLL.Log("usp_mining_insert_ContendInfo",parms);
                 result = dal.ExecuteNonQuery("usp_mining_insert_ContendInfo", parms);
	        }
	        catch (Exception ex)
	        {
                LogBLL.Error("InsertContendByProjectService",ex);
	        }

            return result;
        }
    }
}

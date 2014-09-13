using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using DataCrawler.Model.Hankook;
using RionSoft.IBRS.Business.DAL;
using LogNet;
namespace DataCrawler.DAL.Hankook
{
    public class CostomerDAL
    {
        /// <summary>
        /// 数据 来源 是 SiteData , 论坛,博客微博
        /// </summary>
        public List<ShowDataInfo> GetCustomerSiteDataService(QueryHankookArgs queryArgs)
        {
            //标题,tag 媒体,时间,序号
            List<ShowDataInfo> list = new List<ShowDataInfo>();

            object[] obj = new object[] { 
                queryArgs.start, queryArgs.end,queryArgs.datatype, queryArgs.pageIndex,
                queryArgs.pagesize, queryArgs.file1, queryArgs.file2, queryArgs.file3,queryArgs.sitetype };
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectid",queryArgs.ProjectId),
                    new SqlParameter("start",queryArgs.start),
                    new SqlParameter("end",queryArgs.end),
                    new SqlParameter("datatype",queryArgs.datatype),
                    new SqlParameter("pageIndex",queryArgs.pageIndex),
                    new SqlParameter("pagesize",queryArgs.pagesize),
                    new SqlParameter("file1",queryArgs.file1),
                    new SqlParameter("file2",queryArgs.file2),
                    new SqlParameter("file3",queryArgs.file3),
                    new SqlParameter("sitetype",queryArgs.sitetype),
                    new SqlParameter("analysis",queryArgs.analysis),
                    new SqlParameter("attention",queryArgs.attention),
                    new SqlParameter("showstatus",queryArgs.showstatus)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_minmig_select_customer_sitedata", parms);

                dt = dal.SelectData("usp_minmig_select_customer_sitedata", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                string dataName = GetDataName(queryArgs.sitetype);
                foreach (DataRow row in dt.Rows)
                {
                   
                    ShowDataInfo showdataInfo = new ShowDataInfo();
                    showdataInfo.Id = Convert.ToInt32(row["Id"]);
                    showdataInfo.Title = row["Title"].ToString();
                    showdataInfo.Content = row["Content"].ToString();
                    showdataInfo.Time = Convert.ToDateTime(row["ContentDate"]).ToString("yyyy-MM-dd");
                    showdataInfo.SrcUrl = row["SrcUrl"].ToString();
                    showdataInfo.SiteName = row["SiteName"].ToString();
                    showdataInfo.Analysis = GetAnalysisColor(Convert.ToInt32(row["Analysis"]));

                 
                    List<string> lststr = new List<string>() { row["Tag1"].ToString(), row["Tag2"].ToString(), row["Tag3"].ToString() };
                    lststr.RemoveAll(c=>c=="");
                    showdataInfo.Tag = string.Join(";",lststr.ToArray());
                    showdataInfo.DataName = dataName;

                    showdataInfo.Suggest = row["Smessage"] == DBNull.Value ? "" : row["Smessage"].ToString();
                    list.Add(showdataInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetCustomerSiteDataService", ex);
            }
            return list;
        }

        public int GetCustomerSiteDataCountService(QueryHankookArgs queryArgs)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectid",queryArgs.ProjectId),
                    new SqlParameter("start",queryArgs.start),
                    new SqlParameter("end",queryArgs.end),
                    new SqlParameter("datatype",queryArgs.datatype),
                    new SqlParameter("file1",queryArgs.file1),
                    new SqlParameter("file2",queryArgs.file2),
                    new SqlParameter("file3",queryArgs.file3),
                    new SqlParameter("sitetype",queryArgs.sitetype),
                    new SqlParameter("analysis",queryArgs.analysis),
                    new SqlParameter("attention",queryArgs.attention),
                    new SqlParameter("showstatus",queryArgs.showstatus)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_minmig_select_customer_sitedataCount", parms);
                object objResult = dal.ExecuteScalar("usp_minmig_select_customer_sitedataCount", parms);
                result = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetCustomerSiteDataCountService", ex);
            }
            return result;
        }

       
      

        public List<ProjectInfo> GetProjectListService(string userName)
        {

            List<ProjectInfo> list = new List<ProjectInfo>();
            try
            {
                DataTable dt = new DataTable();

                SqlParameter[] parms = new SqlParameter[] { 
                 new SqlParameter("username",userName)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();

                dt = dal.SelectData("usp_minmig_select_customer_project", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
             
                foreach (DataRow row in dt.Rows)
                {
                    ProjectInfo project = new ProjectInfo();
                    project.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    project.ProjectName = row["ProjectName"].ToString();
                    project.MatchingRule = row["MatchingRule"].ToString();
                    project.EmpId = row["EmpId"].ToString();
                    project.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                    project.EndDate = DateTime.Parse(row["EndDate"].ToString());
                    project.RunStatus = int.Parse(row["RunStatus"].ToString());
                    list.Add(project);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetCustomerNewsService", ex);
            }
            return list;

        }

        public int UpdateSiteDataMessByIdService(int id,string mess)
        {
            int result = 0;
            object[] obj = new object[] { id, mess };

            try
            {
                SqlParameter[]parms = new SqlParameter[]{
                     new SqlParameter("id",id),
                     new SqlParameter("mess",mess)
                
                };
                   IBRSCommonDAL dal = new IBRSCommonDAL();
                   LogBLL.Log("usp_minmig_update_customer_sitedata_mess", parms);
                   result = dal.ExecuteNonQuery("usp_minmig_update_customer_sitedata_mess", parms);
            }
            catch (Exception ex)
            {

                LogBLL.Error("UpdateSiteDataMessByIdService", ex);
            }
            return result;
        }

        public int UpdateHankookMessByIdService(int id, string mess)
        {
            int result = 0;
            object[] obj = new object[] { id, mess };

            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                     new SqlParameter("id",id),
                     new SqlParameter("mess",mess)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_minmig_update_customer_hankook_mess", parms);
                result = dal.ExecuteNonQuery("usp_minmig_update_customer_hankook_mess", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateHankookMessByIdService", ex);
            }
            return result;
        }


        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerInfo GetCustomerByCustomerIdService(string customerId)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            try
            {
                DataTable dt = new DataTable();

                SqlParameter[] parms = new SqlParameter[] { 
                   new SqlParameter("customerId",customerId)
                };


                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_minmig_select_customer_ByCustomerId", parms);
                dt = dal.SelectData("usp_minmig_select_customer_ByCustomerId", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return customerInfo;
                }
                customerInfo.CustomerId = dt.Rows[0]["CustomerId"].ToString();
                customerInfo.LoginPwd = dt.Rows[0]["LoginPwd"].ToString();
                customerInfo.SecretKey = dt.Rows[0]["SecretKey"].ToString();
                customerInfo.EmpName = dt.Rows[0]["EmpName"].ToString();
                customerInfo.UserPermissions = int.Parse(dt.Rows[0]["UserPermissions"].ToString());
                customerInfo.ProjectId = int.Parse(dt.Rows[0]["ProjectId"].ToString());
                customerInfo.RunStatus = int.Parse(dt.Rows[0]["RunStatus"].ToString());
                


                return customerInfo;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetCustomerByCustomerIdService", ex);
            }
            return customerInfo;



        }

        public bool GetExistContentIdService(int projectId,int contendId)
        {
            bool b_exists = false;
            try
            {
                SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("projectId",projectId),
                    new SqlParameter("contendId",contendId)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_customer_existcontendId", parms);
                object objResult = dal.ExecuteScalar("usp_mining_select_customer_existcontendId", parms);
                b_exists = Convert.ToInt32(objResult)>0;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetExistContentIdService", ex);
            }
            return b_exists;
        }


        public List<ContendTB> GetContendTbListByProjectIdService(int projectId)
        {

            List<ContendTB> list = new List<ContendTB>();

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parms = new SqlParameter[] { 
                   new SqlParameter("projectId",projectId)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_customer_contendTopFour", parms);
                dt = dal.SelectData("usp_mining_select_customer_contendTopFour", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }

                foreach (DataRow row in dt.Rows)
                {
                    ContendTB contend = new ContendTB();
                    contend.ContendId = Convert.ToInt32(row["ContendId"]);
                    contend.ContendName = row["ContendName"].ToString();
                    list.Add(contend);
                }
            }
            catch (Exception ex)
            {

                LogBLL.Error("GetContendTbListByProjectIdService", ex);
            }

            return list;
        }

        #region Func 辅助方法
        public Func<int, string> GetDataName = (int sitetype) =>
            {
                if (sitetype==1)
                {
                    return "网络论坛";
                }
                else if (sitetype == 3)
                {
                    return "网络博客";
                }
                else if (sitetype==5)
                {
                    return "网络微博";
                }
                return "网络";
            };

        public Func<int, string> GetAnalysisColor = (int analysis) =>
        {
            if (analysis==1)
            {
                return "blue";
            }
            else if (analysis == 3)
            {
                return "red";
            }
            else
            {
                return "green";
            }
        };

      #endregion
    }
}

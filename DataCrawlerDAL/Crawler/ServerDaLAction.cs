using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RionSoft.IBRS.Business.DAL;
using DataCrawler.Model.Crawler;
using LogNet;
namespace DataCrawler.DAL.Crawler
{
    public class ServerDaLAction
    {
        public ServerDaLAction() { }

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

        #region
        /// <summary>
        /// 查询项目的详细信息,以及 包含 各类型的数据
        /// </summary>
        /// <returns></returns>
        public List<ProjectDetailModel> ProjectListDetailService()
        {
            List<ProjectDetailModel> list = new List<ProjectDetailModel>();
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_projectlistdetail");
                DataTable dt = dal.SelectData("usp_mining_select_projectlistdetail", null);
                foreach (DataRow row in dt.Rows)
                {
                    ProjectDetailModel plistModel = new ProjectDetailModel();
                    plistModel.ProjectId = Convert.ToInt32(row["ProjectName"]);
                    plistModel.ProjectName = row["ProjectName"].ToString();
                    plistModel.MatchingTypeName = row["MatchingTypeName"].ToString();
                    plistModel.MatchingRule = row["MatchingRule"].ToString();
                    plistModel.EmpId = row["EmpName"].ToString();
                    plistModel.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    plistModel.EndDate = Convert.ToDateTime(row["EndDate"]);
                    plistModel.ForumNum = Convert.ToInt32(row["ForumNum"]);
                    plistModel.NewsNum = Convert.ToInt32(row["NewsNum"]);
                    plistModel.BlogNum = Convert.ToInt32(row["BlogNum"]);
                    plistModel.MicroBlogNum = Convert.ToInt32(row["MicroBlogNum"]);
                    list.Add(plistModel);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("ProjectListDetailService 获取数据异常", ex);
            }
            return list;
        }


        public List<Forum> FoumPagerService(int pageSize, int pageIndex, object[] objParms)
        {

            List<Forum> list = new List<Forum>();
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] parms = null;
                if (objParms == null)
                {
                    parms = new SqlParameter[] { 
                        new SqlParameter("pagesize",pageSize ),
                         new SqlParameter("pageindex",pageIndex ), new SqlParameter("parms_date1","" ),

                          new SqlParameter("parms_date2", ""),
                           new SqlParameter("parms_url","" ), new SqlParameter("parms_matchType","" ),
                        new SqlParameter("parms_matchRule", "")
                    };


                }
                else
                {
                    parms = new SqlParameter[] { 
                        new SqlParameter("pagesize",pageSize ),
                         new SqlParameter("pageindex",pageIndex ),
                         new SqlParameter("parms_date1",objParms[0]),

                          new SqlParameter("parms_date2", objParms[1]),
                           new SqlParameter("parms_url",objParms[2] ), 
                           new SqlParameter("parms_matchType",objParms[3] ),
                        new SqlParameter("parms_matchRule",objParms[4])
                    };


                }
                IBRSCommonDAL dal = new IBRSCommonDAL();

                LogBLL.Log("usp_mining_select_forum_pager", parms);
                dt = dal.SelectData("usp_mining_select_forum_pager", parms);

                foreach (DataRow row in dt.Rows)
                {
                    Forum formModel = new Forum();
                    formModel.Title = row["title"].ToString();
                    formModel.Author = row["author"].ToString();
                    formModel.SrcUrl = row["srcurl"].ToString();
                    formModel.PageView = Convert.ToInt32(row["pageview"]);
                    formModel.Reply = Convert.ToInt32(row["reply"]);
                    formModel.CreateDate = Convert.ToDateTime(row["createdate"]);
                    list.Add(formModel);
                }

                return list;
            }
            catch (Exception ex)
            {


                LogBLL.Error("FoumPagerService 获取数据异常", ex);

            }

            return list;
        }


        public int ForumRecordCountService(object[] objParms)
        {

            int recordCount = 0;
            try
            {
                SqlParameter[] parms = null;

                object[] obj = null;
                if (objParms == null)
                {
                    parms = new SqlParameter[] { 
                       
                         new SqlParameter("parms_date1",""),

                          new SqlParameter("parms_date2", ""),
                           new SqlParameter("parms_url",""), 
                           new SqlParameter("parms_matchType",""),
                        new SqlParameter("parms_matchRule","")
                    };
                    obj = new object[] { "", "", "", "", "" };

                }
                else
                {
                    //parms_date1, parms_date2, parms_url, parms_matchType, parms_matchRule 


                    parms = new SqlParameter[] { 
                     
                         new SqlParameter("parms_date1",objParms[0]),

                          new SqlParameter("parms_date2", objParms[1]),
                           new SqlParameter("parms_url",objParms[2] ), 
                           new SqlParameter("parms_matchType",objParms[3] ),
                        new SqlParameter("parms_matchRule",objParms[4])
                    };
                    obj = new object[] { objParms[0], objParms[1], objParms[2], objParms[3], objParms[4] };

                }
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_forum_RecordCount", parms);

                object objResult = dal.ExecuteScalar("usp_mining_select_forum_RecordCount", parms);
                recordCount = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {

                LogBLL.Error("ForumRecordCountService 获取数据异常", ex);
            }
            return recordCount;
        }

        public int InsertProjectListService(ProjectList pList)
        {

            int proiectId = 0;
            try
            {
                /*xaing 这里得到新增项目的Id 然会返回这个Id的值*/
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectname",pList.ProjectName ),
                    new SqlParameter("matchingruletype", pList.MatchingRuleType),
                    new SqlParameter("matchingrule",  pList.MatchingRule),
                    new SqlParameter("rsskey",pList.RssKey),
                    new SqlParameter("empid",  pList.EmpId),
                    new SqlParameter("createdate",  pList.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("enddate",pList.EndDate )
              };

                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Insert_ProjectList", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Insert_ProjectList", parms);
                proiectId = Convert.ToInt32(objResult);

            }
            catch (Exception ex)
            {

                LogBLL.Error("InsertProjectListService", ex);
            }
            return proiectId;
        }
        public int AllotCategoryService(int projectId, int cateId)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectid",projectId ), 
                    new SqlParameter("cateId",cateId )
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_insert_ProjectCate", parms);
                result = dal.ExecuteNonQuery("usp_mining_insert_ProjectCate", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("AllotCategoryService", ex);
            }
            return result;
        }

        public int UpdateProjectListRC_ForumService(int projectId, int RC_value)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectid",projectId ), 
                    new SqlParameter("rc_forum",RC_value )
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_projectlistRc_Forum", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_projectlistRc_Forum", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateProjectListRC_ForumService", ex);
            }
            return result;
        }

        public int UpdateProjectListRC_NewsService(int projectId, int RC_value)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectid",projectId ), 
                    new SqlParameter("rc_news",RC_value )
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_projectlistRc_News", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_projectlistRc_News", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateProjectListRC_NewsService", ex);
            }
            return result;
        }
        public int UpdateProjectListRC_BlogService(int projectId, int RC_value)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectid",projectId ), 
                    new SqlParameter("rc_blog",RC_value )
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_projectlistRc_Blog", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_projectlistRc_Blog", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateProjectListRC_BlogService", ex);
            }
            return result;
        }




        /// <summary>
        /// 查询 可运行的项目列表
        /// </summary>
        /// <returns></returns>
        public List<ProjectList> GetProjectListService(int searchType, string sarchkey, int runStatus, string loginName)
        {
            List<ProjectList> list = new List<ProjectList>();
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("searchType",searchType ),  
                new SqlParameter("sarchkey",sarchkey == null ? "" : sarchkey ), 
                new SqlParameter("runStatus",runStatus ),
                 new SqlParameter("empId",loginName )//登录名
            };


                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjectList", parms);
                dt = dal.SelectData("usp_mining_Select_ProjectList", parms);

                foreach (DataRow row in dt.Rows)
                {
                    ProjectList plistModel = new ProjectList();
                    plistModel.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    plistModel.ProjectName = row["ProjectName"].ToString();
                    plistModel.MatchingRuleType = Convert.ToInt32(row["MatchingRuleType"]);
                    plistModel.MatchingRule = row["MatchingRule"].ToString();
                    plistModel.EmpId = row["EmpId"].ToString();
                    plistModel.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    plistModel.EndDate = Convert.ToDateTime(row["EndDate"]);
                    plistModel.PList_RunStatus = Convert.ToInt32(row["RunStatus"]);
                    plistModel.ForumNum = Convert.ToInt32(row["ForumNum"]);
                    plistModel.NewsNum = Convert.ToInt32(row["NewsNum"]);
                    plistModel.BlogNum = Convert.ToInt32(row["BlogNum"]);
                    plistModel.MicroBlogNum = Convert.ToInt32(row["MicroBlogNum"]);

                    list.Add(plistModel);
                }

                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetProjectListService", ex);
            }

            return list;
        }


        public ProjectDetailModel GetProjectListDetailByProjectidService(int projectId)
        {

            ProjectDetailModel plistModel = null;

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId",projectId ),
            };

                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjectListDetilByProjectId", parms);
                dt = dal.SelectData("usp_mining_Select_ProjectListDetilByProjectId", parms);

                foreach (DataRow row in dt.Rows)
                {
                    string propjectName = row["ProjectName"].ToString();
                    if (string.IsNullOrEmpty(propjectName))
                    {
                        break;
                    }

                    plistModel = new ProjectDetailModel();
                    plistModel.ProjectName = propjectName;
                    plistModel.MatchingTypeName = row["MatchngTypeName"].ToString();
                    plistModel.MatchingRule = row["MatchingRule"].ToString();
                    plistModel.EmpId = row["EmpId"].ToString();
                    // Convert.ToDateTime(["EndDate"];
                    //Convert.ToInt32(row[
                    plistModel.EndDate = Convert.ToDateTime(row["EndDate"]);
                    plistModel.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    plistModel.ForumNum = Convert.ToInt32(row["ForumNum"]);
                    plistModel.NewsNum = Convert.ToInt32(row["NewsNum"]);
                    plistModel.BlogNum = Convert.ToInt32(row["BlogNum"]);
                    plistModel.MicroBlogNum = Convert.ToInt32(row["MicroBlogNum"]);
                }

                return plistModel;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetProjectListDetailByProjectidService 获取数据异常", ex);


            }

            return plistModel;
        }



        public List<SiteList> GetUseableSiteListByProjectIdService(int projectId, int siteType, int pageSize, int pageIndex)
        {
            List<SiteList> list = new List<SiteList>();

            try
            {


                DataTable dt = new DataTable();

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId",projectId ),
                 new SqlParameter("siteType", siteType),
                  new SqlParameter("pageSize",pageSize ),
                   new SqlParameter("pageIndex",pageIndex ),
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ShowUseableSiteListByProjectId", parms);
                dt = dal.SelectData("usp_mining_Select_ShowUseableSiteListByProjectId", parms);

                foreach (DataRow row in dt.Rows)
                {
                    SiteList sitelist = new SiteList();
                    sitelist.SiteId = Convert.ToInt32(row["SiteId"]);
                    //row[
                    sitelist.SiteName = row["SiteName"].ToString();
                    sitelist.PlateName = row["PlateName"].ToString();
                    sitelist.SiteUrl = row["SiteUrl"].ToString();

                    list.Add(sitelist);

                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetUseableSiteListByProjectIdService", ex);

            }

            return list;
        }

        public int GetRecordCountUseableSiteListByProjectIdService(int projectId, int siteType)
        {

            int recordCount = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId",projectId ), new SqlParameter("siteType",siteType )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_RecordCountUseableSiteListByProjectId", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Select_RecordCountUseableSiteListByProjectId", parms);
                recordCount = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetRecordCountUseableSiteListByProjectIdService", ex);
            }
            return recordCount;
        }

        public List<SiteList> GetSiteListBySiteTypeService(int siteType, int pageSize, int pageIndex, int searchType, string searchKey)
        {
            List<SiteList> list = new List<SiteList>();


            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("siteType",siteType ),
                 new SqlParameter("pageSize", pageSize),
                  new SqlParameter("pageIndex",pageIndex ),
                   new SqlParameter("searchType",searchType ),
                    new SqlParameter("searchKey", searchKey)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();

                LogBLL.Log("usp_mining_Select_SiteListBySiteType", parms);
                dt = dal.SelectData("usp_mining_Select_SiteListBySiteType", parms);

                foreach (DataRow row in dt.Rows)
                {
                    SiteList sitelist = new SiteList();
                    sitelist.SiteId = Convert.ToInt32(row["SiteId"]);
                    sitelist.SiteName = row["SiteName"].ToString();
                    sitelist.PlateName = row["PlateName"].ToString();
                    sitelist.SiteUrl = row["SiteUrl"].ToString();
                    sitelist.SiteRank = row.IsNull("SiteRank") ? 0 : Convert.ToInt32(row["SiteRank"]);
                    sitelist.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    sitelist.Remark = row["Remark"].ToString();
                    list.Add(sitelist);
                }

                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("usp_mining_Select_SiteListBySiteType 根据站点类型获取站点数据异常", ex);
                // base.Log(LogLevel.Error, ex.Message);
            }

            return list;
        }



        public int GetRecordCountSiteListBySiteTypeService(int siteType, int searchType, string searchKey)
        {

            int recordCount = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("siteType",siteType ),
                 new SqlParameter("searchType", searchType),
                  new SqlParameter("searchKey", searchKey)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_RecordCountSiteListBySiteType", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Select_RecordCountSiteListBySiteType", parms);
                recordCount = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetRecordCountSiteListBySiteTypeService", ex);


            }
            return recordCount;
        }



        public int InsertSiteListService(string siteName, string plateName, string siteUrl, int siteRank, int siteType)
        {

            int result = 0;
            try
            {


                //@sitename,@plateName,@siteUrl,@siteType,@siteRank,@createdate
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sitename",siteName ),
                 new SqlParameter("plateName", plateName),
                  new SqlParameter("siteUrl",siteUrl ),
                   new SqlParameter("siteType",siteType ),
                    new SqlParameter("siteRank",siteRank ),
                     new SqlParameter("createdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Insert_SiteListBySiteType", parms);

                result = dal.ExecuteNonQuery("usp_mining_Insert_SiteListBySiteType", parms);
            }
            catch (Exception ex)
            {

                LogBLL.Error("InsertSiteListService", ex);

            }
            return result;
        }



        public int UpdateSiteListBySiteIdService(int siteId, string siteName, string plateName, string siteUrl, int siteRank)
        {

            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("siteId",siteId ),
                new SqlParameter("siteName", siteName),
                new SqlParameter("plateName", plateName),
                new SqlParameter("siteUrl", siteUrl),
                new SqlParameter("siteRank",siteRank ),
                new SqlParameter("updateDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") )
            };

                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Update_SiteListBySiteId", parms);
                result = dal.ExecuteNonQuery("usp_mining_Update_SiteListBySiteId", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateSiteListBySiteIdService", ex);

            }
            return result;
        }


        public int DeleteSiteListBySiteIdService(int siteId)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("siteId", siteId),
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Delete_SiteListBySiteId", parms);
                result = dal.ExecuteNonQuery("usp_mining_Delete_SiteListBySiteId", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("DeleteSiteListBySiteIdService", ex);

            }
            return result;
        }


        #endregion


        #region SiteData 数据相关
        public List<SiteDataModel> GetSiteDateModelService(QueryDataModelParms queryDataParms)
        {

            List<SiteDataModel> list = new List<SiteDataModel>();

            object[] obj = new object[] {queryDataParms.projectId ,queryDataParms.pagesize,queryDataParms.pageindex,
                queryDataParms.sitetype,queryDataParms.searchType,queryDataParms.searchKey,queryDataParms.startTime,
                queryDataParms.endTime,queryDataParms.analysis,queryDataParms.showstatus,queryDataParms.attention,queryDataParms.hot};
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectId", queryDataParms.projectId),
                    new SqlParameter("pagesize", queryDataParms.pagesize),
                    new SqlParameter("pageindex", queryDataParms.pageindex),
                    new SqlParameter("sitetype", queryDataParms.sitetype),
                    new SqlParameter("searchType", queryDataParms.searchType),
                    new SqlParameter("searchKey", queryDataParms.searchKey),
                    new SqlParameter("startTime",queryDataParms.startTime ),
                    new SqlParameter("endTime", queryDataParms.endTime),
                    new SqlParameter("analysis", queryDataParms.analysis),
                    new SqlParameter("showstatus", queryDataParms.showstatus),
                    new SqlParameter("attention",queryDataParms.attention ),
                    new SqlParameter("hot", queryDataParms.hot)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjectDataByProjectId", parms);

                dt = dal.SelectData("usp_mining_Select_ProjectDataByProjectId", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {
                    SiteDataModel siteDataModel = new SiteDataModel();
                    siteDataModel.SD_Id = Convert.ToInt32(row["SD_Id"]);
                    siteDataModel.Title = row["Title"].ToString();
                    siteDataModel.Content = row["Content"].ToString();
                    siteDataModel.ContentDate = row["ContentDate"] == DBNull.Value ? Convert.ToDateTime(row["CreateDate"]) : Convert.ToDateTime(row["ContentDate"]);
                    siteDataModel.SrcUrl = row["SrcUrl"].ToString();
                    siteDataModel.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    siteDataModel.CreateDate = siteDataModel.ContentDate;
                    siteDataModel.SiteName = row["SiteName"].ToString();
                    siteDataModel.Analysis = row["Analysis"] == DBNull.Value ? 0 : Convert.ToInt32(row["Analysis"]);
                    siteDataModel.Attention = row["Attention"] == DBNull.Value ? 0 : Convert.ToInt32(row["Attention"]);
                    siteDataModel.ShowStatus = row["ShowStatus"] == DBNull.Value ? 0 : Convert.ToInt32(row["ShowStatus"]);
                    siteDataModel.PlateName = row["PlateName"].ToString();

                    //每一条,有6个Tag
                    siteDataModel.Tag1 = row["Tag1"] == DBNull.Value ? "" : row["Tag1"].ToString();
                    siteDataModel.Tag2 = row["Tag2"] == DBNull.Value ? "" : row["Tag2"].ToString();
                    siteDataModel.Tag3 = row["Tag3"] == DBNull.Value ? "" : row["Tag3"].ToString();
                    siteDataModel.Tag4 = row["Tag4"] == DBNull.Value ? "" : row["Tag4"].ToString();
                    siteDataModel.Tag5 = row["Tag5"] == DBNull.Value ? "" : row["Tag5"].ToString();
                    siteDataModel.Tag6 = row["Tag6"] == DBNull.Value ? "" : row["Tag6"].ToString();
                    
                    siteDataModel.Hot = row["Hot"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hot"]);
                    list.Add(siteDataModel);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetSiteDateModelService", ex);

            }


            return list;
        }

        public SiteDataModel GetSiteDateModelBySd_IdService(int sd_id)
        {

            SiteDataModel siteDataModel = new SiteDataModel();

            try
            {
                DataTable dt = new DataTable();

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sd_id", sd_id)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjectDataByBySd_Id", parms);
                dt = dal.SelectData("usp_mining_Select_ProjectDataByBySd_Id", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return siteDataModel;
                }
                foreach (DataRow row in dt.Rows)
                {
                    siteDataModel.SD_Id = Convert.ToInt32(row["SD_Id"]);
                    siteDataModel.Title = row["Title"].ToString();
                    siteDataModel.Content = row["Content"].ToString();
                    siteDataModel.ContentDate = row["ContentDate"] == DBNull.Value ? Convert.ToDateTime(row["CreateDate"]) : Convert.ToDateTime(row["ContentDate"]);
                    siteDataModel.SrcUrl = row["SrcUrl"].ToString();
                    //siteDataModel.SiteId = Convert.ToInt32(row["SiteId"]);
                    siteDataModel.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    siteDataModel.CreateDate = siteDataModel.ContentDate;
                    siteDataModel.SiteName = row["SiteName"].ToString();
                    siteDataModel.Analysis = row["Analysis"] == DBNull.Value ? 0 : Convert.ToInt32(row["Analysis"]);
                    siteDataModel.Attention = row["Attention"] == DBNull.Value ? 0 : Convert.ToInt32(row["Attention"]);
                    siteDataModel.ShowStatus = row["ShowStatus"] == DBNull.Value ? 0 : Convert.ToInt32(row["ShowStatus"]);
                    //每一条,有3个Tag
                    siteDataModel.Tag1 = row["Tag1"] == DBNull.Value ? "" : row["Tag1"].ToString();
                    siteDataModel.Tag2 = row["Tag2"] == DBNull.Value ? "" : row["Tag2"].ToString();
                    siteDataModel.Tag3 = row["Tag3"] == DBNull.Value ? "" : row["Tag3"].ToString();
                    siteDataModel.Tag4 = row["Tag4"] == DBNull.Value ? "" : row["Tag4"].ToString();
                    siteDataModel.Tag5 = row["Tag5"] == DBNull.Value ? "" : row["Tag5"].ToString();
                    siteDataModel.Tag6 = row["Tag6"] == DBNull.Value ? "" : row["Tag6"].ToString();

                    siteDataModel.Hot = row["Hot"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hot"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetSiteDateModelBySd_IdService", ex);

            }


            return siteDataModel;
        }


        public int GetSiteDateCountService(QueryDataModelParms queryDataParms)
        {

            int recordCount = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectId",queryDataParms.projectId ),
                    new SqlParameter("sitetype",queryDataParms.sitetype ),
                    new SqlParameter("searchType", queryDataParms.searchType),
                    new SqlParameter("searchKey",queryDataParms.searchKey ),
                    new SqlParameter("startTime",queryDataParms.startTime ),
                    new SqlParameter("endTime", queryDataParms.endTime),
                    new SqlParameter("analysis", queryDataParms.analysis),
                    new SqlParameter("showstatus",queryDataParms.showstatus ),
                    new SqlParameter("attention", queryDataParms.attention),
                    new SqlParameter("hot",queryDataParms.hot )
                          
            };

                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjectDataCountByProjectId", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Select_ProjectDataCountByProjectId", parms);
                recordCount = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetSiteDateCountService", ex);
            }
            return recordCount;
        }

        #endregion


        #region



        public int GetTagDataCountService(string searchKey)
        {

            int recordCount = 0;
            try
            {


                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("searchKey",searchKey )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_TagDataCount", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Select_TagDataCount", parms);
                recordCount = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetTagDataCountService", ex);
            }
            return recordCount;
        }

        public int DeleteTagByIdService(int tagId)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("tagId", tagId),
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Delete_TagById", parms);
                result = dal.ExecuteNonQuery("usp_mining_Delete_TagById", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("DeleteTagByIdService", ex);
            }
            return result;
        }

        public int InsertTagInfoService(string tagName, string secondTag, string koreanTranslate)
        {

            int result = 0;
            try
            {


                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("tagName", tagName),
                  new SqlParameter("secondTag", secondTag),
                    new SqlParameter("koreanTranslate",koreanTranslate )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Insert_Tag", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Insert_Tag", parms);
                result = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("InsertTagInfoService", ex);

            }
            return result;
        }

        public int UpdateTagInfoByTagIdService(int tagId, string tagName, string secondTag, string koreanTranslate)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("tagId",tagId ),
                 new SqlParameter("tagName", tagName),
                  new SqlParameter("secondTag", secondTag),
                   new SqlParameter("koreanTranslate",koreanTranslate )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Update_TagByTagId", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Update_TagByTagId", parms);
                result = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateTagInfoByTagIdService", ex);
            }
            return result;
        }

        public List<ProjectTagRelation> GetProjectByRunStatusService()
        {

            List<ProjectTagRelation> list = new List<ProjectTagRelation>();

            try
            {

                DataTable dt = new DataTable();
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjectListRunStatus");
                dt = dal.SelectData("usp_mining_Select_ProjectListRunStatus", null);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {

                    ProjectTagRelation project = new ProjectTagRelation();
                    project.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    project.ProjectName = row["ProjectName"].ToString();

                    list.Add(project);

                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetProjectByRunStatusService", ex);

            }


            return list;
        }

        public List<ProjectTagRelation> GetProjectTagRelationByProjectIdService(int projectId)
        {

            List<ProjectTagRelation> list = new List<ProjectTagRelation>();

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId", projectId)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Select_ProjetTagRelationByProlectId", parms);
                dt = dal.SelectData("usp_mining_Select_ProjetTagRelationByProlectId", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {
                    ProjectTagRelation projectTagRelation = new ProjectTagRelation();
                    projectTagRelation.ProjectId = Convert.ToInt32(row["ProjectId"]);
                    projectTagRelation.ProjectName = row["ProjectName"].ToString();

                    TagList taglist = new TagList();
                    taglist.Id = Convert.ToInt32(row["Id"]);
                    taglist.Tid = Convert.ToInt32(row["Tid"]);
                    taglist.TagName = row["TagName"].ToString();
                    taglist.KoreanTranslate = row["KoreanTranslate"].ToString();
                    projectTagRelation.TagList = taglist;
                    list.Add(projectTagRelation);

                }
                return list;
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetProjectTagRelationByProjectIdService", ex);
            }

            return list;
        }
        #endregion


        #region
        public int AllotProjectTagService(int projectId, List<string> listTags)
        {
            int result = 0;
            for (int i = 0; i < listTags.Count; i++)
            {
                try
                {
                    SqlParameter[] parms = new SqlParameter[]
                     {
                        new SqlParameter("projectId", projectId),
                        new SqlParameter("tagId", int.Parse(listTags[i]))
                     };

                    IBRSCommonDAL dal = new IBRSCommonDAL();
                    LogBLL.Log("usp_mining_Insert_ProjectTagByProjectId", parms);
                    result += dal.ExecuteNonQuery("usp_mining_Insert_ProjectTagByProjectId", parms);
                }
                catch (Exception ex)
                {
                    LogBLL.Error("AllotProjectTagService", ex);
                }
            }
            return result;
        }

        public int DeleteProjetTagRelationByProjectIdService(int projectId, int tagId)
        {

            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("projectId", projectId),
                     new SqlParameter("tagId",tagId )
                 };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Delete_ProjetTagRelationByProjectId", parms);
                result = dal.ExecuteNonQuery("usp_mining_Delete_ProjetTagRelationByProjectId", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("DeleteProjetTagRelationByProjectId", ex);
            }
            return result;
        }


        public int CkeckTag1StCountService(string tags)
        {
            int result = 0;
            try
            {
                string sql = "select count(distinct Tid) from dbo.TagList where Id in(" + tags + ")";

                IBRSCommonDAL dal = new IBRSCommonDAL();
                object objResult = dal.ExecuteScalar(sql);
                result = Convert.ToInt32(objResult);

            }
            catch (Exception ex)
            {

                LogBLL.Error("CkeckTag1StCountService", ex);
            }
            return result;
        }

        /*ashx  方法*/

        public int DeleteSiteDateBySd_idService(int sd_id)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sd_id", sd_id)
            };

                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Delete_SiteDataBySdId", parms);
                result = dal.ExecuteNonQuery("usp_mining_Delete_SiteDataBySdId", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("DeleteSiteDateBySd_idService", ex);
            }
            return result;
        }

        public int UpdateSiteDate_Analysis_BySd_idService(int sd_id, int analysis)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sd_id",sd_id ),
                 new SqlParameter("analysis",analysis )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_analysisBySd_id", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_analysisBySd_id", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateSiteDate_Analysis_BySd_idService", ex);
            }
            return result;
        }
        public int UpdateSiteDate_Hot_BySd_idService(int sd_id, int hot)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sd_id", sd_id), new SqlParameter("hot", hot)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_hotBySd_id", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_hotBySd_id", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateSiteDate_Hot_BySd_idService", ex);

            }
            return result;
        }


        public int UpdateSiteDate_Attention_BySd_idService(int sd_id, int attention)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sd_id", sd_id), new SqlParameter("attention", attention)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_attentionBySd_id", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_attentionBySd_id", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateSiteDate_Attention_BySd_idService", ex);

            }
            return result;
        }
        public int UpdateSiteDate_ShowStatus_BySd_idService(int sd_id, int showstatus)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("sd_id", sd_id),  new SqlParameter("showstatus", showstatus),
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_showstatusBySd_id", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_showstatusBySd_id", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateSiteDate_ShowStatus_BySd_idService", ex);

            }
            return result;
        }


        public string SelectProjectNameByProjectIdService(int projectid, int runStatus)
        {
            string sql = "select ProjectName from ProjectList where ProjectId = " + projectid + " and RunStatus=" + runStatus;
            string ProjectName = "";
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log(sql);
                object objResult = dal.ExecuteScalar(sql);
                ProjectName = objResult.ToString();
            }
            catch (Exception ex)
            {
                LogBLL.Error("SelectProjectNameByProjectIdService", ex);
            }
            return ProjectName;
        }

        /// <summary>
        /// 打标签
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="sd_id"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public int DoBatchTagService(int pid, int sd_id, int tagId)
        {

            int result = 0;
            try
            {
                object[] obj = new object[] { pid, sd_id, tagId };

                //打完标签的同时, 更新 showstatus = 1
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("pid",pid ), 
                new SqlParameter("sd_id", sd_id),
                new SqlParameter("tagid",tagId )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_sitedata_tag", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_sitedata_tag", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("DoBatchTagService", ex);

            }
            return result;
        }

        public int InsertImportSiteDataByProjectIdService(SiteDataModel dataModel)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectid",dataModel.ProjectId ), 
                new SqlParameter("title",dataModel.Title ), 
                new SqlParameter("srcurl",dataModel.SrcUrl ),
                new SqlParameter("sitename", dataModel.SiteName),
                   new SqlParameter("contentdate",dataModel.ContentDate.ToString("yyyy-MM-dd 12:00:00") ),
                      new SqlParameter("sitetype", dataModel.SiteType)
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_Insert_ImportSiteDataBy_ProjectId", parms);
                object objResult = dal.ExecuteScalar("usp_mining_Insert_ImportSiteDataBy_ProjectId", parms);
                result = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("InsertImportSiteDataByProjectIdService", ex);
            }

            return result;
        }

        #endregion


        public int InsertPullInfoService(int projectId, int classId, int mdifference)
        {

            int result = 0;
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("projectId", projectId),
                  new SqlParameter("classId", classId),
                    new SqlParameter("mdifference",mdifference )
                 };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_insert_PullInfo", parms);
                result = dal.ExecuteNonQuery("usp_mining_insert_PullInfo", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Error("InsertPullInfoService", ex);

            }
            return result;
        }

        public int UpdateProjectListStatusByProjectIdService(int projectId, int runStatus)
        {

            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
              
                   new SqlParameter("projectId", projectId),
                      new SqlParameter("runStatus", runStatus)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_update_ProjectListStatus", parms);
                result = dal.ExecuteNonQuery("usp_mining_update_ProjectListStatus", parms);

            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateProjectListStatusByProjectIdService", ex);
            }
            return result;
        }


    }
}

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
    public class CategoryDAL
    {
        public CategoryDAL() { }

        /// <summary>
        /// 插入一条数据,返回插入的Id
        /// </summary>
        /// <param name="catename"></param>
        /// <param name="empname"></param>
        /// <param name="classid"></param>
        /// <returns></returns>
        public int InsertCategoryInfoService(string catename, string empname, int classid)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("catename",catename ),
                     new SqlParameter("empname", empname),
                      new SqlParameter("classid",classid )
                                };
                IBRSCommonDAL dal = new IBRSCommonDAL();

                LogBLL.Log("usp_mining_insert_CategoryInfo", parms);

                object obj = dal.ExecuteScalar("usp_mining_insert_CategoryInfo", parms);
                result = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogBLL.Error("InsertCategoryInfoService", ex);
            }


            return result;
        }

        public List<SiteList> GetSiteListByCateIdService(int cateid)
        {

            List<SiteList> list = new List<SiteList>();
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("cateid",cateid )
            };
                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log("usp_mining_select_CategorySiteRelationByCateId", parms);
                DataTable dt = dal.SelectData("usp_mining_select_CategorySiteRelationByCateId", parms);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        SiteList sitelist = new SiteList();
                        sitelist.SiteId = Convert.ToInt32(row["SiteId"]);
                        sitelist.SiteName = row["SiteName"].ToString();
                        sitelist.PlateName = row["PlateName"].ToString();
                        sitelist.SiteUrl = row["SiteUrl"].ToString();
                        list.Add(sitelist);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetSiteListByCateIdService", ex);

            }

            return list;
        }


        public List<CategoryInfo> GetCategoryListService()
        {
            List<CategoryInfo> list = new List<CategoryInfo>();
            try
            {

                string sql = "select CateId,CategoryName,ClassId from dbo.CategoryInfo";
                DataTable dt = new DataTable();

                IBRSCommonDAL dal = new IBRSCommonDAL();
                LogBLL.Log(sql);
                dt = dal.SelectData(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CategoryInfo cateInfo = new CategoryInfo();
                        cateInfo.CateId = Convert.ToInt32(row["CateId"]);
                        cateInfo.CategoryName = row["CategoryName"].ToString();
                        cateInfo.ClassId = Convert.ToInt32(row["ClassId"]);
                        list.Add(cateInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetCategoryListService", ex);
            }
            return list;
        }

        public List<CategoryInfo> GetCategoryListByClassIdService(int classId)
        {
            List<CategoryInfo> list = new List<CategoryInfo>();
            try
            {

                string sql = "select CateId,CategoryName,ClassId,CreateDate from dbo.CategoryInfo where ClassId = " + classId;
               

                DataTable dt = new DataTable();
                LogBLL.Log(sql);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                dt = dal.SelectData(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CategoryInfo cateInfo = new CategoryInfo();
                        cateInfo.CateId = Convert.ToInt32(row["CateId"]);
                        cateInfo.CategoryName = row["CategoryName"].ToString();
                        cateInfo.ClassId = Convert.ToInt32(row["ClassId"]);
                        cateInfo.CreateDate = Convert.ToDateTime(row["CreateDate"]).ToString("yyyy-MM-dd");
                        object objCount = new IBRSCommonDAL().ExecuteScalar("select count(1) from dbo.CategorySiteRelation where CateId = " + cateInfo.CateId);
                        if (objCount != DBNull.Value)
                        {
                            cateInfo.SiteCount = Convert.ToInt32(objCount);
                        }
                        list.Add(cateInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetCategoryListByClassIdService", ex);
            }
            return list;
        

        }


    }
}

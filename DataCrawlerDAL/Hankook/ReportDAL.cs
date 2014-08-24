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
    public class ReportDAL
    {

        /// <summary>
        /// 话题社区来源排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_htsqlypmService(string startdate, string enddate, int projectId)
        {
            List<TopicReport> list = new List<TopicReport>();

            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_htsqlypm", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_htsqlypm", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }

                foreach (DataRow row in dt.Rows)
                {
                    TopicReport reportItem = new TopicReport();
                    reportItem.Id = Convert.ToInt32(row["Id"]);
                    reportItem.MessTitle = row["MessTitle"].ToString();
                    reportItem.AppearCount = Convert.ToInt32(row["AppearCount"]);
                    reportItem.AppearRate = (Convert.ToDecimal(row["AppearRate"])).ToString("##.##%");
                    list.Add(reportItem);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_htsqlypmService", ex);
            }

            return list;
        }
        /// <summary>
        /// 负面 话题社区来源排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_fmhtsqlypmService(string startdate, string enddate, int projectId)
        {
            List<TopicReport> list = new List<TopicReport>();

            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_fmhtsqlypm", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_fmhtsqlypm", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {
                    TopicReport reportItem = new TopicReport();
                    reportItem.Id = Convert.ToInt32(row["Id"]);
                    reportItem.MessTitle = row["MessTitle"].ToString();
                    reportItem.AppearCount = Convert.ToInt32(row["AppearCount"]);
                    reportItem.AppearRate = (Convert.ToDecimal(row["AppearRate"])).ToString("##.##%");
                    list.Add(reportItem);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_fmhtsqlypmService", ex);
            }

            return list;
        }

        /// <summary>
        /// 重复话题排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_cfhtpmService(string startdate, string enddate, int projectId)
        {
            List<TopicReport> list = new List<TopicReport>();

            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_cfhtpm", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_cfhtpm", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }

                foreach (DataRow row in dt.Rows)
                {
                    TopicReport reportItem = new TopicReport();
                    reportItem.Id = Convert.ToInt32(row["Id"]);
                    reportItem.MessTitle = row["MessTitle"].ToString();
                    reportItem.AppearCount = Convert.ToInt32(row["AppearCount"]);
                    list.Add(reportItem);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_cfhtpmService", ex);
            }

            return list;
        }

        /// <summary>
        /// 重复话题排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_fmcfhtpmService(string startdate, string enddate, int projectId)
        {
            List<TopicReport> list = new List<TopicReport>();

            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_fmcfhtpm", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_fmcfhtpm", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }

                foreach (DataRow row in dt.Rows)
                {
                    TopicReport reportItem = new TopicReport();
                    reportItem.Id = Convert.ToInt32(row["Id"]);
                    reportItem.MessTitle = row["MessTitle"].ToString();
                    reportItem.AppearCount = Convert.ToInt32(row["AppearCount"]);
                    list.Add(reportItem);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_fmcfhtpmService", ex);
            }

            return list;
        }




        /// <summary>
        ///  各媒介来源 数量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PieReport GetReport_gmjsllyService(string startdate, string enddate, int projectId)
        {
            PieReport reportItem = new PieReport();
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_gmjslly", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_gmjslly", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return reportItem;
                }

                foreach (DataRow row in dt.Rows)
                {
                    reportItem.Data1 = Convert.ToInt32(row["Forum"]);
                    reportItem.Data2 = Convert.ToInt32(row["News"]);
                    reportItem.Data3 = Convert.ToInt32(row["Blog"]);
                    reportItem.Data4 = Convert.ToInt32(row["Microblog"]);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_gmjsllyService", ex);
            }

            return reportItem;

        }

        /// <summary>
        /// 各调性话题数量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PieReport GetReport_gdxhtslService(string startdate, string enddate, int projectId)
        {
            PieReport reportItem = new PieReport();
            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_gdxhtsl", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_gdxhtsl", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return reportItem;
                }

                foreach (DataRow row in dt.Rows)
                {
                    reportItem.Data1 = Convert.ToInt32(row["Zheng"]);
                    reportItem.Data2 = Convert.ToInt32(row["Zhong"]);
                    reportItem.Data3 = Convert.ToInt32(row["Fu"]);
                    reportItem.Data4 = Convert.ToInt32(row["WU"]);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_gdxhtslService", ex);
            }

            return reportItem;

        }


        /// <summary>
        ///  话题声量趋势图
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<HuaTiInfo> GetReport_htslqstService(string startdate, string enddate, int projectId)
        {
            List<HuaTiInfo> list = new List<HuaTiInfo>();

            try
            {

                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_htslqst", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_htslqst", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }

                foreach (DataRow row in dt.Rows)
                {
                    HuaTiInfo huatiInfo = new HuaTiInfo();
                    huatiInfo.date = Convert.ToDateTime(row["ContentDate"]).ToString("yyyy-MM-dd");
                    huatiInfo.value = Convert.ToInt32(row["PrepareCount"]);
                    list.Add(huatiInfo);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_htslqstService", ex);
            }
            return list;
        }

          /// <summary>
        ///  负面话题声量趋势图
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<HuaTiInfo> GetReport_fmhtslqstService(string startdate, string enddate, int projectId)
        {
            List<HuaTiInfo> list = new List<HuaTiInfo>();
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_fmhtslqst", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_fmhtslqst", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {
                    HuaTiInfo huatiInfo = new HuaTiInfo();
                    huatiInfo.date = Convert.ToDateTime(row["ContentDate"]).ToString("yyyy-MM-dd");
                    huatiInfo.value = Convert.ToInt32(row["PrepareCount"]);
                    list.Add(huatiInfo);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_fmhtslqstService", ex);
            }
            return list;
        }

        /// <summary>
        /// 话题趋势表
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<HTQSB_Info> GetReport_htqsbService(string startdate, string enddate, int projectId)
        {
            List<HTQSB_Info> list = new List<HTQSB_Info>();
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                  new SqlParameter("startdate",startdate),
                  new SqlParameter("enddate",enddate),
                  new SqlParameter("projectId",projectId)
                };

                LogBLL.Log("usp_mining_report_htqsb", parms);
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt = dal.SelectData("usp_mining_report_htqsb", parms);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return list;
                }
                foreach (DataRow row in dt.Rows)
                {
                    HTQSB_Info htqsb = new HTQSB_Info();
                    htqsb.Id = Convert.ToInt32(row["Id"]);
                    htqsb.ContentDate = Convert.ToDateTime(row["ContentDate"]).ToString("yyyy-MM-dd");
                    htqsb.News_Z_Num = Convert.ToInt32(row["News_Z_Num"]);
                    htqsb.News_F_Num = Convert.ToInt32(row["News_F_Num"]);
                    htqsb.Blog_Z_Num = Convert.ToInt32(row["Blog_Z_Num"]);
                    htqsb.Blog_F_Num = Convert.ToInt32(row["Blog_F_Num"]);
                    htqsb.Forum_Z_Num = Convert.ToInt32(row["Forum_Z_Num"]);
                    htqsb.Forum_F_Num = Convert.ToInt32(row["Forum_F_Num"]);
                    htqsb.Microblog_F_Num = Convert.ToInt32(row["Microblog_F_Num"]);

                    list.Add(htqsb);
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("GetReport_fmhtslqstService", ex);
            }
            return list;
        }
      
        
    }
}

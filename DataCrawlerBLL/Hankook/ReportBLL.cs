using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataCrawler.Model.Hankook;
using DataCrawler.DAL.Hankook;
namespace DataCrawler.BLL.Hankook
{
    public class ReportBLL
    {
        ReportDAL dalService = new ReportDAL();


        /// <summary>
        /// 话题社区来源排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_htsqlypmManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_htsqlypmService(startdate, enddate, projectId);
        }


        /// <summary>
        /// 负面 话题社区来源排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_fmhtsqlypmManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_fmhtsqlypmService(startdate, enddate, projectId);
        }


        /// <summary>
        /// 重复话题排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_cfhtpmManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_cfhtpmService(startdate, enddate, projectId);
        }

        /// <summary>
        /// 重复话题排名
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TopicReport> GetReport_fmcfhtpmManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_fmcfhtpmService(startdate, enddate, projectId);
        }


        /// <summary>
        /// 各媒介来源 数量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<PieData> GetReport_gmjsllyManager(string startdate, string enddate, int projectId)
        {
            List<PieData> list = new List<PieData>();
            PieReport report = dalService.GetReport_gmjsllyService(startdate, enddate, projectId);

            PieData siteR1 = new PieData() { from = "论坛", value = report.Data1, color = "#FF6600" };
            PieData siteR2 = new PieData() { from = "新闻", value = report.Data2, color = "#F8FF01" };
            PieData siteR3 = new PieData() { from = "博客", value = report.Data3, color = "#B0DE09" };
            PieData siteR4 = new PieData() { from = "微博", value = report.Data4, color = "#0D8ECF" };
            list.Add(siteR1); list.Add(siteR2); list.Add(siteR3); list.Add(siteR4);
            return list;
        }


        /// <summary>
        /// 各调性话题数量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<PieData> GetReport_gdxhtslManager(string startdate, string enddate, int projectId)
        {
            List<PieData> list = new List<PieData>();
            PieReport report = dalService.GetReport_gdxhtslService(startdate, enddate, projectId);
            PieData siteR1 = new PieData() { from = "正", value = report.Data1, color = "#FF6600" };
            PieData siteR2 = new PieData() { from = "中", value = report.Data2, color = "#F8FF01" };
            PieData siteR3 = new PieData() { from = "负", value = report.Data3, color = "#B0DE09" };
            PieData siteR4 = new PieData() { from = "无", value = report.Data4, color = "#0D8ECF" };
            list.Add(siteR1); list.Add(siteR2); list.Add(siteR3); list.Add(siteR4);

            return list;
          
        }

        public List<HuaTiInfo>GetReport_htslqstManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_htslqstService(startdate, enddate, projectId);
        }

         public List<HuaTiInfo>GetReport_fmhtslqstManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_fmhtslqstService(startdate, enddate, projectId);
        }

         public List<HTQSB_Info> GetReport_htqsbManager(string startdate, string enddate, int projectId)
        {
            return dalService.GetReport_htqsbService(startdate, enddate, projectId);
        }
        
        
    }
}

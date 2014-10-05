using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System.Diagnostics;
namespace DataCrawler.BLL.Crawler
{
    public class SiteDataBLL 
    {
      

        public List<SiteDataModel> GetSiteDateModelManager(QueryDataModelParms queryDataParms)
        {
            //这里要做参数处理 queryDataParms
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.GetSiteDateModelService(queryDataParms);
        }

        public int GetSiteDateCountManager(QueryDataModelParms queryDataParms)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            //这里要做参数处理 queryDataParms
            return sqlDalAction.GetSiteDateCountService(queryDataParms);
        }

        public int DeleteSiteDataBySd_idManager(int sd_id)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.DeleteSiteDataBySd_idService(sd_id);
        }

        public int UpdateSiteDate_Analysis_BySd_idManager(int sd_id, int analysis)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.UpdateSiteDate_Analysis_BySd_idService(sd_id, analysis);
        }

        public int UpdateSiteDate_Hot_BySd_idManager(int sd_id, int hot)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.UpdateSiteDate_Hot_BySd_idService(sd_id, hot);
        }

        public int UpdateSiteDate_Attention_BySd_idManager(int sd_id, int attention)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.UpdateSiteDate_Attention_BySd_idService(sd_id, attention);
        }


        public int UpdateSiteDate_ShowStatus_BySd_idManager(int sd_id, int showstatus)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.UpdateSiteDate_ShowStatus_BySd_idService(sd_id, showstatus);
        }

        /// <summary>
        /// 根据Sd_id 查询单条数据
        /// </summary>
        /// <param name="sd_id"></param>
        /// <returns></returns>
        public SiteDataModel GetSiteDateModelBySd_IdManager(int sd_id)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.GetSiteDateModelBySd_IdService(sd_id);
        }

        public string SelectProjectNameByProjectIdManager(int projectid, int runStatus)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.SelectProjectNameByProjectIdService(projectid, runStatus);
        }

        public int InsertImportSiteDataByProjectIdManager(List<SiteDataModel> dataList)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int result = 0;
            foreach (SiteDataModel dataModel in dataList)
            {
                try
                {
                    result += sqlDalAction.InsertImportSiteDataByProjectIdService(dataModel);
                }
                catch (Exception ex)
                {
                    LogNet.LogBLL.Error("InsertImportSiteDataByProjectIdManager",ex);
                    result += 0;
                }
            }
            return result;
        }

        public int BatchTagListBySd_Id(DataTag dataTag)
        {
            int result = 0;
            try
            {
              DataTagDAL dataTagdalAction = new DataTagDAL();
              result = dataTagdalAction.UpdateDataTagBySD_IdService(dataTag);
               
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("BatchTagListBySd_Id", ex);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System.Diagnostics;

namespace DataCrawler.BLL.Crawler
{
    public class SiteListBLL 
    {

        public List<SiteList> GetSiteListBySiteTypeManager(int siteType, int pageSize, int pageIndex, int searchType, string searchKey)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            List<SiteList> list = sqlDalAction.GetSiteListBySiteTypeService(siteType, pageSize, pageIndex, searchType, searchKey);
            return list;
        }

        public int RecordCountSiteListBySiteTypeManager(int siteType, int searchType, string searchKey)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.GetRecordCountSiteListBySiteTypeService(siteType, searchType, searchKey);
        }

        public int InsertSiteListManager(string siteName, string plateName, string siteUrl, int siteRank, int siteType)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            return sqlDalAction.InsertSiteListService(siteName, plateName, siteUrl, siteRank, siteType);
        }

        public int UpdateSiteListBySiteIdManager(int siteId, string siteName, string plateName, string siteUrl, int siteRank)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int result = sqlDalAction.UpdateSiteListBySiteIdService(siteId, siteName, plateName, siteUrl, siteRank);
            return result;
        }

        public int DeleteSiteListBySiteIdManager(int siteId)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int recordCount = sqlDalAction.DeleteSiteListBySiteIdService(siteId);
            return recordCount;
        }
    }
}

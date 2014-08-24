using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using System.Data.Linq;
using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;

namespace DataCrawler.BLL.Crawler
{
    public class CategoryBLL
    {

        public int InsertCategoryManager(string catename, string empname, int classid)
        {
            CategoryDAL del = new CategoryDAL();
            return del.InsertCategoryInfoService(catename, empname, classid);
        }
        public List<SiteList> GetSiteListByCateIdManager(int cateid)
        {
            CategoryDAL del = new CategoryDAL();
            return del.GetSiteListByCateIdService(cateid);
        }

        public List<CategoryInfo> GetCategoryListManager()
        {
            CategoryDAL del = new CategoryDAL();
            return del.GetCategoryListService();
        }
        public List<CategoryInfo> GetCategoryListByClassIdManager(int classId)
        {
            CategoryDAL del = new CategoryDAL();
            return del.GetCategoryListByClassIdService(classId);
        }
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Data;




using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System.Diagnostics;

namespace DataCrawler.BLL.Crawler
{
    public class DataAnalysisBLL 
    {
        


         protected List<ProjectDetailModel> ProjectListDetailManager()
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            List<ProjectDetailModel> plist = sqlDalAction.ProjectListDetailService();
           
            return plist;
        }

         protected List<Forum> FoumPagerManager(int pageIndex, int pageSize, string[] objParms)
        {

            List<Forum> plist = new List<Forum>();
            ServerDaLAction sqlDalAction = new ServerDaLAction();

            if (objParms == null)
            {
                plist = sqlDalAction.FoumPagerService(pageSize, pageIndex, null);
            }
            else
            {
                plist = sqlDalAction.FoumPagerService(pageSize, pageIndex, objParms);
            }
            return plist;
        }

         protected int ForumRecordCountManager(string[] objParms)
        {
            ServerDaLAction sqlDalAction = new ServerDaLAction();
            int pageCount = sqlDalAction.ForumRecordCountService(objParms);
            return pageCount;
        }
      
    }
}

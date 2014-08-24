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
    public class DataTagDAL
    {

        public int UpdateDataTagBySD_IdService(DataTag dataTag)
        {
            int result = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[] {
                   new SqlParameter("sd_id",dataTag.SD_Id),
                   new SqlParameter("tag1",dataTag.Tag1),
                   new SqlParameter("tag2",dataTag.Tag2),
                   new SqlParameter("tag3",dataTag.Tag3),
                   new SqlParameter("tag4",dataTag.Tag4),
                   new SqlParameter("tag5",dataTag.Tag5),
                   new SqlParameter("tag6",dataTag.Tag6)
                };
                IBRSCommonDAL dal = new IBRSCommonDAL();

                LogBLL.Log("usp_mining_update_datatagBySD_Id", parms);
                Object objResult = dal.ExecuteScalar("usp_mining_update_datatagBySD_Id", parms);
               result = Convert.ToInt32(objResult);
            }
            catch (Exception ex)
            {
                LogBLL.Error("UpdateDataTagBySD_IdService", ex);
            }
            return result;
        }

    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using DataCrawler.Model.Crawler;
using RionSoft.IBRS.Business.DAL;

namespace DataCrawler.DAL.Crawler
{
    public class PullDataDAL
    {
        //获取正在运行的 项目,runstatus = 2, 0 是项目建立, 1 是分配分类,站点等,2 表示项目启动,3 是项目暂停 99是废弃
        private static string sql_1 = "select ProjectId,MatchingRule from projectlist where RunStatus = 1 ";

        private static string sql_2 = "select distinct ClassId from ProjectDetail where projectid = @projectId and (runStatus = 0 or runStatus = 1) ";

        private static string sql_maxCid = "select max(Cid) from @tableName where (ProjectId = @projectId or ProjectId = 0)";

        private static string sql_3 = @"select Cid,Title,Content,ContentDate,SrcUrl,@tableName.CreateDate,SiteList.SiteName from @tableName 
               join SiteList on @tableName.SiteId = SiteList.SiteId 
               where (@kwyWordWhere) 
                  and (ProjectId = @projectId or ProjectId = 0) and  Cid > @maxSD_P_Cid and Cid < @maxDiffCid
                 order by Cid,ContentDate desc";

        private static string sql_4 = " select MinCid,MDifference from dbo.PullInfo where ProjectId = @projectId and classId = @classId";

        private static string sql_5 = "update PullInfo set MinCid=@minCid,UpdateDate = '@updateDate' where ProjectId =@projectId  and classId = @classId";


        public void PullDataFirstService()
        {
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();

                DataTable dt_1 = dal.SelectData(sql_1);
                if (dt_1 != null)
                {
                    foreach (DataRow row in dt_1.Rows)
                    {
                        try
                        {
                            string projectId = row["ProjectId"].ToString();
                            string kwyWord = row["MatchingRule"].ToString();
                            //对keyWord进行 转化sql查询语句

                            string mSQL = sql_2.Replace("@projectId", projectId);
                            DataTable dt_2 = new IBRSCommonDAL().SelectData(mSQL);

                            if (dt_2 != null)
                            {
                                foreach (DataRow rows_2 in dt_2.Rows)
                                {
                                    string classId = rows_2["ClassId"].ToString();
                                    PullData2Next(projectId, kwyWord, classId);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            LogNet.LogBLL.Error("PullDataFirstService foreach", ex);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("PullDataFirstService ", ex);
            }
        }

        public int PullData2Next(string projectId, string kwyWord, string classId)
        {
            string kwyWordWhere = GetWhereStr(kwyWord);
            int result = 0;
            string tableName = GetTableName(classId);
            if (tableName != "")
            {
                try
                {
                    string mSQL2 = sql_maxCid.Replace("@tableName", tableName).Replace("@projectId", projectId);
                    object objRes = new IBRSCommonDAL().ExecuteScalar(mSQL2);

                    int maxOriCid = objRes == DBNull.Value ? 0 : Convert.ToInt32(objRes);
                    if (maxOriCid <= 0)
                    {
                        return 0;
                    }

                    string msql_4 = sql_4.Replace("@projectId", projectId).Replace("@classId", classId);

                    DataTable dt_4 = new IBRSCommonDAL().SelectData(msql_4);

                    if (dt_4.Rows == null || dt_4.Rows.Count <= 0)
                    {
                        return 0;
                    }
                    int minCid = Convert.ToInt32(dt_4.Rows[0]["MinCid"]);
                    int maxDiffCid = Convert.ToInt32(dt_4.Rows[0]["MDifference"]);

                    int maxCid = minCid + maxDiffCid;
                    bool isContinue = true;
                    if (maxCid > maxOriCid)
                    {
                        maxCid = maxOriCid;
                    }
                    else if (maxCid == maxOriCid)
                    {
                        isContinue = false;
                    }
                    if (isContinue)
                    {

                        string mSQl_3 = sql_3.Replace("@tableName", tableName).Replace("@projectId", projectId)
                       .Replace("@kwyWordWhere", kwyWordWhere).Replace("@maxSD_P_Cid", minCid.ToString()).Replace("@maxDiffCid", maxCid.ToString());



                        DataTable dt_3 = new IBRSCommonDAL().SelectData(mSQl_3);
                        //help.ExecuteDataTable(trans, CommandType.Text, mSQl_3);
                        //插入数据库
                        InsertSiteData(dt_3, projectId, classId);

                        string mSQL_5 = sql_5.Replace("@projectId", projectId).Replace("@classId", classId)
                          .Replace("@minCid", maxCid.ToString()).Replace("@updateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        new IBRSCommonDAL().SelectData(mSQL_5);

                    }

                }
                catch (Exception ex)
                {

                    LogNet.LogBLL.Error(ex.Message);
                }
            }//if (tableName!="")结束

            return result;
        }

        #region 得到表名
        /// <summary>
        /// 根据ClassId得到表名
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public string GetTableName(string classId)
        {
            string tableName = "";
            switch (classId)
            {
                case "1":
                    tableName = "Forum";
                    break;
                case "2":
                    tableName = "News";
                    break;
                case "3":
                    tableName = "Blog";
                    break;
                case "5":
                    tableName = "MicroBlog";
                    break;
                default:
                    tableName = "";
                    break;
            }
            return tableName;
        }

        #endregion

        #region  得到查询条件
        /// <summary>
        /// 得到查询条件
        /// </summary>
        /// <param name="strRegular"></param>
        /// <returns></returns>
        public string GetWhereStr(string strRegular)
        {
            string[] strContainsArray = new string[] { };

            if (strRegular.Contains("-"))
            {
                strContainsArray = strRegular.Substring(0, strRegular.IndexOf('-')).Split('+');
            }
            else
            {
                strContainsArray = strRegular.Split('+');
            }

            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(" 1<0 ");
            for (int i = 0; i < strContainsArray.Length; i++)
            {
                sbulider.Append(" or title like '%" + strContainsArray[i] + "%' or Content like '%" + strContainsArray[i] + "%'");
            }
            return sbulider.ToString();
        }
        #endregion

        #region 插入数据到SiteData表
        /// <summary>
        /// 插入数据到SiteData表
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="dt_3"></param>
        /// <param name="projectId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public int InsertSiteData(DataTable dt_3, string projectId, string classId)
        {
            int result = 0;
            if (dt_3 != null && dt_3.Rows.Count > 0)
            {
                string Cid, Title, Content, SrcUrl, SiteName;
                DateTime ContentDate, CreateDate;
                foreach (DataRow rows_3 in dt_3.Rows)
                {
                    try
                    {
                        Cid = rows_3["Cid"].ToString();
                        Title = rows_3["Title"].ToString();
                        Content = rows_3["Content"].ToString();
                        ContentDate = rows_3["ContentDate"] == DBNull.Value ? ContentDate = DateTime.Now : Convert.ToDateTime(rows_3["ContentDate"]);
                        SrcUrl = rows_3["SrcUrl"].ToString();
                        CreateDate = rows_3["CreateDate"] == DBNull.Value ? ContentDate = DateTime.Now : Convert.ToDateTime(rows_3["CreateDate"]);
                        SiteName = rows_3["SiteName"].ToString();
                        int sitetype = int.Parse(classId);
                        SqlParameter[] parameters =
                            {
                                new SqlParameter("cid",Cid), new SqlParameter("title",Title),
                                new SqlParameter("content",Content), new SqlParameter("contentdate",ContentDate),
                                new SqlParameter("srcurl",SrcUrl), new SqlParameter("projectid",projectId),
                                new SqlParameter("createdate",CreateDate), new SqlParameter("sitetype",sitetype),
                                new SqlParameter("sitename",SiteName)
                            };
                       // LogNet.LogBLL.Log("usp_mining_insert_SiteData", parameters);
                        result += new IBRSCommonDAL().ExecuteNonQuery("usp_mining_insert_SiteData", parameters);

                    }
                    catch (Exception ex)
                    {
                        LogNet.LogBLL.Error(ex.Message);
                        //时间转化失败
                    }
                }//# foreach结束
            }//If (dt_3!=null) 结束

            return result;
        }
        #endregion

    }
}

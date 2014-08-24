using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using RionSoft.IBRS.Business.DAL;
using ConPullSiteData;

namespace DataCrawler.DAL.Crawler
{
    public class PullDataDAL
    {
        //获取正在运行的 项目,runstatus = 2, 0 是项目建立, 1 是分配分类,站点等,2 表示项目启动,3 是项目暂停 99是废弃

        private static string sql_GetProjectsWithRunStatus1 = "select ProjectId,MatchingRuleType,MatchingRule from projectlist where RunStatus = 1 ";

        private static string sql_GetActiveClassIdsByProjectId = "select distinct ClassId from ProjectDetail where projectid = @projectId and (runStatus = 0 or runStatus = 1) ";

        private static string sql_maxCid = "select max(Cid) from @tableName where (ProjectId = @projectId or ProjectId = 0)";

        private static string sql_receiptCommonData = "Select RC_Forum,RC_News,RC_Blog from ProjectList where ProjectId=@projectId";

        private static string sql_GetContentData =
              @"select Cid,Title,Content,ContentDate,SrcUrl,@tableName.CreateDate,SiteList.SiteName,SiteList.SiteId,SiteList.PlateName 
                from @tableName 
                join SiteList on @tableName.SiteId = SiteList.SiteId 
                where (@kwyWordWhere) 
                    and (ProjectId = @projectId @isRcData) 
                    and  Cid > @maxSD_P_Cid 
                    and Cid < @maxDiffCid
                order by Cid,ContentDate desc";

        private static string sql_GetPullInfoCid = " select MinCid,MDifference from dbo.PullInfo where ProjectId = @projectId and classId = @classId";

        private static string sql_5 = "update PullInfo set MinCid=@minCid,UpdateDate = '@updateDate' where ProjectId =@projectId  and classId = @classId";


        // private static string sql_hankook = "insert into dbo.hankook(title,content,url,analysis,tags,pulished,contend,[type]) values(@title,@content,@url,@analysis,@tags,@pulished,@contend,@type)";


        /// <summary>
        /// 从爬虫 大范围 拉取 论坛,新闻,博客,微博数据
        /// </summary>
        public void PullDataFirstService()
        {
            try
            {
                IBRSCommonDAL dal = new IBRSCommonDAL();
                DataTable dt_1 = dal.SelectData(sql_GetProjectsWithRunStatus1);
                if (dt_1 != null)
                {
                    foreach (DataRow row in dt_1.Rows)
                    {
                        try
                        {
                            string projectId = row["ProjectId"].ToString();
                            string kwyWord = row["MatchingRule"].ToString();
                            int matchType = Convert.ToInt32(row["MatchingRuleType"]);
                            //对keyWord进行 转化sql查询语句
                            string mSQL = sql_GetActiveClassIdsByProjectId
                                .Replace("@projectId", projectId);
                            DataTable dt_2 = new IBRSCommonDAL().SelectData(mSQL);

                            if (dt_2 != null)
                            {
                                foreach (DataRow rows_2 in dt_2.Rows)
                                {
                                    string classId = rows_2["ClassId"].ToString();
                                    if (classId == "1")
                                    {
                                        Console.WriteLine("项目 为论坛");
                                    }
                                    else if (classId == "2")
                                    {
                                        Console.WriteLine("项目 为新闻");
                                    }
                                    else if (classId == "3")
                                    {
                                        Console.WriteLine("项目 为博客");
                                    }
                                    else if (classId == "5")
                                    {
                                        Console.WriteLine("项目 为微博");
                                    }

                                    PullData2Next(projectId, kwyWord, classId, matchType);


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


        public int PullData2Next(string projectId, string kwyWord, string classId, int matchType)
        {
            string kwyWordWhere = GetWhereStr(kwyWord, matchType);
            int result = 0;
            string tableName = GetTableName(classId);
            if (tableName != "")
            {
                try
                {
                    string mSQL2 = sql_maxCid.Replace("@tableName", tableName).Replace("@projectId", projectId);
                    object objRes = new IBRSCommonDAL().ExecuteScalar(mSQL2);

                    int maxContentTableCid = objRes == DBNull.Value ? 0 : Convert.ToInt32(objRes);
                    if (maxContentTableCid <= 0)
                    {
                        return 0;
                    }

                    string msql_4 = sql_GetPullInfoCid.Replace("@projectId", projectId).Replace("@classId", classId);

                    DataTable dt_4 = new IBRSCommonDAL().SelectData(msql_4);

                    if (dt_4.Rows == null || dt_4.Rows.Count <= 0)
                    {
                        return 0;
                    }
                    int minCid = Convert.ToInt32(dt_4.Rows[0]["MinCid"]);
                    int maxDiffCid = Convert.ToInt32(dt_4.Rows[0]["MDifference"]);

                    int maxCid = minCid + maxDiffCid;
                    bool needPull = true;
                    if (maxCid > maxContentTableCid)
                    {
                        maxCid = maxContentTableCid;
                    }

                    if (minCid == maxContentTableCid)
                    {
                        needPull = false;
                    }

                    //是否继续拉数据,如果需要继续 拉取数据
                    if (needPull)
                    {

                        string sql_rcData = sql_receiptCommonData.Replace("@projectId", projectId);

                        DataTable dt_Rc_Data = new IBRSCommonDAL().SelectData(sql_rcData);

                        int rc_forum = int.Parse(dt_Rc_Data.Rows[0]["RC_Forum"].ToString());
                        int rc_news = int.Parse(dt_Rc_Data.Rows[0]["RC_News"].ToString());
                        int rc_blog = int.Parse(dt_Rc_Data.Rows[0]["RC_Blog"].ToString());

                        string RcDataStr = "";
                        if ((tableName == "Forum" && rc_forum == 1) || (tableName == "News" && rc_news == 1) || tableName == "Blog" && rc_blog == 1)
                        {
                            RcDataStr = "or ProjectId = 0";
                        }

                        string mSQl_3 = sql_GetContentData.Replace("@tableName", tableName).Replace("@projectId", projectId).Replace("@isRcData", RcDataStr)
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
        public string GetWhereStr(string strRegular, int matchType)
        {
            string[] strContainsArray = new string[] { };
            string[] strNoContainsArray = new string[] { };
            string[] strMustContainsArray = new string[] { };

            if (strRegular.Contains("-"))
            {
                //非
                // strContainsArray = strRegular.Substring(0, strRegular.IndexOf('-')).Split('+'); //包含-,先去掉前面的,得到+
                strNoContainsArray = strRegular.Substring(strRegular.IndexOf('-') + 1).Split('-');
            }
            if (strRegular.Contains("&"))
            {
                //且

                int end = strRegular.Length - 1;
                if (strRegular.Contains("-"))
                {
                    end = strRegular.IndexOf('-') - 1;
                }

                strMustContainsArray = strRegular.Substring(strRegular.IndexOf('&') + 1, end - strRegular.IndexOf('&')).Split('&');

                Console.WriteLine(strMustContainsArray.Length);
            }

            if (strRegular.Contains("+"))
            {

                if (strRegular.Contains("&"))
                {
                    strContainsArray = strRegular.Substring(0, strRegular.IndexOf('&')).Split('+');
                }
                else if (strRegular.Contains("-"))
                {
                    strContainsArray = strRegular.Substring(0, strRegular.IndexOf('-')).Split('+');
                }
                else
                {
                    strContainsArray = strRegular.Split('+');
                }
            }
            else
            {
                //不包含+,只有单个的搜索关键字


                strContainsArray = new string[] { strRegular };

            }
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(" 1 < 0 ");
            for (int i = 0; i < strContainsArray.Length; i++)
            {
                if (matchType == 0)
                {
                    //matchType = 0
                    sbulider.Append(" or (title like '%" + strContainsArray[i] + "%' or Content like '%" + strContainsArray[i] + "%') ");
                }
                else if (matchType == 1)
                {
                    //只匹配标题
                    sbulider.Append(" or (title like '%" + strContainsArray[i] + "%') ");
                }
                else if (matchType == 2)
                {
                    //只匹配内容
                    sbulider.Append(" or (Content like '%" + strContainsArray[i] + "%') ");
                }
                else
                {
                    sbulider.Append(" ");
                }

            }
            for (int i = 0; i < strMustContainsArray.Length; i++)
            {
                if (matchType == 0)
                {
                    //matchType = 0
                    sbulider.Append(" and (title like '%" + strMustContainsArray[i] + "%' or Content like '%" + strMustContainsArray[i] + "%') ");
                }
                else if (matchType == 1)
                {
                    //只匹配标题
                    sbulider.Append(" and (title like '%" + strMustContainsArray[i] + "%')");
                }
                else if (matchType == 2)
                {
                    //只匹配内容
                    sbulider.Append(" and (Content like '%" + strMustContainsArray[i] + "%') ");
                }
                else
                {
                    sbulider.Append(" ");
                }

            }
            for (int i = 0; i < strNoContainsArray.Length; i++)
            {
                if (matchType == 0)
                {
                    //matchType = 0
                    sbulider.Append(" and (title not like '%" + strNoContainsArray[i] + "%' and Content not like '%" + strNoContainsArray[i] + "%') ");
                }
                else if (matchType == 1)
                {
                    //只匹配标题
                    sbulider.Append(" and (title not like '%" + strNoContainsArray[i] + "%') ");
                }
                else if (matchType == 2)
                {
                    //只匹配内容
                    sbulider.Append(" and (Content not like '%" + strNoContainsArray[i] + "%') ");
                }
                else
                {
                    sbulider.Append(" ");
                }
            }
            return sbulider.ToString();
        }

        //仅用来测试的方法
        private void testRegular()
        {
            //string resular = "A1+M1&B1&C1-D1-E1";
            string resular = "JOICO+嘉珂";
            Console.WriteLine(GetWhereStr(resular, 0));
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
                string Cid, Title, Content, SrcUrl, SiteName, plateName;
                DateTime ContentDate, CreateDate;
                int siteId;
                foreach (DataRow rows_3 in dt_3.Rows)
                {
                    try
                    {
                        Cid = rows_3["Cid"].ToString();
                        Title = rows_3["Title"].ToString();
                        Content = rows_3["Content"].ToString();
                        ContentDate = rows_3["ContentDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rows_3["ContentDate"]);
                        SrcUrl = rows_3["SrcUrl"].ToString();
                        CreateDate = rows_3["CreateDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rows_3["CreateDate"]);
                        SiteName = rows_3["SiteName"].ToString();
                        siteId = Convert.ToInt32(rows_3["SiteId"]);
                        plateName = rows_3["PlateName"].ToString();
                        int sitetype = int.Parse(classId);
                        SqlParameter[] parameters =
                            {
                                new SqlParameter("cid",Cid), 
                                new SqlParameter("title",Title),
                                new SqlParameter("content",Content), 
                                new SqlParameter("contentdate",ContentDate),
                                new SqlParameter("srcurl",SrcUrl), 
                                new SqlParameter("projectid",projectId),
                                new SqlParameter("createdate",CreateDate), 
                                new SqlParameter("sitetype",sitetype),
                                new SqlParameter("sitename",SiteName),
                                new SqlParameter("siteid",siteId),
                                new SqlParameter("platename",plateName)
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


        #region 导入Rss数据

        public int InsertSiteDataByRssData(List<RssDataInfo> listRss)
        {
            int result = 0;
            foreach (RssDataInfo item in listRss)
            {
                //@title,@content,@srcurl,@projectId,@createdate,@sitetype,@sitename,@analysis,@showstatus,@tag1
                try
                {
                    SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("title",item.title),
                    new SqlParameter("content",item.description),
                    new SqlParameter("srcurl",item.link),
                    new SqlParameter("projectId",item.ProjectId),
                    new SqlParameter("contentdate",Convert.ToDateTime(item.Published)),
                    new SqlParameter("sitetype",2),//2表示新闻
                    new SqlParameter("sitename",item.sitename),
                    new SqlParameter("analysis",item.analysis),
                    new SqlParameter("showstatus",2), //2表示状态为已审核
                    new SqlParameter("tag1",item.tags)
                };

                    // LogNet.LogBLL.Log("usp_mining_insert_siedateByRssData", parms);

                    result += new IBRSCommonDAL().ExecuteNonQuery("usp_mining_insert_siedateByRssData", parms);
                }
                catch (Exception ex)
                {
                    LogNet.LogBLL.Error("InsertSiteDataByRssData foreach", ex);
                }
            }
            return result;
        }
        #endregion

        #region 获取Rss 项目个关键字的键值对
        public Dictionary<int, string> GetRssP_And_KDictServie()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            string sql_RssP_And_KDict = "select distinct ProjectId,RssKey from ProjectList where RssKey is not null and EndDate>getdate() ";

            try
            {
                DataTable dt_RssP_And_K = new IBRSCommonDAL().SelectData(sql_RssP_And_KDict);

                if (dt_RssP_And_K != null && dt_RssP_And_K.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_RssP_And_K.Rows)
                    {
                        int projectId = Convert.ToInt32(row["ProjectId"]);
                        string RssKey = row["RssKey"].ToString();
                        dict.Add(projectId, RssKey);
                    }
                }
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("GetRssP_And_KDictServie", ex);
            }

            return dict;
        }
        #endregion


        #region  暂时不用 插入韩泰 Rss数据, 已改版

        /*
        /// <summary>
        /// 插入韩泰 Rss数据
        /// </summary>
        /// <param name="listRss"></param>
        /// <returns></returns>
        public int InsertHankook(List<HanKookRss> listRss)
        {
            int result = 0;
            foreach (HanKookRss item in listRss)
            {

                //插入数据库  hankook
                //sql_hankook
                //插入 SiteData 表

                SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("title",item.title),
                    new SqlParameter("content",item.description),
                    new SqlParameter("url",item.link),
                    new SqlParameter("analysis",item.analysis),
                    new SqlParameter("tags",item.tags),
                    new SqlParameter("published",Convert.ToDateTime(item.Published)),
                    new SqlParameter("contend",item.contend),
                    new SqlParameter("type",item.type),
                    new SqlParameter("sitename",item.sitename)
                };

                result += new IBRSCommonDAL().ExecuteNonQuery("usp_mining_insert_hankook", parms);

            }

            return result;
        }
        */

        #endregion
    }
}

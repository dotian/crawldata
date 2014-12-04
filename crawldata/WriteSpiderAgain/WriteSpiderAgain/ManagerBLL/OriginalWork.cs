using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using WriteSpiderAgain.EntityModel;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using WriteSpiderAgain.ServiceDAL;
using System.IO;
namespace WriteSpiderAgain.ManagerBLL
{
    public class OriginalWork
    {
        ServiceDALAction dalAction = new ServiceDALAction();
        CommonHelper common = new CommonHelper();
        HttpHelper _hh = new HttpHelper();
      
        #region 对新浪微博 进行必要的转码 解码格式 utf-8
        /// <summary>
        /// 这里是 访问新浪微博 对中文进行转码 使用
        /// </summary>
        /// <param name="encodeStr"></param>
        /// <returns></returns>
        public string StrDecode(string strHTML)
        {
            strHTML = strHTML.Replace(@"\/", "/").Replace(@"\""", "\"").Replace(@"\n", "").Replace(@"\r", "").Replace(@"\t", "");
            string[] temp = strHTML.Split(new string[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder biuilder = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length == 4)
                {
                    temp[i] = ((char)Convert.ToInt32(temp[i], 16)).ToString();
                }
                else if (i > 1)
                {
                    string m = temp[i].Substring(4);
                    temp[i] = ((char)Convert.ToInt32(temp[i].Substring(0, 4), 16)).ToString() + m;
                }
            }

            string result = string.Join("", temp);
            strHTML = Regex.Replace(result, @"\r\n|\n|\t", "", RegexOptions.IgnoreCase);
            return strHTML;
        }

        #endregion
        public static object objReadXml = new object();
        public int GetWorkPage(CrawlTarget crawlTarget)
        {
            //进入任务 模式
            //首先更新 这个任务
            //System.Threading.Thread.Sleep(3000);
            int resultCount = 0;
            try
            {
                //解析 Content模板

                 TemplateModel templateModel  = null;
                lock (objReadXml)
                {
                   string xmlPath = "TemplateXMl\\" + crawlTarget.Tid + ".xml";
                   if (!File.Exists(xmlPath))
                   {
                       templateModel = new TemplateModel(File.ReadAllText(xmlPath));
                   }else
	               {
                        templateModel = new TemplateModel(crawlTarget.TemplateContent);
	               }
                }
              
                string strKeyWordEncode = crawlTarget.KeyWords.Replace("+", " ");
                strKeyWordEncode = System.Web.HttpUtility.UrlEncode(strKeyWordEncode, Encoding.GetEncoding(crawlTarget.SiteEncoding));
                crawlTarget.SiteUrl = crawlTarget.SiteUrl.Replace("<>",strKeyWordEncode);
              
                string strHTML = "";
                bool rev = _hh.Open(crawlTarget.SiteUrl, Encoding.GetEncoding(crawlTarget.SiteEncoding), ref strHTML);

                if (!rev)
                {
                    //进入这里直接返回0
                    //超时, 连接被意外关闭, 远程主机强迫, time out, 403, 500
                    return 0;
                }

                strHTML = Regex.Replace(strHTML, @"\r\n|\r|\n|\t", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (crawlTarget.ClassName.ToLower()=="microblog")
                {
                    //新浪微博要转码
                     strHTML = StrDecode(strHTML);
                }
                Regex reg = new Regex(templateModel.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection matchCollection = reg.Matches(strHTML);

                if (matchCollection.Count > 1)
                {
                    #region  判断是相对路径还是绝对路径
                    string strSiteEntry = "";
                    int ipos = crawlTarget.SiteUrl.LastIndexOf('/');
                    strSiteEntry = ipos == 6 ? crawlTarget.SiteUrl + "/" : crawlTarget.SiteUrl.Substring(0, ipos) + "/";
                    #endregion

                    foreach (Match match in matchCollection)
                    {
                        string inhtmlGrops1 = match.Groups[1].Value.Trim();
                        if (inhtmlGrops1.Length == 0)
                            continue;
                         DataModel model = new DataModel();
                        try
                        {
                            model = GetDataModel(templateModel, inhtmlGrops1, strSiteEntry, crawlTarget.SiteEncoding);

                             if (model==null)
                             {
                                 continue;
                             }
                        }
                        catch 
                        {
                            continue;
                        }
                       
                        if (true)
                        {
                            //模板有匹配到正确的数据,才能够插入数据库,不然信息 变成 垃圾数据
                            model.ProjectID = crawlTarget.ProjectId;
                            model.SiteId = crawlTarget.SiteId;
                            SqlParameter[] parms;
                            switch (crawlTarget.ClassName.ToLower())
                            {
                                case "forum":
                                    parms = GetForumParmsSQL(model);
                                    resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_Forum", parms);
                                    break;
                                case "news":
                                    parms = GetNewsParmsSQL(model);
                                    resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_News", parms);
                                    break;
                                case "blog":
                                    parms = GetBlogsParmsSQL(model);
                                    resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_Blog", parms);
                                    break;
                                default:
                                    parms = GetMicroblogSQL(model);
                                    resultCount += HelperSQL.ExecNonQuery("usp_Spider_Insert_MicroBlog", parms);
                                    break;
                            }
                            //插入到数据库
                            //本来失败还要进入失败 库的,但是好像不需要
                        }

                    }

                }
                return resultCount>0?1:0;
            }
            catch
            {

                return 0;
            }
        }

        #region Create SQLParms
        public SqlParameter[] GetForumParmsSQL(DataModel model)
        {
            //Title,Content,ContentDate,Author,[Views],Replies,CreateDate,SrcUrl,SiteId,projectId

            SqlParameter[] parms = new SqlParameter[] { 
              new SqlParameter("title",model.Title),
              new SqlParameter("content",model.Content??model.Title),
              new SqlParameter("contentdate",model.ContentDate??Convert.DBNull),
              new SqlParameter("author",model.Author??Convert.DBNull),
              new SqlParameter("pageview",model.PageView<=0?1:model.PageView),
              new SqlParameter("reply",model.Reply<=0?1:model.Reply),
              new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              new SqlParameter("srcUrl",model.SiteUrl),
              new SqlParameter("siteid",model.SiteId),
              new SqlParameter("projectid",model.ProjectID)
            };
            return parms;
        }

        public SqlParameter[] GetNewsParmsSQL(DataModel model)
        {
            //Title ,Content,ContentDate,SrcUrl,SiteId,CreateDate,projectId

            SqlParameter[] parms = new SqlParameter[] { 
              new SqlParameter("title",model.Title),
              new SqlParameter("content",model.Content),
              new SqlParameter("contentdate",model.ContentDate??Convert.DBNull),
              new SqlParameter("srcUrl",model.SiteUrl),
              new SqlParameter("siteid",model.SiteId),
              new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              new SqlParameter("projectid",model.ProjectID)
            };
            return parms;

        }

        public SqlParameter[] GetBlogsParmsSQL(DataModel model)
        {
            //Title ,Content,ContentDate,SrcUrl,SiteId,CreateDate,projectId
            //SrcUrl, Content, Title, Author, CreateDate, SiteId, ContentDate, projectid
            SqlParameter[] parms = new SqlParameter[] { 
              new SqlParameter("title",model.Title),
              new SqlParameter("content",model.Content==null?model.Title:model.Content),
              new SqlParameter("contentdate",model.ContentDate??Convert.DBNull),
            new SqlParameter("author",model.Author==null?Convert.DBNull:model.Author),
              new SqlParameter("srcUrl",model.SiteUrl),
              new SqlParameter("siteid",model.SiteId),
               new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              new SqlParameter("projectid",model.ProjectID)
            };
            return parms;
        }

        /// <summary>
        /// 组装  新浪微博 microbloggings 参数
        /// </summary>
        /// <param name="parms">参数列表</param>
        /// <param name="strSrcUrl">微博用户地址</param>
        /// <param name="strContent">内容</param>
        /// <param name="strAuthor">作者</param>
        /// <param name="strSiteID">站点Id</param>
        /// <param name="strContentDate">微博发表时间</param>
        /// <param name="strProjectID">项目Id</param>
        /// <returns></returns>
        public SqlParameter[] GetMicroblogSQL(DataModel dataModel)
        {
            //Content,ContentDate,SrcUrl,SiteId,projectid,CreateDate,Author,Title
            /* 微博没有标题,还是 以内容当做标题*/
            SqlParameter[] parms = new SqlParameter[] { 
              new SqlParameter("title",dataModel.Content.Length>200?dataModel.Content.Substring(0,190)+"...":dataModel.Content),
              new SqlParameter("content",dataModel.Content),
              new SqlParameter("contentdate",dataModel.ContentDate??Convert.DBNull),
              new SqlParameter("author",dataModel.Author),
              new SqlParameter("srcUrl",dataModel.SiteUrl),
              new SqlParameter("siteid",dataModel.SiteId),
              new SqlParameter("projectid",dataModel.ProjectID),
              new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              
            };
            return parms;
        }

        #endregion

        public DataModel GetDataModel(TemplateModel templateModel, string inputMatchHtml, string strSiteEntry, string encod)
        {
            if (inputMatchHtml.Trim().Length == 0)
                return null;
            DataModel dataModel = new DataModel();
            dataModel.SiteUrl = GetUrl(templateModel.SrcUrlRegex, inputMatchHtml, ref strSiteEntry);

            #region //匹配 Title
           
                //GetContent(dataModel.Url, encod);
            if (templateModel.TitleRegex!="")
            {
                 dataModel.Title = common.GetMatchRegex(templateModel.TitleRegex, inputMatchHtml);
                //去掉 标题里面的 标签<>
                 dataModel.Title = Regex.Replace(dataModel.Title.Trim(), "<.+?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase).Trim();
               
                if (string.IsNullOrEmpty(dataModel.Title))
                 {
                     return null;
                 }
            }
           
            #endregion
            #region //匹配 Content
            if (templateModel.ContentRegex != "")
            {
              
                string contentHtml  = GetContent(dataModel.SiteUrl,"utf-8");
                contentHtml = Regex.Replace(contentHtml,@"\r\n|\r|\n","");

                dataModel.Content = common.GetMatchRegex(templateModel.ContentRegex, contentHtml);
                dataModel.Content = common.NoHTML(dataModel.Content).Trim();
            }

            #endregion


            #region //匹配 Author
            if (templateModel.AuthorRegex != "")
            {
                dataModel.Author = common.NoHTML(common.GetMatchRegex(templateModel.AuthorRegex, inputMatchHtml));
            }
            #endregion

           
            #region //匹配 Replies
            if (templateModel.RepliesRegex != "")
            {
                string strReplies = common.NoHTML(common.GetMatchRegex(templateModel.RepliesRegex, inputMatchHtml));
                int repliesInt = 0;
                if (strReplies.IndexOf('/') >= 0)
                {
                    strReplies =strReplies.Substring(0, strReplies.IndexOf('/'));
                }
                int.TryParse(strReplies, out repliesInt);
                dataModel.Reply = repliesInt;
            }
            #endregion

            #region //匹配 View
            if (templateModel.ViewsRegex != "")
            {
                string strView = common.NoHTML(common.GetMatchRegex(templateModel.ViewsRegex, inputMatchHtml));
                int viewInt = 0;
                if (strView.IndexOf('/') >= 0)
                {
                    strView = strView.Substring(strView.IndexOf('/') + 1);
                }
                int.TryParse(strView, out viewInt);
                dataModel.PageView = viewInt;
            }
            #endregion

            #region //匹配ContentDate
            if (templateModel.ContentDateRegex != "")
            {
                string strContentDate = common.GetMatchRegex(templateModel.ContentDateRegex, inputMatchHtml).Trim();

                DateTime time;
                bool b_time = false;
                if (strContentDate.Trim().Length <=6)
                {
                    b_time = false;
                }
                else if (strContentDate.Contains("-") && strContentDate.Contains(":"))
                {
                    if (!b_time && strContentDate.Split('-').Length <= 2)
                    {
                        //格式 可能是 MM-dd hh:mm  07-12 07:50 ,我们要 补上年份
                        strContentDate = DateTime.Now.ToString("yyyy-") + strContentDate;
                        DateTime.TryParse(strContentDate, out time);
                    }
                    DateTime.TryParse(strContentDate, out time);
                    if (time.Ticks > 100000)
                    {
                        //格式可以直接转换
                        dataModel.ContentDate = time;
                        b_time = true;
                    }
                }
                else
                {
                    DateTime.TryParse(strContentDate, out time);
                    if (time.Ticks > 100000)
                    {
                        //格式可以直接转换
                        dataModel.ContentDate = time;
                        b_time = true;
                    }
                }
                if (!b_time)
                {
                    dataModel.ContentDate = null;
                    //yyyy-mm-dd
                }
            }
            #endregion
            return dataModel;
        }

        public string GetUrl(string templateModel, string inputMatchHtml, ref string strSiteEntry)
        {
            #region 得到站点Url
            //mc2[0].Groups[1].Value
            string resultUrl = "";
            string strUrl = common.NoHTML(common.GetMatchRegex(templateModel, inputMatchHtml));
            if (strUrl != "")
            {
                if (strUrl.IndexOf("http://") != -1)
                {
                    resultUrl = strUrl;
                }
                else
                {
                    if (strUrl.IndexOf('/') == 0)
                    {
                        resultUrl = strSiteEntry + strUrl.Substring(1);
                    }
                    else if (strUrl.IndexOf("../") == 0)
                    {

                        strSiteEntry = strSiteEntry.Substring(0, strSiteEntry.Length - 1);
                        int count = Regex.Matches(strUrl, "../").Count;
                        for (int i = 0; i < count; i++)
                        {
                            strSiteEntry = strSiteEntry.Substring(0, strSiteEntry.LastIndexOf('/'));

                        }
                        strSiteEntry = strSiteEntry + "/";
                        resultUrl = strSiteEntry + strUrl.Replace("../", "");
                    }
                    else
                    {
                        resultUrl = strSiteEntry + strUrl;
                    }
                }
            }
            else
            {
                return null;
            }

            #endregion

            return resultUrl;
        }


    

        public string GetContent(string resultUrl, string encod)
        {
            string strHTML = "";
            bool bRev = _hh.Open(resultUrl, Encoding.GetEncoding(encod), ref strHTML);
            if (bRev == false)
            {
                return "";
            }
            return strHTML;
        }
    }
}

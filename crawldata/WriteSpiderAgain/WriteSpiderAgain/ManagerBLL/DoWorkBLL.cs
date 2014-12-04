using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace WebSpider
{
    public class DoWork
    {
        static string CapText(Match m)
        {
            return "";
        }

        public void doTest()
        {
            string strHTML = "";
           // bool rev = _hh.Open("http://weibo.com/u/3087668437?c=spr_web_sq_baidus_weibo_t400031", Encoding.GetEncoding("GB2312"), ref strHTML);
            bool rev = _hh.Open("http://s.weibo.com/weibo/%25E8%25A7%2582%25E4%25B8%2596%25E9%259F%25B3?topnav=1&wvr=5&b=1", Encoding.GetEncoding("utf-8"), ref strHTML);
            //http://s.weibo.com/weibo/%25E8%25A7%2582%25E4%25B8%2596%25E9%259F%25B3?topnav=1&wvr=5&b=1

           strHTML = StrDecode(strHTML);
            NoHTML(ref strHTML);

        }
       
       
      
        /// <summary>
        /// 获取 网站页面全部内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="revData"></param>
        /// <returns></returns>
        public static bool OpenURL(string url, Encoding encoding, ref string revData)
        {
            try
            {
                System.Net.HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url).AbsoluteUri);
                
                req.Timeout = 30000;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), encoding); // 解码方式
                revData = sr.ReadToEnd();
                return true;
            }
            catch (Exception e)
            {
                revData = e.Message;
            }
            return false;

        }
        /// <summary>
        /// 匹配 对应的 节点 
        /// </summary>
        /// <param name="root">xml模版</param>
        /// <param name="noteName">得到节点名称所对应的内容</param>
        /// <param name="inputMatchHtml">待匹配的html</param>
        /// <param name="strPatton">精确匹配 正则表达式</param>
        /// <param name="StrRestlt">返回匹配的结果</param>
        public static void GetMatchRegex(string patten, string inputMatchHtml, string strPatton, ref string StrRestlt)
        {
            if (patten != null)
            {
                Regex regex = new Regex(patten, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection mc = regex.Matches(inputMatchHtml);
                if (mc.Count > 0)
                {
                    if (strPatton == null || strPatton == "")
                    {
                        //一次匹配 就得到了结果
                        StrRestlt = mc[0].Groups[1].Value;
                        return;
                    }
                    //需要精确匹配
                    regex = new Regex(strPatton, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    string strInit = mc[0].Groups[0].Value;
                    mc = regex.Matches(strInit);
                    if (mc.Count > 0)
                    {
                        StrRestlt = mc[0].Groups[1].Value.Trim();
                    }
                }
            }
        }


        #region
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

        public static void NoHTML(ref string Htmlstring) //去除HTML标记   
        {   
              //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);   
              //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);   

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);   
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"\r\n|\n|\t", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");   
            Htmlstring.Replace(">", "");   
            Htmlstring.Replace("/r/n", "");
            Htmlstring.Replace(@"\r", "").Replace(@"\n","");
            Htmlstring = Htmlstring.Replace(" ", "");   
        }

        #region 得到xml模版的节点信息,用正则来匹配html,可二次匹配

        /// <summary>
        /// 匹配 对应的 节点 
        /// </summary>
        /// <param name="root">xml模版</param>
        /// <param name="noteName">得到节点名称所对应的内容</param>
        /// <param name="inputMatchHtml">待匹配的html</param>
        /// <param name="strPatton">xml模版的节点信息不能精确匹配的时候,strPatton 用于二次匹配的正则表达式</param>
        /// <param name="StrRestlt">返回匹配的结果</param>
        public void GetMatchRegex(XmlNode root, string noteName, string inputMatchHtml, string strPatton, ref string StrRestlt)
        {
            XmlNode cnode = root.SelectSingleNode(noteName);
            if (cnode != null && cnode.FirstChild != null)
            {
                Regex regex = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection mc = regex.Matches(inputMatchHtml);
                if (mc.Count > 0)
                {
                    if (strPatton == null || strPatton == "")
                    {
                        //一次匹配 就得到了结果
                        StrRestlt = mc[0].Groups[1].Value;
                        return;
                    }
                    //需要精确匹配
                    regex = new Regex(strPatton, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    string strInit = mc[0].Groups[0].Value;
                    mc = regex.Matches(strInit);
                    if (mc.Count > 0)
                    {
                        StrRestlt = mc[0].Groups[1].Value.Trim();
                    }
                }
            }
        }
        #endregion

        private static string GetURL(string strSrcUrl, string strSiteEntry, int ipos)
        {
            if (strSrcUrl.IndexOf("http://") != -1) //全路径
            {

            }
            else
            {
                if (strSrcUrl.IndexOf("/") == 0)
                {
                    if (ipos == 6)
                    {
                        strSrcUrl = strSiteEntry + strSrcUrl.Substring(1);
                    }
                    else
                    {
                        if (strSrcUrl.Substring(1).IndexOf("/") < 0)
                        {
                            strSrcUrl = strSiteEntry + strSrcUrl.Substring(1);
                        }
                        else //相对路径
                        {
                            int i = strSiteEntry.Substring(7).IndexOf("/");
                            strSrcUrl = i >= 0 ? strSiteEntry.Substring(0, i + 7) + "/" + strSrcUrl.Substring(1) : strSrcUrl = strSiteEntry + strSrcUrl.Substring(1);
                        }
                    }
                }
            }
            return strSrcUrl;
        }

        /// <summary>
        /// 得到 url 路径  有的是 相对的 有的是绝对的
        /// </summary>
        /// <param name="strSiteEntry"></param>
        /// <param name="ipos"></param>
        /// <param name="strSrcUrl"></param>
        /// <param name="urlMatch">mc2[0].Groups[1].Value</param>
        private static void GetMatchURL(ref string strSiteEntry, int ipos, ref string strSrcUrl, string urlMatch)
        {
            if (urlMatch.IndexOf("http://") != -1) //全路径
            {
                strSrcUrl = urlMatch;
            }
            else
            {
                if (urlMatch.IndexOf('/') == 0)
                    if (ipos == 6)
                        strSrcUrl = strSiteEntry + urlMatch.Substring(1);
                    else
                    {
                        if (urlMatch.Substring(1).IndexOf('/') < 0) //不是相对路径
                        {
                            strSrcUrl = strSiteEntry + urlMatch.Substring(1);
                        }
                        else //相对路径
                        {
                            //得到root
                            int i = strSiteEntry.Substring(7).IndexOf('/');
                            strSrcUrl = i >= 0 ? strSiteEntry.Substring(0, i + 7) + "/" : strSiteEntry;
                            strSrcUrl += urlMatch.Substring(1);
                        }
                    }
                else if (urlMatch.IndexOf("../") == 0)
                {
                    strSiteEntry = strSiteEntry.Substring(0, strSiteEntry.Length - 1);
                    int count = Regex.Matches(urlMatch, "../").Count;
                    for (int l = 0; l < count; l++)
                    {
                        strSiteEntry = strSiteEntry.Substring(0, strSiteEntry.LastIndexOf('/'));
                    }
                    strSiteEntry = strSiteEntry + "/";
                    strSrcUrl = strSiteEntry + urlMatch.Replace("../", "");
                }
                else
                {
                    int i = strSiteEntry.Substring(7).IndexOf('/');
                    strSrcUrl = i > 0 ? strSiteEntry.Substring(0, i + 7) + "/" : strSiteEntry;
                    strSrcUrl += urlMatch;
                }
            }
        }

        /// <summary>
      /// 增删改 统一调用的方法
      /// </summary>
      /// <param name="sqlStr">sql语句</param>
      /// <param name="parms">参数列表</param>
      /// <param name="errorSqlStr">异常时的sql语句 default默认</param>
      /// <param name="errorParms">异常时的 sql参数</param>
      /// <returns></returns>
        private int ExecNonQuery(string sqlStr, SqlParameter[] parms,  string strSiteID,string strSite)
        {
             Mysql mysql = GlobalVars.ConnectionPoolVars.GConnectionPool.GetConnection();
             lock (mysql)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mysql.GetConnection();
                    cmd.CommandTimeout = 120;
                    cmd.CommandText = sqlStr;
                    if (parms!=null)
	                {
                        cmd.Parameters.AddRange(parms);
	                }
                    int rows = cmd.ExecuteNonQuery();
                    mysql.isUsing = false;
                }
                catch (Exception e)
                {
                    #region 异常捕获
                    try
                    {
                        if (e.Message.IndexOf("事务在触") < 0)
                        {
                            if (e.Message.IndexOf("事务(进程") < 0)
                            {
                                try
                                {
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.CommandTimeout = 60;
                                    cmd.Connection = mysql.GetConnection();
                                    cmd.CommandText =  "INSERT INTO errorsite (siteid, siteurl, error_message, CrawlFinishDate) VALUES (@p1, @p2, @p3, @p4)";
                                    cmd.Parameters.AddRange(new SqlParameter[]{
                                       new SqlParameter("@p1",strSiteID),
                                       new SqlParameter("@p2",strSite), 
                                       new SqlParameter("@p3",e.Message), 
                                       new SqlParameter("@p4", DateTime.Now.ToString("yyyy'-'MM'-'dd"))
                                      });
                                    cmd.ExecuteNonQuery();
                                }
                                catch 
                                {
                                }
                            }
                            else //锁错误
                            return 0;
                        }
                    }
                    catch 
                    {
                    }
                    #endregion
                }
                mysql.isUsing = false;
            }
            return 1;
        }

        /// <summary>
        /// 改写的GetContentOnePage 方法 得到 一页的数据 
        /// </summary>
        /// <param name="root">文档中单个节点</param>
        /// <param name="strSite">站点url</param>
        /// <param name="strSiteID">站点ID</param>
        /// <param name="strEncoding">解码规则</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strProjectID">项目Id</param>
        /// <returns> 1  成功;0  超时;1 其他错误； </returns>
        private int GetContentOnePageChange(XmlNode root, string strSite, string strSiteID, string strEncoding, string strClassName, string strProjectID)
        {
            //新浪微博
            if (strClassName.CompareTo("microbloggings") != 0)
            {
                return 1;
            }
            //System.Threading.Thread.Sleep(3000);//为了 减缓 搜索 频率 先暂停一下
            bool bInsertedError = false;
            XmlNode node = root.SelectSingleNode("Node");//查找<Node>     
            string pattern = node.FirstChild.Value;
            string strHTML = "";
            bool rev = _hh.Open(strSite, Encoding.GetEncoding(strEncoding), ref strHTML);

            #region 查看链接是否正确
            if (!rev)
            {
                if (strHTML.IndexOf("超时") >= 0 || strHTML.IndexOf("time out") >= 0 || strHTML.IndexOf("远程主机强迫") >= 0
                    || strHTML.IndexOf("连接被意外关闭") >= 0 || strHTML.IndexOf("500") >= 0 || strHTML.IndexOf("403") >= 0)
                    return 0;
                else
                {
                    InsertErrorList(strSiteID, strSite, "", strHTML);
                    return 0;
                }
            }
            #endregion
            strHTML = strHTML.Replace(@"\r", "").Replace(@"\n", "");

            if (strClassName.CompareTo("microbloggings") == 0) strHTML = StrDecode(strHTML);//转码  //新浪微博 要转码
            
            Regex reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(strHTML);

            int resultCount = 0;
            if (mc.Count > 1)
            {
                string strSrcUrl, strContent, strTitle, strAuthor, strCreateDate, strReplies,strViews, strContentDate;
                strSrcUrl = strContent = strTitle = strAuthor = strCreateDate = strReplies = strViews = strContentDate = "";
                SqlParameter[] parms;
                string sqlStr ="";

                #region  判断是相对路径还是绝对路径
                string strSiteEntry = "";
                int ipos = strSite.LastIndexOf('/');
                strSiteEntry = ipos == 6 ? strSite + "/" : strSite.Substring(0, ipos) + "/";
                #endregion

                if (strClassName.CompareTo("microbloggings") == 0)
                {
                    #region  类型是 新浪微博
                    foreach (Match m in mc)
                    {
                        //得到 微博的东西
                        GetMicrobloggings(root, m.Groups[0].Value, ref strSrcUrl, ref strContent, ref strAuthor, ref strContentDate);
                        if (strSrcUrl == "" || strSrcUrl==null || strContent == "" || strContent == null)
                        {
                            return 0;
                               
                        }
                        //得到 参数列表 sql语句
                        sqlStr = GetMicrobloggingsSQL(out parms, strSrcUrl, strContent, strAuthor, strSiteID, strContentDate, strProjectID);
                        //插入到数据库
                        resultCount+= ExecNonQuery(sqlStr, parms, strSiteID, strSite);
                    }
                    #endregion
                }
                else if (strClassName.CompareTo("news") == 0 || strClassName.CompareTo("newsdata") == 0)
                {
                    #region 类型是 news
                     //http://news.fznews.com.cn/shehui/more/
                    foreach (Match m in mc)
                    {
                        GetNews(root, m.Groups[0].Value, ref strSrcUrl, ref strContent, ref strTitle, ref strContentDate, strSiteID, strSite, 
                            ref bInsertedError,ref strSiteEntry, ipos, m.Groups[1].Value,strEncoding);
                        if (strContent.CompareTo("")==0) continue;
                        if (strTitle.CompareTo("") == 0) continue;
                        sqlStr = GetNewsSQL(out parms, strSrcUrl, strContent, strTitle, strSiteID, strContentDate, strProjectID, strClassName);
                        resultCount += ExecNonQuery(sqlStr, parms, strSiteID, strSite);
                    }
                    #endregion
                }
                else if (strClassName.CompareTo("forums") == 0 || strClassName.CompareTo("baiduforum") == 0)
                {
                    #region  类型是 forums
                    foreach (Match m in mc)
                    {
                       GetForums(root, m.Groups[0].Value,ref strSrcUrl,ref strContent,ref strTitle,ref  strContentDate,ref  strAuthor,ref  strReplies,
                           ref strViews, strSiteID, strSite, ref bInsertedError,ref  strSiteEntry, ipos, m.Groups[1].Value, strEncoding);
                        if (strContent.CompareTo("")==0) continue;
                        if (strTitle.CompareTo("") == 0) continue;
                       sqlStr = GetForumsSQL(out parms,strSrcUrl,strContent,strTitle,strAuthor,strReplies,strViews,strSiteID,strContentDate,strProjectID,strClassName);
                       resultCount += ExecNonQuery(sqlStr, parms, strSiteID, strSite);
                    }
                    #endregion
                }
                else
                { 
                    #region  类型是 博客
                    foreach (Match m in mc)
	                {
                        GetBlogs(root, m.Groups[0].Value, ref strSrcUrl, ref strContent, ref strTitle, ref strAuthor, ref strContentDate, ref strSiteEntry, ipos,
                            strSiteID, strSite, ref strEncoding, ref bInsertedError);
                        if (strContent.CompareTo("")==0) continue;
                        if (strTitle.CompareTo("") == 0) continue;
                        sqlStr =GetBlogsSQL(out parms,strSrcUrl,strContent,strTitle,strAuthor,strSiteID,strContentDate,strProjectID,strClassName);
                        resultCount += ExecNonQuery(sqlStr, parms, strSiteID, strSite);
                    }
                    #endregion

                }

                return resultCount = resultCount>0?1:0;//返回 爬下来的结果  1表示正常 0表示爬取失败
            }
            else
            {
                nsGlobalOutput.Output.Write("Node has error siteid = " + strSiteID);
                InsertErrorList(strSiteID, strSite, "", "Node not found!");
                return 0;
            }
        }

        #region 博客

        /// <summary>
        /// 组装 Bloggs 参数
        /// </summary>
        /// <param name="parms">参数列表</param>
        /// <param name="strSrcUrl">url 地址</param>
        /// <param name="strContent">内容</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strAuthor">作者</param>
        /// <param name="strSiteID">站点Id</param>
        /// <param name="strContentDate">内容时间</param>
        /// <param name="strProjectID">项目Id</param>
        /// <param name="strClassName">类名</param>
        /// <returns></returns>
        public string GetBlogsSQL(out SqlParameter[] parms, string strSrcUrl, string strContent, string strTitle,string strAuthor, 
            string strSiteID, string strContentDate, string strProjectID, string strClassName)
        {
            #region 类型为博客 blogs
            string sqlStr = "INSERT INTO " + strClassName + " (SrcUrl, Content, Title, Author, CreateDate, SiteId, ContentDate, projectid) VALUES (" +
                                      "@SrcUrl, @Content, @Title, @Author, @CreateDate, @SiteId, @ContentDate, @projectid)";
            parms = new SqlParameter[8];
            parms[0] = new SqlParameter("@SrcUrl", strSrcUrl.CompareTo("") == 0 ? Convert.DBNull : strSrcUrl);
            parms[1] = new SqlParameter("@Content", strContent);
            parms[2] = new SqlParameter("@Title", strTitle.CompareTo("") == 0 ? Convert.DBNull : strTitle);
            parms[3] = new SqlParameter("@Author", strAuthor.CompareTo("") == 0 ? Convert.DBNull : strAuthor);
            parms[4] = new SqlParameter("@CreateDate", DateTime.Now.ToString());
            parms[5] = new SqlParameter("@SiteId", strSiteID);
            DateTime date = new DateTime();
            parms[6] = new SqlParameter("@ContentDate", strContentDate.CompareTo("") == 0 ? Convert.DBNull : DateTime.TryParse(strContentDate, out date) ? strContentDate : strContentDate);
            parms[7] = new SqlParameter("@projectid", strProjectID.CompareTo("") == 0 ? Convert.DBNull : strProjectID);
            return sqlStr;
            #endregion
        }


        public void GetBlogs(XmlNode root, string inputMatchHtml, ref string strSrcUrl, ref string strContent,ref string strTitle,
            ref string strAuthor, ref string strContentDate, ref string strSiteEntry, int ipos, string strSiteID, string strSite,
            ref string strEncoding, ref bool bInsertedError)
        {
           // SrcUrl,Title,Content,ContentDate,Author

            if (inputMatchHtml.Trim().Length == 0) return;
            string strPatton = "";

            #region 得到 URL
            GetMatchRegex(root, "SrcUrl", inputMatchHtml, strPatton, ref strSrcUrl);
            #endregion

            #region  得到Content 
            strPatton = "";
            GetMatchRegex(root, "Content", inputMatchHtml, strPatton, ref strContent);

            NoHTML(ref strContent);
            

            #endregion

            #region 得到 Title
            GetMatchRegex(root, "Title", inputMatchHtml,"", ref strTitle);
            NoHTML(ref strTitle);
            strPatton = "<.+?>";
            strTitle = Regex.Replace(strTitle, strPatton, new MatchEvaluator(CapText), RegexOptions.Singleline | RegexOptions.IgnoreCase);
            strTitle = strTitle.Trim();
            if (strTitle.Length == 0 || strTitle.CompareTo("") == 0)
            {
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, "title is null!");
                bInsertedError = true;
                return;
            }

            #endregion

            #region 得到ContentDate
            //strContentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            GetMatchRegex(root, "ContentDate", inputMatchHtml, "", ref strContentDate);
            strContentDate = strContentDate == "" ? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") : strContentDate;
            #endregion

            #region 得到Author
            strPatton = @">(.+?)</a>";
            GetMatchRegex(root, "Author", inputMatchHtml, strPatton, ref strAuthor);
            #endregion

        }

        #endregion

        #region  新浪微博


        /// <summary>
        /// 得到 新浪微博的各个参数
        /// </summary>
        /// <param name="root">xml模版</param>
        /// <param name="inputMatchHtml">待匹配的html</param>
        /// <param name="strSrcUrl">返回的 微博作者地址</param>
        /// <param name="strContent">微博内容</param>
        /// <param name="strAuthor">微博作者</param>
        /// <param name="strContentDate">微博发表时间</param>
        public void GetMicrobloggings(XmlNode root,string inputMatchHtml,ref string strSrcUrl,ref string strContent,
            ref string strAuthor,ref string strContentDate)
        {
            //SrcUrl, Content,Author, CreateDate, SiteId, ContentDate, projectid
            /*过程: 得到节点 匹配节点 判断匹配结果 返回*/
            //System.Threading.Thread.Sleep(3000);
            if (inputMatchHtml.Trim().Length == 0) return;
            inputMatchHtml = Regex.Replace(inputMatchHtml, @"\r\n|\n|\t", "", RegexOptions.IgnoreCase);
            string strPatton = "";
            #region 得到 URL
               strPatton = "href=\"(.+?)\"";
               GetMatchRegex(root, "SrcUrl", inputMatchHtml, strPatton, ref strSrcUrl);
            #endregion

            #region 得到Content
               strPatton = "";
               GetMatchRegex(root, "Content", inputMatchHtml, strPatton, ref strContent);
               NoHTML(ref strContent);
            #endregion

            #region 得到作者Author
               strPatton = "title=\"(.+?)\"";
               GetMatchRegex(root, "Author", inputMatchHtml, strPatton, ref strAuthor);
            #endregion

            #region 得到 微博时间ContentDate
               strPatton = "title=\"(.+?)\"";
               GetMatchRegex(root, "ContentDate", inputMatchHtml, strPatton, ref strContentDate);
            #endregion
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
        public string GetMicrobloggingsSQL(out SqlParameter[] parms, string strSrcUrl, string strContent, string strAuthor,
            string strSiteID, string strContentDate, string strProjectID)
        {
                #region 类型为 新浪微博 microbloggings
                string sqlStr = "INSERT INTO microbloggings (SrcUrl, Content, Author, CreateDate, SiteId, ContentDate, projectid,Title) VALUES (" +
                                          "@srcUrl, @content, @author, @createdate, @siteId, @contentDate, @projectId,@title)";
                parms = new SqlParameter[8];
                string[] pa = new string[] { strSrcUrl, strContent, strAuthor, strSiteID, strContentDate, strProjectID };
                parms[0] = new SqlParameter("@srcUrl", strSrcUrl.CompareTo("") == 0 ? Convert.DBNull : strSrcUrl);
                parms[1] = new SqlParameter("@content", strContent);
                parms[2] = new SqlParameter("@author", strAuthor.CompareTo("") == 0 ? Convert.DBNull : strAuthor);
                parms[3] = new SqlParameter("@createdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                parms[4] = new SqlParameter("@siteId", strSiteID);
                DateTime date = new DateTime();
                parms[5] = new SqlParameter("@contentDate", strContentDate.CompareTo("") == 0 ? Convert.DBNull : DateTime.TryParse(strContentDate, out date) ? strContentDate : strContentDate);
                parms[6] = new SqlParameter("@projectId", strProjectID.CompareTo("") == 0 ? Convert.DBNull : strProjectID);
                //补充 新浪微博比较特殊 title = Content
                parms[7] = new SqlParameter("@title", strContent.CompareTo("") == 0 ? Convert.DBNull : strContent);
            return sqlStr;
                #endregion
                

        }

        #endregion

        #region news

        /// <summary>
        /// 得到 news 的各个参数
        /// </summary>
        /// <param name="root">模板</param>
        /// <param name="inputMatchHtml">待匹配的内容</param>
        /// <param name="strSrcUrl">站点url</param>
        /// <param name="strContent">内容</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strContentDate">内容时间</param>
        /// <param name="strSiteID">站点ID</param>
        /// <param name="strSite">站点</param>
        /// <param name="bInsertedError">插入错误信息是否成功</param>
        public void GetNews(XmlNode root, string inputMatchHtml, ref string strSrcUrl, ref string strContent, ref string strTitle, ref string strContentDate,
            string strSiteID, string strSite, ref bool bInsertedError, ref string strSiteEntry, int ipos, string urlMatch,string strEncoding)
        {
            //SrcUrl,Title,Content,ContentDate
            if (inputMatchHtml.Trim().Length == 0) return;
            string strPatton = "";
           
            #region 得到 URL
            GetMatchRegex(root, "SrcUrl", inputMatchHtml, strPatton, ref strSrcUrl);
            #region 得到 URL 以后 做处理
            strSrcUrl = GetURL(strSrcUrl, strSiteEntry, ipos);

            #endregion

            #endregion
            //得到URL 以后 要判断路径

            #region 得到Content
            string strGetEncoding = _hh.GetEncoding(strSrcUrl);
            if (strGetEncoding.Length == 0)
            {
                InsertErrorList(strSiteID, strSite, strSrcUrl, "Get Encoding Error!");
                return;
            }
            strEncoding = strGetEncoding;
            //System.Threading.Thread.Sleep(3000);

            bool bRev = _hh.Open(strSrcUrl, Encoding.GetEncoding(strEncoding), ref strContent);
            if (bRev == false)
            {
                if (strContent.IndexOf("超时") >= 0)
                    if (strContent.IndexOf("超时") >= 0 || strContent.IndexOf("time out") >= 0 || strContent.IndexOf("远程主机强迫") >= 0
                        || strContent.IndexOf("连接被意外关闭") >= 0 || strContent.IndexOf("500") >= 0 || strContent.IndexOf("403") >= 0)
                        return;
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, strContent);
                bInsertedError = true;
                return;
            }
            else if (strContent.CompareTo("") == 0)
            {
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, "Content data is 404!");
                bInsertedError = true;
                return;
            }
            NoHTML(ref strContent);


            #endregion 

            #region 得到 Title
           
            GetMatchRegex(root, "Title", inputMatchHtml, "", ref strTitle);
            strPatton = "<.+?>";
            strTitle = Regex.Replace(strTitle, strPatton, new MatchEvaluator(CapText), RegexOptions.Singleline | RegexOptions.IgnoreCase);
            strTitle = strTitle.Trim();
            if (strTitle.Length == 0 || strTitle.CompareTo("") == 0)
            {
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, "title is null!");
                bInsertedError = true;
                return;
            }

            #endregion

            #region 得到ContentDate
            strContentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            #endregion

        }

        /// <summary>
        /// 组装 新闻 News 的 参数
        /// </summary>
        /// <param name="parms">参数列表</param>
        /// <param name="strSrcUrl">url</param>
        /// <param name="strContent">内容</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strSiteID">站点Id</param>
        /// <param name="strContentDate">内容时间</param>
        /// <param name="strProjectID">项目Id</param>
        /// <param name="strClassName">分类名 为 news 或者是 newsdata</param>
        /// <returns></returns>
        public string GetNewsSQL(out SqlParameter[] parms, string strSrcUrl, string strContent, string strTitle, string strSiteID, string strContentDate, string strProjectID, string strClassName)
        {
            //SrcUrl,Title,Content,ContentDate,SiteId,projectId
            #region 类型为 新闻 news 或者是 newsdata
            string sqlStr = "INSERT INTO "+ strClassName +" (SrcUrl, Content, Title, CreateDate, SiteId, ContentDate, projectid) VALUES (" +
                                       "@SrcUrl, @Content, @Title, @CreateDate, @SiteId, @ContentDate, @projectid)";
            parms = new SqlParameter[7];
            parms[0] = new SqlParameter("@SrcUrl", strSrcUrl.CompareTo("") == 0 ? Convert.DBNull : strSrcUrl);
            parms[1] = new SqlParameter("@Content", strContent);
            parms[2] = new SqlParameter("@Title", strTitle.CompareTo("") == 0 ? Convert.DBNull : strTitle);
            parms[3] = new SqlParameter("@CreateDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            parms[4] = new SqlParameter("@SiteId", strSiteID);
            DateTime date = new DateTime();
            parms[5] = new SqlParameter("@ContentDate", strContentDate.CompareTo("") == 0 ? Convert.DBNull : DateTime.TryParse(strContentDate, out date) ? strContentDate : strContentDate);
            parms[6] = new SqlParameter("@projectid", strProjectID.CompareTo("") == 0 ? Convert.DBNull : strProjectID);
            #endregion
            return sqlStr;
        }

        #endregion

        #region  论坛

        /// <summary>
        /// 组装 论坛 forums  
        /// </summary>
        /// <param name="parms">参数 列表</param>
        /// <param name="strSrcUrl">url地址</param>
        /// <param name="strContent">内容</param>
        /// <param name="strTitle">标题</param>
        /// <param name="strAuthor">作者</param>
        /// <param name="strReplies">回复</param>
        /// <param name="strViews">视图</param>
        /// <param name="strSiteID">站点Id</param>
        /// <param name="strContentDate">内容时间</param>
        /// <param name="strProjectID">项目Id</param>
        /// <param name="strClassName">分类名</param>
        /// <returns></returns>
        public string GetForumsSQL(out SqlParameter[] parms, string strSrcUrl, string strContent, string strTitle,string strAuthor,
            string strReplies,string strViews, string strSiteID, string strContentDate, string strProjectID, string strClassName)
        {
            #region 类型为 论坛 forums
           string sqlStr = "INSERT INTO " + strClassName + " (SrcUrl, Content, Title, Author, CreateDate, Replies, Views, SiteId, ContentDate, projectid) VALUES (" +
                        "@SrcUrl, @Content, @Title, @Author, @CreateDate, @Replies, @Views, @SiteId, @ContentDate, @projectid)";

            parms = new SqlParameter[10];
            parms[0] = new SqlParameter("@SrcUrl", strSrcUrl.CompareTo("") == 0 ? Convert.DBNull : strSrcUrl);
            parms[1] = new SqlParameter("@Content", strContent);
            parms[2] = new SqlParameter("@Title", strTitle.CompareTo("") == 0 ? Convert.DBNull : strTitle);
            parms[3] = new SqlParameter("@Author", strAuthor.CompareTo("") == 0 ? Convert.DBNull : strAuthor);
            parms[4] = new SqlParameter("@CreateDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            parms[5] = new SqlParameter("@Replies", strReplies.CompareTo("") == 0 ? Convert.DBNull : strReplies);
            parms[6] = new SqlParameter("@Views", strViews.CompareTo("") == 0 ? Convert.DBNull : strViews);
            parms[7] = new SqlParameter("@SiteId", strSiteID);
            DateTime date = new DateTime();
            parms[8] = new SqlParameter("@ContentDate", strContentDate.CompareTo("") == 0 ? Convert.DBNull : DateTime.TryParse(strContentDate, out date) ? strContentDate : strContentDate);
            parms[9] = new SqlParameter("@projectid", strProjectID.CompareTo("") == 0 ? Convert.DBNull : strProjectID);

            return sqlStr;
            #endregion
        }

        public void GetForums(XmlNode root, string inputMatchHtml, ref string strSrcUrl, ref string strContent, ref string strTitle, 
            ref string strContentDate, ref string strAuthor,ref string strReplies,ref string strViews,
            string strSiteID, string strSite, ref bool bInsertedError, ref string strSiteEntry, int ipos, string urlMatch, string strEncoding)
        {
            //SrcUrl,Content,Title,Author,Views,Replies,ContentDate,

            string strPatton = "";

            #region  得到 URL
            GetMatchRegex(root, "SrcUrl", inputMatchHtml, strPatton, ref strSrcUrl);
            #endregion

            #region 得到 URL 以后 做处理
           // strSrcUrl = GetURL(strSrcUrl, strSiteEntry, ipos);

            #endregion

            #region  得到 Content
            strPatton = "";
            GetMatchRegex(root, "Content", inputMatchHtml, strPatton, ref strContent);
           /*
            bool bRev = _hh.Open(strSrcUrl, Encoding.GetEncoding(strEncoding), ref strContent);
            if (bRev == false)
            {
                if (strContent.IndexOf("超时") >= 0)
                    if (strContent.IndexOf("超时") >= 0 || strContent.IndexOf("time out") >= 0 || strContent.IndexOf("远程主机强迫") >= 0
                        || strContent.IndexOf("连接被意外关闭") >= 0 || strContent.IndexOf("500") >= 0 || strContent.IndexOf("403") >= 0)
                        return;
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, strContent);
                bInsertedError = true;
                return;
            }
            else if (strContent.CompareTo("") == 0)
            {
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, "Content data is 404!");
                bInsertedError = true;
                return;
            }
            * */
            NoHTML(ref strContent);

            #endregion

            #region 得到Author
            strPatton = "";
            GetMatchRegex(root, "Author", inputMatchHtml, strPatton, ref strAuthor);
            strAuthor = strAuthor.Substring(strAuthor.IndexOf('>') + 1);
            #endregion

            #region  得到 Title


            GetMatchRegex(root, "Title", inputMatchHtml, "", ref strTitle);
            strPatton = "<.+?>";
            strTitle = Regex.Replace(strTitle, strPatton, new MatchEvaluator(CapText), RegexOptions.Singleline | RegexOptions.IgnoreCase);
            strTitle = strTitle.Substring(strTitle.IndexOf('>') + 1);
            strTitle = strTitle.Trim();
            if (strTitle.Length == 0 || strTitle.CompareTo("") == 0)
            {
                if (!bInsertedError)
                    InsertErrorList(strSiteID, strSite, strSrcUrl, "title is null!");
                bInsertedError = true;
                return;
            }
            #endregion

            #region  得到 Views
            strPatton = "";
            GetMatchRegex(root, "Views", inputMatchHtml, strPatton, ref strViews);
            strViews = strViews.IndexOf('/') >= 0 ? strViews.Substring(strViews.IndexOf('/') + 1) : strViews;
            if (strReplies.CompareTo("") != 0)
            {
                try { Convert.ToInt32(strViews); }
                catch (Exception e) { strViews = ""; }
            }
           
            #endregion

            #region  得到 Replies
            strPatton = "";
            GetMatchRegex(root, "Replies", inputMatchHtml, strPatton, ref strReplies);
            strReplies = strReplies.IndexOf('/') >= 0 ? strReplies.Substring(0, strReplies.IndexOf('/')) : strReplies;
            if (strReplies.CompareTo("") != 0)
            {
                try { Convert.ToInt32(strReplies); }
                catch (Exception e) { strReplies = "";}
            }
            #endregion

            #region  得到  ContentDate

            strPatton = @"\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}";
            GetMatchRegex(root, "ContentDate", inputMatchHtml, strPatton, ref strContentDate);
            strContentDate = strContentDate == "" ? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") : strContentDate;
            #endregion

        }
        #endregion
        #endregion
        /*
         * return:
         *          1  成功;
         *          0  超时;
         *          -1 其他错误；
        */
        //得到 一页的数据
        private int GetContentOnePage(XmlNode root, string strSite, string strSiteID, string strEncoding, string strClassName, string strProjectID)
        {
           
            int microbloggings = 0;
            if (strClassName.CompareTo("microbloggings") == 0)
            {
                //GetContentOnePageChange 这个方法 目前 只做 新浪微博的爬取功能
                microbloggings =GetContentOnePageChange(root, strSite, strSiteID, strEncoding, strClassName, strProjectID);
            }
            if (microbloggings > 0){ return 1;}


            bool bInsertedError = false;
            XmlNode node = root.SelectSingleNode("Node");//查找<Node>     
            string pattern = node.FirstChild.Value;


            string strHTML = "";
            bool rev = _hh.Open(strSite, Encoding.GetEncoding(strEncoding), ref strHTML);

            if (!rev)
            {
                //if (strHTML.IndexOf("超时") >= 0)
                if (strHTML.IndexOf("超时") >= 0 || strHTML.IndexOf("time out") >= 0 || strHTML.IndexOf("远程主机强迫") >= 0
                    || strHTML.IndexOf("连接被意外关闭") >= 0 || strHTML.IndexOf("500") >= 0 || strHTML.IndexOf("403") >= 0)
                    return 0;
                else
                {
                    InsertErrorList(strSiteID, strSite, "", strHTML);
                    return 0;
                }
            }

            Regex reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(strHTML);
           
            //这里是关键 点
            //测试 这里让 它匹配永远成立 原来 : mc.Count > 1 



            if (mc.Count > 1)
            {
                #region  判断mc.Count > 1
                string strSiteEntry = "";
                /*
                string Path = Environment.TickCount.ToString() + ".txt";
                Regex reg1 = new Regex("http://(.+?)/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection mc1 = reg1.Matches(strSite);
                if (mc1.Count > 0)
                {
                    strSiteEntry = "http://" + mc1[0].Groups[1].Value + "/";
                    Path = mc1[0].Groups[1].Value + ".txt";
                }
                 */
                int ipos = strSite.LastIndexOf('/');
                if (ipos == 6)
                    strSiteEntry = strSite + "/";
                else
                    strSiteEntry = strSite.Substring(0, ipos) + "/";

                //得到 站点 的地址 
                /*
                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();

                System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, false, System.Text.Encoding.GetEncoding("gb2312"));
                */
                //f2.WriteLine("total " + mc.Count + " nodes! ");
                //f2.WriteLine("-------------------------------------");
                string strSrcUrl = "", strContent = "", strTitle = "", strAuthor = "",
                        strCreateDate = "", strReplies = "", strViews = "", strContentDate = "";

                #endregion

                #region 遍历循环 foreach (Match m in mc)
                foreach (Match m in mc)
                {

                   // System.Threading.Thread.Sleep(3000);
                    //[] charContent = new char[1];
                    strSrcUrl = ""; strContent = ""; strTitle = ""; strAuthor = "";
                    strCreateDate = ""; strReplies = ""; strViews = ""; strContentDate = ""; 

                    if (m.Groups[1].Value.Trim().Length == 0) continue;

                    XmlNode cnode = root.SelectSingleNode("SrcUrl");
                    Regex reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    MatchCollection mc2 = reg2.Matches(m.Groups[1].Value);
                    if (mc2.Count > 0) //url 查询搜索条目
                    {
                        //Console.WriteLine(mc2[0].Groups[1].Value);
                        //f2.WriteLine("url: " + mc2[0].Groups[1].Value);
                        //sql += "'" + mc2[0].Groups[1].Value + "'";
                        //strSrcUrl = mc2[0].Groups[1].Value;
                        // 变换路径不支持
                        if (mc2[0].Groups[1].Value.IndexOf("http://") != -1) //全路径
                            strSrcUrl = mc2[0].Groups[1].Value;
                        else
                        {
                            if (mc2[0].Groups[1].Value.IndexOf('/') == 0)
                                if (ipos == 6) 
                                    strSrcUrl = strSiteEntry + mc2[0].Groups[1].Value.Substring(1);
                                else
                                {
                                    if (mc2[0].Groups[1].Value.Substring(1).IndexOf('/') < 0) //不是相对路径
                                    {
                                        strSrcUrl = strSiteEntry + mc2[0].Groups[1].Value.Substring(1);
                                    }
                                    else //相对路径
                                    {
                                        int i = strSiteEntry.Substring(7).IndexOf('/');
                                        if (i >= 0) //得到root
                                        {
                                            strSrcUrl = strSiteEntry.Substring(0, i + 7) + "/" + mc2[0].Groups[1].Value.Substring(1);
                                        }
                                        else
                                            strSrcUrl = strSiteEntry + mc2[0].Groups[1].Value.Substring(1);
                                    }
                                }
                            else if (mc2[0].Groups[1].Value.IndexOf("../") == 0)
                            {
                                strSiteEntry = strSiteEntry.Substring(0, strSiteEntry.Length - 1);
                                int count = Regex.Matches(mc2[0].Groups[1].Value, "../").Count;
                                for (int l = 0; l < count; l++)
                                {
                                    strSiteEntry = strSiteEntry.Substring(0, strSiteEntry.LastIndexOf('/'));
                                }
                                strSiteEntry = strSiteEntry + "/";
                                strSrcUrl = strSiteEntry + mc2[0].Groups[1].Value.Replace("../", "");
                            }
                            else
                            {
                                if (mc2[0].Groups[1].Value.IndexOf('/') > 0)
                                {
                                    int i = strSiteEntry.Substring(7).IndexOf('/');

                                    strSrcUrl = strSiteEntry.Substring(0, i + 7) + "/" + mc2[0].Groups[1].Value;
                                }
                                else
                                    strSrcUrl = strSiteEntry + mc2[0].Groups[1].Value;
                            }
                        }

                        if (strClassName.CompareTo("news") == 0 || strClassName.CompareTo("blogs") == 0)
                        {
                            String strGetEncoding = _hh.GetEncoding(strSrcUrl);
                            if (strGetEncoding.Length == 0)
                            {
                                InsertErrorList(strSiteID, strSite, strSrcUrl, "Get Encoding Error!");
                                continue;
                            }
                            strEncoding = strGetEncoding;

                           // System.Threading.Thread.Sleep(3000);
                        }
                        
                        bool bRev = _hh.Open(strSrcUrl, Encoding.GetEncoding(strEncoding), ref strContent);
                        #region 超时判断
                        if (bRev == false)
                        {
                            //if (strContent.IndexOf("超时") >= 0)
                            if (strContent.IndexOf("超时") >= 0 || strContent.IndexOf("time out") >= 0 || strContent.IndexOf("远程主机强迫") >= 0
                                || strContent.IndexOf("连接被意外关闭") >= 0 || strContent.IndexOf("500") >= 0 || strContent.IndexOf("403") >= 0)
                                continue;
                            if (!bInsertedError)
                                InsertErrorList(strSiteID, strSite, strSrcUrl, strContent);
                            bInsertedError = true;
                            continue;
                        }
                        #endregion
                        else if (strContent.CompareTo("") == 0)
                        {
                            if (!bInsertedError)
                                InsertErrorList(strSiteID, strSite, strSrcUrl, "Content data is 404!");
                            bInsertedError = true;
                            continue;
                        }

                        NoHTML(ref strContent);
                        //charContent = strContent.ToCharArray();
                        //sql += "',@charContent";

                    }
                    else
                        continue;
                #endregion
                    cnode = root.SelectSingleNode("Title");//class="font14_hei_210a.+?>(.+?)<
                    reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    mc2 = reg2.Matches(m.Groups[1].Value);
                    if (mc2.Count > 0) //title
                    {
                        //Console.WriteLine(mc2[0].Groups[1].Value);
                        //f2.WriteLine("title: " + mc2[0].Groups[1].Value.Trim());
                        //sql += ",'" + mc2[0].Groups[1].Value.Trim() + "'";

                        string tmppattern1 = "<.+?>";
                        //reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        strTitle = Regex.Replace(mc2[0].Groups[1].Value.Trim(), tmppattern1, new MatchEvaluator(CapText), RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        strTitle = strTitle.Trim();

                        if (strTitle.Length == 0 || strTitle.CompareTo("") == 0)
                        {
                            if (!bInsertedError)
                                InsertErrorList(strSiteID, strSite, strSrcUrl, "title is null!");
                            bInsertedError = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (!bInsertedError)
                            InsertErrorList(strSiteID, strSite, strSrcUrl, "title is null!");
                        bInsertedError = true;
                        continue;
                    }

                    cnode = root.SelectSingleNode("Author");
                    if (cnode != null && cnode.FirstChild != null)
                    {
                        reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        mc2 = reg2.Matches(m.Groups[1].Value);
                        if (mc2.Count > 0) //author 
                        {
                            //Console.WriteLine(mc2[0].Groups[1].Value);
                            //f2.WriteLine("author: " + mc2[0].Groups[1].Value.Trim());
                            //sql += ",'" + mc2[0].Groups[1].Value.Trim() + "'";
                            strAuthor = mc2[0].Groups[1].Value.Trim();
                        }
                    }
                    #region 这里是注释
                    /*
                    cnode = root.SelectSingleNode("CreateDate");
                    if (cnode != null && cnode.FirstChild != null)
                    {
                        reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        mc2 = reg2.Matches(m.Groups[1].Value);
                        if (mc2.Count > 0) // create date
                        {
                            //Console.WriteLine(mc2[0].Groups[1].Value);
                            //f2.WriteLine("create date: " + mc2[0].Groups[1].Value.TrimEnd().TrimStart());
                            sql += ",'" + mc2[0].Groups[1].Value.TrimEnd().TrimStart() + "'";

                            if (mc2[0].Groups[1].Value.TrimEnd().TrimStart().Length < 12)
                            {
                                strCreateDate = "";
                            }
                            else
                            {
                                try
                                {
                                    strCreateDate = mc2[0].Groups[1].Value.TrimEnd().TrimStart();
                                }
                                catch (Exception e)
                                {
                                    strCreateDate = "";
                                }
                            }
                        }
                    }
                    */

                    #endregion
                    strCreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    #region 这里是注释
                    /*
                    cnode = root.SelectSingleNode("Replies");
                    reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    //reg2 = new Regex("\"i5\"\\s+nowrap>.+?>(.+?)<", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    mc2 = reg2.Matches(m.Groups[1].Value);
                    if (mc2.Count > 0) //reply and view
                    {
                        string strTmp = mc2[0].Groups[1].Value.Trim();
                        string strReply = strTmp.Substring(0, strTmp.IndexOf('/'));
                        string strView = strTmp.Substring(strTmp.IndexOf('/')+1);

                        //f2.WriteLine("replys: " + strReply);
                        //f2.WriteLine("views: " + strView);

                        ////Console.WriteLine(mc2[0].Groups[1].Value);
                    }
                     */
                    #endregion
                    cnode = root.SelectSingleNode("Replies");

                    if (cnode != null && cnode.FirstChild != null)
                    {
                        reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        mc2 = reg2.Matches(m.Groups[1].Value);
                        if (mc2.Count > 0) //reply and view
                        {
                            //f2.WriteLine("replys: " + mc2[0].Groups[1].Value.Trim());
                            string strTmp = mc2[0].Groups[1].Value.Trim();

                            if (strTmp.IndexOf('/') >= 0)
                            {
                                string strReply = strTmp.Substring(0, strTmp.IndexOf('/'));
                                //sql += "," + int.Parse(strReply);
                                strReplies = strReply;
                            }
                            else
                            {
                                //sql += "," + int.Parse(mc2[0].Groups[1].Value.TrimEnd().TrimStart());
                                strReplies = mc2[0].Groups[1].Value.TrimEnd().TrimStart();
                            }
                        }
                    }
                    if (strReplies.CompareTo("") != 0)
                    {
                        try
                        {
                            Convert.ToInt32(strReplies);
                        }
                        catch (Exception e)
                        {
                            strReplies = "";
                        }
                    }

                    cnode = root.SelectSingleNode("Views");
                    if (cnode != null && cnode.FirstChild != null)
                    {
                        reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        mc2 = reg2.Matches(m.Groups[1].Value);
                        if (mc2.Count > 0) //reply and view
                        {
                            //f2.WriteLine("Views: " + mc2[0].Groups[1].Value.Trim());
                            string strTmp = mc2[0].Groups[1].Value.Trim();
                            if (strTmp.IndexOf('/') >= 0)
                            {
                                string strView = strTmp.Substring(strTmp.IndexOf('/') + 1);
                                
                                strViews = strView;
                            }
                            else
                            {
                                
                                strViews = mc2[0].Groups[1].Value.Trim();
                            }
                        }
                    }
                    if (strViews.CompareTo("") != 0)
                    {
                        try
                        {
                            Convert.ToInt32(strViews);
                        }
                        catch (Exception e)
                        {
                            strViews = "";
                        }
                    }

                    #region 这里是注释
                    /*
                    cnode = root.SelectSingleNode("ContentDate");
                    if (cnode != null && cnode.FirstChild != null)
                    {
                        reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        mc2 = reg2.Matches(m.Groups[1].Value);
                        if (mc2.Count > 0) //reply date
                        {
                            //Console.WriteLine(mc2[0].Groups[1].Value);

                            //f2.WriteLine("reply date: " + mc2[0].Groups[1].Value.TrimEnd().TrimStart());
                            sql += ",'" + mc2[0].Groups[1].Value.TrimEnd().TrimStart() + "'";

                            if (mc2[0].Groups[1].Value.TrimEnd().TrimStart().Length < 12)
                                strContentDate = "";
                            else
                            {
                                try
                                {
                                    strContentDate = mc2[0].Groups[1].Value.TrimEnd().TrimStart();
                                }
                                catch (Exception e)
                                {
                                    strContentDate = "";
                                }
                            }
                        }
                    }
                    */
              #endregion

                    strContentDate = strCreateDate;
                    //sql += ", " + strSiteID + ")";

                    if (strContent.CompareTo("") == 0 || strSiteID.CompareTo("") == 0) 
                        continue;
                    //Mysql mysql = new Mysql(GlobalVars.MysqlConn.StrConnection);
                    Mysql mysql = GlobalVars.ConnectionPoolVars.GConnectionPool.GetConnection();


                    lock (mysql)
                    {
                       
                        try
                        {
                            #region 插入数据到数据库
                            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from " + strClassName + " LIMIT 0 , 1", mysql.GetConnection());
                            //SqlCommand cmd = new SqlCommand(("select * from " + strClassName + " LIMIT 0 , 1", mysql.GetConnection());
                            //cmd.ExecuteNonQuery();
                            SqlCommand cmd = new SqlCommand();
                            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                            cmd.Connection = mysql.GetConnection();
                            cmd.CommandTimeout = 120;
                         
                            if (strClassName.CompareTo("forums") == 0 || strClassName.CompareTo("baiduforum") == 0)
                            {
                                #region 类型为 论坛 forums
                                cmd.CommandText = "INSERT INTO " + strClassName + " (SrcUrl, Content, Title, Author, CreateDate, Replies, Views, SiteId, ContentDate, projectid) VALUES (" +
                                    "@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)";
                                                  //"?p1, ?p2, ?p3, ?p4, ?p5, ?p6, ?p7, ?p8, ?p9, ?p10)";
                                if (strSrcUrl.CompareTo("") == 0)
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p1", null));
                                    //cmd.Parameters.Add(new SqlParameter("@p1", null));
                                    cmd.Parameters.AddWithValue("@p1", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p1", strSrcUrl);
                                    //cmd.Parameters.Add(new SqlParameter("@p1", strSrcUrl));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p1", strSrcUrl));

                                //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p2", strContent));
                                //cmd.Parameters.Add(new SqlParameter("@p2", strContent));
                                cmd.Parameters.AddWithValue("@p2", strContent);

                                if (strTitle.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p3", Convert.DBNull);
                                    //cmd.Parameters.Add(new SqlParameter("@p3", null));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p3", null));
                                else
                                {
                                    /*
                                    byte[] source = Encoding.Default.GetBytes(strTitle);
                                    byte[] utf8 = Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.GetEncoding("UTF-8"), source);
                                    string d = Encoding.UTF8.GetString(utf8);
                                    */
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p3", strTitle));
                                    //cmd.Parameters.Add(new SqlParameter("@p3", strTitle));
                                    cmd.Parameters.AddWithValue("@p3", strTitle);
                                }

                                if (strAuthor.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p4", Convert.DBNull);        
                                    //cmd.Parameters.Add(new SqlParameter("@p4", null));        
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p4", null));
                                else
                                    cmd.Parameters.AddWithValue("@p4", strAuthor);
                                    //cmd.Parameters.Add(new SqlParameter("@p4", strAuthor));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p4", strAuthor));

                                if (strCreateDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p5", Convert.DBNull);
                                    //cmd.Parameters.Add(new SqlParameter("@p5", null));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p5", null));
                                else
                                {
                                    if (strCreateDate.Length <= 6)
                                        strCreateDate = DateTime.Now.Year + "-" + strCreateDate;
                                    cmd.Parameters.AddWithValue("@p5", strCreateDate);
                                    //cmd.Parameters.Add(new SqlParameter("@p5", strCreateDate));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p5", strCreateDate));
                                }

                                if (strReplies.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p6", Convert.DBNull);
                                    //cmd.Parameters.Add(new SqlParameter("@p6", null));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p6", null));
                                else
                                    cmd.Parameters.AddWithValue("@p6", strReplies);
                                    //cmd.Parameters.Add(new SqlParameter("@p6", strReplies));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p6", strReplies));

                                if (strViews.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p7", Convert.DBNull);    
                                    //cmd.Parameters.Add(new SqlParameter("@p7", null));    
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p7", null));
                                else
                                    cmd.Parameters.AddWithValue("@p7", strViews);
                                    //cmd.Parameters.Add(new SqlParameter("@p7", strViews));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p7", strViews));

                                //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p8", strSiteID));
                                //cmd.Parameters.Add(new SqlParameter("@p8", strSiteID));
                                cmd.Parameters.AddWithValue("@p8", strSiteID);

                                if (strContentDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p9", Convert.DBNull);
                                    //cmd.Parameters.Add(new SqlParameter("@p9", null));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p9", null));
                                else
                                {
                                    DateTime date = new DateTime();
                                    if (DateTime.TryParse(strContentDate, out date))
                                        cmd.Parameters.AddWithValue("@p9", strContentDate);
                                        //cmd.Parameters.Add(new SqlParameter("@p9", strContentDate));
                                        //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p9", strContentDate));
                                    else
                                        cmd.Parameters.AddWithValue("@p9", Convert.DBNull);
                                        //cmd.Parameters.Add(new SqlParameter("@p9", null));
                                        //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p9", null));
                                }
                                if (strProjectID.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p10", Convert.DBNull);
                                    //cmd.Parameters.Add(new SqlParameter("@p10", null));
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p10", null));
                                else
                                    cmd.Parameters.AddWithValue("@p10", strProjectID);
                                    //cmd.Parameters.Add(new SqlParameter("@p10", strProjectID));
                                //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?p10", strProjectID));

                                #endregion
                            }
                            else if (strClassName.CompareTo("news") == 0)
                            {
                                #region 类型为 新闻 news
                                cmd.CommandText = "INSERT INTO " + strClassName + " (SrcUrl, Content, Title, CreateDate, SiteId, ContentDate, projectid) VALUES (" +
                                                  "@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
                                if (strSrcUrl.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p1", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p1", strSrcUrl);
                                    //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@p1", strSrcUrl));

                                cmd.Parameters.AddWithValue("@p2", strContent);

                                if (strTitle.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p3", Convert.DBNull);
                                else
                                {
                                    /*
                                    byte[] source = Encoding.Default.GetBytes(strTitle);
                                    byte[] utf8 = Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.GetEncoding("UTF-8"), source);
                                    string d = Encoding.UTF8.GetString(utf8);
                                    */
                                    cmd.Parameters.AddWithValue("@p3", strTitle);
                                }

                                if (strCreateDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p4", Convert.DBNull);
                                else
                                {
                                    if (strCreateDate.Length <= 6)
                                        strCreateDate = DateTime.Now.Year + "-" + strCreateDate;

                                    cmd.Parameters.AddWithValue("@p4", strCreateDate);
                                }

                                cmd.Parameters.AddWithValue("@p5", strSiteID);

                                if (strContentDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p6", Convert.DBNull);
                                else
                                {
                                    DateTime date = new DateTime();
                                    if (DateTime.TryParse(strContentDate, out date))
                                        cmd.Parameters.AddWithValue("@p6", strContentDate);
                                    else
                                        cmd.Parameters.AddWithValue("@p6", Convert.DBNull);
                                }


                                if (strProjectID.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p7", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p7", strProjectID);

                                #endregion
                            }
                            else if (strClassName.CompareTo("newsdata") == 0)
                            {
                                #region 类型为 新闻数据集 newsdata
                                cmd.CommandText = "INSERT INTO " + strClassName + " (SrcUrl, Content, Title, CreateDate, SiteId, ContentDate, projectid) VALUES (" +
                                                  "@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
                                if (strSrcUrl.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p1", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p1", strSrcUrl);
                                //cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@p1", strSrcUrl));

                                cmd.Parameters.AddWithValue("@p2", strContent);

                                if (strTitle.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p3", Convert.DBNull);
                                else
                                {
                                    /*
                                    byte[] source = Encoding.Default.GetBytes(strTitle);
                                    byte[] utf8 = Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.GetEncoding("UTF-8"), source);
                                    string d = Encoding.UTF8.GetString(utf8);
                                    */
                                    cmd.Parameters.AddWithValue("@p3", strTitle);
                                }

                                if (strCreateDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p4", Convert.DBNull);
                                else
                                {
                                    if (strCreateDate.Length <= 6)
                                        strCreateDate = DateTime.Now.Year + "-" + strCreateDate;

                                    cmd.Parameters.AddWithValue("@p4", strCreateDate);
                                }

                                cmd.Parameters.AddWithValue("@p5", strSiteID);

                                if (strContentDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p6", Convert.DBNull);
                                else
                                {
                                    DateTime date = new DateTime();
                                    if (DateTime.TryParse(strContentDate, out date))
                                        cmd.Parameters.AddWithValue("@p6", strContentDate);
                                    else
                                        cmd.Parameters.AddWithValue("@p6", Convert.DBNull);
                                }


                                if (strProjectID.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p7", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p7", strProjectID);


                                #endregion
                            }
                            else if (strClassName.CompareTo("microbloggings") == 0)
                            {
                                #region 类型为 微博表 Microbloggings
                                cmd.CommandText = "INSERT INTO " + strClassName + " (SrcUrl, Content, Author, CreateDate, SiteId, ContentDate, porjectid) VALUES (" +
                                                  "@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
                                if (strSrcUrl.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p1", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p1", strSrcUrl);

                                cmd.Parameters.AddWithValue("@p2", strContent);

                                if (strAuthor.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p3", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p3", strAuthor);

                                if (strCreateDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p4", Convert.DBNull);
                                else
                                {
                                    if (strCreateDate.Length <= 6)
                                        strCreateDate = DateTime.Now.Year + "-" + strCreateDate;

                                    cmd.Parameters.AddWithValue("@p4", strCreateDate);
                                }

                                cmd.Parameters.AddWithValue("@p5", strSiteID);

                                if (strContentDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p6", Convert.DBNull);
                                else
                                {
                                    DateTime date = new DateTime();
                                    if (DateTime.TryParse(strContentDate, out date))
                                        cmd.Parameters.AddWithValue("@p6", strContentDate);
                                    else
                                        cmd.Parameters.AddWithValue("@p6", Convert.DBNull);

                                }
                                if (strProjectID.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p7", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p7", strProjectID);

                                #endregion
                            }
                            else
                            {
                                #region 类型 为 博客表 Blogs

                                cmd.CommandText = "INSERT INTO " + strClassName + " (SrcUrl, Content, Title, Author, CreateDate, SiteId, ContentDate, projectid) VALUES (" +
                                                  "@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";
                                if (strSrcUrl.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p1", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p1", strSrcUrl);

                                cmd.Parameters.AddWithValue("@p2", strContent);

                                if (strTitle.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p3", Convert.DBNull);
                                else
                                {
                                    /*
                                    byte[] source = Encoding.Default.GetBytes(strTitle);
                                    byte[] utf8 = Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.GetEncoding("UTF-8"), source);
                                    string d = Encoding.UTF8.GetString(utf8);
                                    */
                                    cmd.Parameters.AddWithValue("@p3", strTitle);
                                }

                                if (strAuthor.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p4", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p4", strAuthor);

                                if (strCreateDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p5", Convert.DBNull);
                                else
                                {
                                    if (strCreateDate.Length <= 6)
                                        strCreateDate = DateTime.Now.Year + "-" + strCreateDate;

                                    cmd.Parameters.AddWithValue("@p5", strCreateDate);
                                }

                                cmd.Parameters.AddWithValue("@p6", strSiteID);

                                if (strContentDate.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p7", Convert.DBNull);
                                else
                                {
                                    DateTime date = new DateTime();
                                    if (DateTime.TryParse(strContentDate, out date))
                                        cmd.Parameters.AddWithValue("@p7", strContentDate);
                                    else
                                        cmd.Parameters.AddWithValue("@p7", Convert.DBNull);

                                }

                                if (strProjectID.CompareTo("") == 0)
                                    cmd.Parameters.AddWithValue("@p8", Convert.DBNull);
                                else
                                    cmd.Parameters.AddWithValue("@p8", strProjectID);


                                #endregion
                            }

                            int rows = cmd.ExecuteNonQuery();
                            //mysql.Disconnect();
                            mysql.isUsing = false;

                            #endregion
                        }
                        catch (Exception e)
                        {
                            #region 异常捕获
                            try
                            {
                                if (e.Message.IndexOf("事务在触") < 0)
                                {
                                    if (e.Message.IndexOf("事务(进程") < 0)
                                    {
                                        try
                                        {
                                            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from ErrorSite LIMIT 0 , 1", mysql.GetConnection());
                                            SqlCommand cmd = new SqlCommand();//new SqlCommand("select * from ErrorSite LIMIT 0 , 1", mysql.GetConnection());
                                            cmd.CommandTimeout = 60;
                                            //cmd.ExecuteNonQuery();
                                            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                                            cmd.Connection = mysql.GetConnection();
                                            cmd.CommandText = "INSERT INTO errorsite (siteid, siteurl, error_message, CrawlFinishDate) VALUES (@p1, @p2, @p3, @p4)";
                                            /*
                                            cmd.Parameters.Add(new SqlParameter("@p1", strSiteID));
                                            cmd.Parameters.Add(new SqlParameter("@p2", strSite));
                                            cmd.Parameters.Add(new SqlParameter("@p3", e.Message));
                                            */
                                            cmd.Parameters.AddWithValue("@p1", strSiteID);
                                            cmd.Parameters.AddWithValue("@p2", strSite);
                                            cmd.Parameters.AddWithValue("@p3", e.Message);
                                            string date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                                            cmd.Parameters.AddWithValue("@p4", date);
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception e1)
                                        {
                                        }
                                    }
                                    else //锁错误
                                        return 0;
                                }
                                
                                //mysql.Disconnect();
                            }
                            catch (Exception e1)
                            {
                                //mysql.Disconnect();
                            }
                            #endregion
                        }
                          
                        mysql.isUsing = false;
                    }
                    
                    //GlobalVars.MysqlConn.ConnMysql.ExecuteSQLQuery(sql);
                    //f2.WriteLine("-------------------------------------");
                }
                return 1;
                //f2.Close();
                //f2.Dispose();
            }
            else
            {
                nsGlobalOutput.Output.Write("Node has error siteid = " + strSiteID);

                InsertErrorList(strSiteID, strSite, "", "Node not found!");
                return 0;
            }
            //return -1;
        }

        public void InsertErrorList(string strSiteID, string strSite, string strNodeSite,string error_messgae)
        {
            //Mysql mysql = new Mysql(GlobalVars.MysqlConn.StrConnection);
            Mysql mysql = GlobalVars.ConnectionPoolVars.GConnectionPool.GetConnection();
            lock (mysql)
            {
                //Mysql mysql = new Mysql(GlobalVars.MysqlConn.StrConnection);
                try
                {
                    //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from ErrorSite LIMIT 0 , 1", mysql.GetConnection());
                    //SqlCommand cmd = new SqlCommand("select * from ErrorSite LIMIT 0 , 1", mysql.GetConnection());
                    SqlCommand cmd = new SqlCommand();
                    //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                    cmd.Connection = mysql.GetConnection();
                    cmd.CommandTimeout = 60;
                    //cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO errorsite (siteid, siteurl, nodeurl, error_message, CrawlFinishDate) VALUES (@p1, @p2, @p3, @p4, @p5)";

                    /*
                    cmd.Parameters.Add(new SqlParameter("@p1", strSiteID));
                    cmd.Parameters.Add(new SqlParameter("@p2", strSite));
                    if (strNodeSite.CompareTo("") ==0 )
                        cmd.Parameters.Add(new SqlParameter("@p3", Convert.DBNull));
                    else
                        cmd.Parameters.Add(new SqlParameter("@p3", strNodeSite));
                    cmd.Parameters.Add(new SqlParameter("@p4", error_messgae));
                    */
                    cmd.Parameters.AddWithValue("@p1", strSiteID);
                    cmd.Parameters.AddWithValue("@p2", strSite);
                    if (strNodeSite.CompareTo("") == 0)
                        cmd.Parameters.AddWithValue("@p3", Convert.DBNull);
                    else
                        cmd.Parameters.AddWithValue("@p3", strNodeSite);
                    cmd.Parameters.AddWithValue("@p4", error_messgae);
                    string date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                    cmd.Parameters.AddWithValue("@p5", date);
                    cmd.ExecuteNonQuery();
                    
                    //mysql.Disconnect();
                }
                catch (Exception e1)
                {
                    nsGlobalOutput.Output.Write("InsertErrorList error: " + e1.Message);    
                    mysql.isUsing = false;
                    //mysql.Disconnect();
                }
                mysql.isUsing = false;
            }
        }

        public void OptMainNode(ref XmlNode root, ref XmlNode nodeMainPage, ref string strSite, string strEncoing)
        {
            string pattern = nodeMainPage.FirstChild.Value;
            string strHTML = "";
            bool bRev = _hh.Open(strSite, Encoding.GetEncoding(strEncoing), ref strHTML);
            if (bRev == false) return;

            strHTML = Regex.Replace(strHTML, "<!--.+?-->", new MatchEvaluator(CapText), RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //strHTML = Regex.Replace(strHTML,"<!--.+?-->","");

            Regex reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mcMain = reg.Matches(strHTML);
            if (mcMain.Count > 0)
            {
                int forumindex = 0;

                foreach (Match mForum in mcMain)
                {
                    reg = new Regex(nodeMainPage.Attributes[0].Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    MatchCollection mc = reg.Matches(mForum.Groups[1].Value);
                    if (mc.Count == 0)
                        continue;

                    string fsite = "";
                    Regex reg1 = new Regex("(http://.+?)/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    MatchCollection mc1 = reg1.Matches(strSite);
                    if (mc1.Count > 0)
                    {
                        fsite = mc1[0].Groups[1].Value + "/";
                    }

                    string path = "forum" + forumindex + ".txt";
                    forumindex++;
                    XmlNode node = root.SelectSingleNode("Node");//查找<Node>     
                    pattern = node.FirstChild.Value;
                    fsite += mc[0].Groups[1].Value;
                    strHTML = "";
                    bRev = _hh.Open(fsite, Encoding.GetEncoding(strEncoing), ref strHTML);
                    if (!bRev) continue;

                    reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    mc = reg.Matches(strHTML);
                    if (mc.Count > 0)
                    {
                        //string Path = Environment.TickCount.ToString() + ".txt";

                        System.IO.FileStream f = System.IO.File.Create(path);
                        f.Close();

                        System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding(strEncoing));

                        //f2.WriteLine("total " + mc.Count + " nodes! ");
                        //f2.WriteLine("-------------------------------------");

                        foreach (Match m in mc)
                        {
                            if (m.Groups[1].Value.Trim().Length == 0) continue;

                            XmlNode cnode = root.SelectSingleNode("SrcUrl");
                            Regex reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            MatchCollection mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //url
                            {
                                //Console.WriteLine(mc2[0].Groups[1].Value);
                                //f2.WriteLine("url: " + mc2[0].Groups[1].Value);
                            }
                            else
                                continue;

                            cnode = root.SelectSingleNode("Title");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //title
                            {
                                //Console.WriteLine(mc2[0].Groups[1].Value);
                                //f2.WriteLine("title: " + mc2[0].Groups[1].Value.Trim());
                            }

                            cnode = root.SelectSingleNode("Author");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //author 
                            {
                                //Console.WriteLine(mc2[0].Groups[1].Value);
                                //f2.WriteLine("author: " + mc2[0].Groups[1].Value.Trim());
                            }

                            cnode = root.SelectSingleNode("CreateDate");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) // create date
                            {
                                //Console.WriteLine(mc2[0].Groups[1].Value);
                                //f2.WriteLine("create date: " + mc2[0].Groups[1].Value.TrimEnd().TrimStart());
                            }

                            /*
                            cnode = root.SelectSingleNode("Replies");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            //reg2 = new Regex("\"i5\"\\s+nowrap>.+?>(.+?)<", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //reply and view
                            {
                                string strTmp = mc2[0].Groups[1].Value.Trim();
                                string strReply = strTmp.Substring(0, strTmp.IndexOf('/'));
                                string strView = strTmp.Substring(strTmp.IndexOf('/')+1);

                                //f2.WriteLine("replys: " + strReply);
                                //f2.WriteLine("views: " + strView);

                                ////Console.WriteLine(mc2[0].Groups[1].Value);
                            }
                             */
                            cnode = root.SelectSingleNode("Replies");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //reply and view
                            {
                                //f2.WriteLine("replys: " + mc2[0].Groups[1].Value.Trim());

                            }

                            cnode = root.SelectSingleNode("Views");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //reply and view
                            {
                                //f2.WriteLine("Views: " + mc2[0].Groups[1].Value.Trim());

                            }


                            cnode = root.SelectSingleNode("ContentDate");
                            reg2 = new Regex(cnode.FirstChild.Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //reply date
                            {
                                //Console.WriteLine(mc2[0].Groups[1].Value);

                                //f2.WriteLine("reply date: " + mc2[0].Groups[1].Value.TrimEnd().TrimStart());
                            }

                            //f2.WriteLine("-------------------------------------");
                        }

                        f2.Close();
                        f2.Dispose();
                    }
                }
            }

        }

        //这里是 运行的程序的开始
        public int Run(Spider.CrawlTarget target)
        {
            try
            {
                //string strUrlEncode = System.Web.HttpUtility.UrlEncode("黑皮", Encoding.GetEncoding("GB2312"));
                //string strNes = _hh.Get("http://news.baidu.com/ns?word=" + strUrlEncode + "&tn=news&from=news&ie=gb2312", Encoding.GetEncoding("GB2312"));
                //string strNes = _hh.Get("http://news.baidu.com/ns?word=" + strUrlEncode, Encoding.GetEncoding("GB2312"));

                string strSite = target.strUrl, strPattern = target.pattern, strSiteid = target.siteID,
                       strKeyWord = target.strKeyword, strClassName = target.strClassName, strEncoing = target.Encoding;//"GB2312";
                //稍微 修改一点
                // strSite = @"http://s.weibo.com/weibo/%25E8%25A7%2582%25E4%25B8%2596%25E9%259F%25B3&Refer=STopic_box";
                string srtHtmlData = "";
                bool bRev = _hh.Open(strSite, Encoding.GetEncoding(strEncoing), ref srtHtmlData);
                #region 这里不用关注
                if (bRev == false)
                {

                    if (srtHtmlData.IndexOf("超时") >= 0 || srtHtmlData.IndexOf("time out") >= 0 || srtHtmlData.IndexOf("远程主机强迫") >= 0 
                        || srtHtmlData.IndexOf("连接被意外关闭") >= 0 || srtHtmlData.IndexOf("500") >= 0 || srtHtmlData.IndexOf("403") >= 0)
                    {
                        nsGlobalOutput.Output.Write(strSiteid + " 超时!");
                        return 0;
                    }
                    else
                    {
                        InsertErrorList(strSiteid, strSite, "", srtHtmlData);
                        return 0;
                    }
                }
                #endregion

                XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(strPattern);
                xmlDoc.LoadXml(strPattern);
                XmlNode root = xmlDoc.SelectSingleNode("template");//查找<template>     

                XmlNode nodeMainPage = root.SelectSingleNode("MainPage");//查找<MainPage>     

                if (nodeMainPage.FirstChild != null)
                {
                    OptMainNode(ref root, ref nodeMainPage, ref strSite, strEncoing);
                }
                else
                {
                    if (strClassName.CompareTo("news") == 0 || strClassName.CompareTo("blogs") == 0 || strClassName.CompareTo("microbloggings") == 0)
                    {
                        string strKeyWordEncode = System.Web.HttpUtility.UrlEncode(strKeyWord, Encoding.GetEncoding(strEncoing));
                        //strSite += strKeyWordEncode;
                        strSite = strSite.Replace("<>", strKeyWordEncode);
                    }

                    return GetContentOnePage(root, strSite, strSiteid, strEncoing, strClassName, target.strProjectid);

                    #region 这里是注释
                    /*
                    XmlNode node = root.SelectSingleNode("NextPage");//查找<Node>   

                    if (node == null) return -1;

                    if (target.strNextpagecount == "" || target.strStatus.CompareTo("2") == 0) return 1;

                    int count = int.Parse(target.strNextpagecount);
                    if (node.Attributes["Url"] != null)
                    {
                        string pageUrl = node.Attributes["Url"].Value;
                        int h = 0;
                        if (pageUrl.IndexOf("()") >= 0) //从2开始
                        {
                            h = 2;
                        }
                        else
                        {
                            h = 1;
                        }
                        int p = 0;
                        for (int i = h; p < (count - 1); i++,p++)
                        {
                            string strNextpage = "";
                            if (h == 2)
                                strNextpage = pageUrl.Replace("()", i.ToString());
                            else
                                strNextpage = pageUrl.Replace("(0)", i.ToString());

                            GetContentOnePage(root, strNextpage, strSiteid, strEncoing, strClassName, target.strProjectid);
                        }
                    }
                    else
                    {
                        //XmlNode mainnode = node.SelectSingleNode("Page");//查找<count>    
                        // XmlNode mainnode = node.Attributes["count"];//查找<count>    
                        
                        //int count = (mainnode == null || mainnode.Value == "") ? 5 : int.Parse(mainnode.Value);
                        
                        string pageMainpattern = node.Attributes["mainnode"].Value;

                        string pagepattern = node.FirstChild.Value;

                        string strHTML = "";
                        bRev = _hh.Open(strSite, Encoding.GetEncoding(strEncoing), ref strHTML);

                        if (bRev)
                        {
                            Regex reg = new Regex(pageMainpattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            MatchCollection mc = reg.Matches(strHTML);
                            if (mc.Count > 0) //计算分页
                            {
                                //reg = new Regex(pagepattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                //MatchCollection mc1 = reg.Matches(mc[0].Groups[1].Value);

                                string fsite = "";
                                Regex reg1 = new Regex("(http://.+?)/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                MatchCollection mc11 = reg1.Matches(strSite);
                                if (mc11.Count > 0)
                                {
                                    fsite = mc11[0].Groups[1].Value + "/";
                                }

                                for (int i = 0; i < count; i++)
                                {
                                    reg = new Regex(pagepattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                    MatchCollection mc1 = reg.Matches(mc[0].Groups[1].Value);
                                    string strNextpage = fsite + mc1[i].Groups[1].Value;
                                    GetContentOnePage(root, strNextpage, strSiteid, strEncoing, strClassName, target.strProjectid);

                                }

                            }
                        }
                        else
                        {
                            if (strHTML.IndexOf("超时") >= 0 || strHTML.IndexOf("time out") >= 0)
                            {
                                nsGlobalOutput.Output.Write(strSiteid + " 超时!");
                                return 0;
                            }
                            else
                            {
                                //if (strHTML.IndexOf("事务在触") < 0)
                             
                                nsGlobalOutput.Output.Write("error: " + strHTML);
                                if (strHTML.IndexOf("事务在触") < 0)
                                {
                                    InsertErrorList(strSiteid, strSite, "", strHTML);
                                    return -1;
                                }
                            
                            }
                            //InsertErrorList(strSiteid, strSite, strHTML);
                        }
                    }
                     * */
                    #endregion
                    // GetContentOnePage(root, strSite, strSiteid, strEncoing);
                }

            }
            catch(Exception e)
            {
                nsGlobalOutput.Output.Write("run error: " + e.Message);
                InsertErrorList(target.siteID, target.strUrl, "", e.Message);
                return 0;
                //Console.WriteLine("{0} Exception caught.", e);
            }
            return 1;
        }
        #region Member Fields
        private HttpHelper _hh = new HttpHelper();
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using ConGetData.DAL;
using LogNet;
using System.Threading;

namespace ConGetData.BLL
{
    public class MicroblogWork : ISpiderData
    {
        CommonHelper common = new CommonHelper();
       

        private static DateTime sinaHitTime;

        private static int sinaHitCount = 0;
        private static int SinaHitCount
        {
            get
            {
                return sinaHitCount;
            }
            set
            {
                if (value >= 20)
                {
                    sinaHitTime = DateTime.Now;
                }

                sinaHitCount = value;
            }
        }

        public void RunWork(CrawlTarget target)
        {
            LogBLL.Info(string.Format("开始微博, project {0}, site {1}", target.ProjectId, target.SiteId));
            if (target.SiteUrl.Contains("<>"))
            {
                string strKeyWordEncode = target.KeyWords.Replace("+", " ");
                strKeyWordEncode = System.Web.HttpUtility.UrlEncode(strKeyWordEncode, Encoding.GetEncoding(target.SiteEncoding));
                target.SiteUrl = target.SiteUrl.Replace("<>", strKeyWordEncode);
            }

            HttpHelper _hh = new HttpHelper();

            if (target.SiteUrl.Contains("sina"))
            {
                _hh.StrCookie = SystemConst.StrSinaCookie;
            }
            else if (target.SiteUrl.Contains("qq.com"))
            {
                _hh.StrCookie = SystemConst.StrTencentCookie;
            }

            string rawHtml = _hh.Open(target.SiteUrl, target.XmlTemplate.SiteEncoding);
            string html = common.StrDecode(rawHtml);
            Console.WriteLine(html.Length);
            html = Regex.Replace(html, @"\r\n|\r|\n|\t", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            Regex reg = new Regex(target.XmlTemplate.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = GetMatchCollection(reg, html);

            int result = 0;
            foreach (Match match in mc)
            {
                string inputMatchHtml = match.Groups[1].Value.Trim();
                if (inputMatchHtml.Length == 0)
                    continue;
                DataModel model = GetDataModel(target.XmlTemplate, inputMatchHtml, target.SiteUrl);

                if (model.Content != null)
                {
                    model.SiteId = target.SiteId;
                    model.ProjectID = target.ProjectId;

                    Console.WriteLine("------------------------------------------------------------------------");
                    Console.WriteLine(model.Title);
                    Console.WriteLine(model.ContentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    Console.WriteLine("------------------------------------------------------------------------");

                    LogBLL.Info(string.Format("微博匹配, project {0}, site {1}", target.ProjectId, target.SiteId));
                    result += InsertSiteData(model);
                }
            }
            if (result > 0)
            {
                Console.WriteLine(target.SiteId + " 抓取成功");
                LogBLL.Info(string.Format("微博抓取成功结束, project {0}, site {1}", target.ProjectId, target.SiteId));
            }
            else
            {
                LogBLL.Info(string.Format("微博抓取匹配失败, project {0}, site {1} \r\n {2}", target.ProjectId, target.SiteId, rawHtml));
            }

        }

        public XmlTemplate GetXmlTemplate(string tempContent)
        {
            return new XmlTemplate(tempContent);
        }

        public MatchCollection GetMatchCollection(Regex reg, string html)
        {
            return reg.Matches(html);
        }

        public DataModel GetDataModel(XmlTemplate xmlTemp, string inputMatchHtml, string parentUrl)
        {
            //得到 
            DataModel model = new DataModel();
            string title = common.GetMatchRegex(xmlTemp.Title, inputMatchHtml);
            model.Title = common.NoHTML(title).Trim();
            string siteUrl = common.GetMatchRegex(xmlTemp.SrcUrl, Regex.Replace(inputMatchHtml, "<em>\\[(.+?)\\]", "")).Trim();

            model.SiteUrl = common.GetUrl(siteUrl.Replace("&amp;", "&"), parentUrl);
            if (model.SiteUrl.Length < 5 || model.Title.Length <= 3) return model;

            // 由于进去 要登录,所以 就在当前页面得到内容  和时间
            string content = common.GetMatchRegex(xmlTemp.InnerContent, inputMatchHtml);
            model.Content = common.NoHTML(content).Trim();
            string innerDate = common.GetMatchRegex(xmlTemp.InnerDate, inputMatchHtml);
            model.ContentDate = common.GetInnerData(innerDate);
            return model;
        }

        public int InsertSiteData(DataModel model)
        {
            //插入数据库
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] { 
              new SqlParameter("title",model.Title.Length>20?model.Title.Substring(0,20)+"...":model.Title),
              new SqlParameter("content",model.Title),
              new SqlParameter("contentdate",model.ContentDate),
              new SqlParameter("author",model.Author??""),
              new SqlParameter("srcUrl",model.SiteUrl),
              new SqlParameter("siteid",model.SiteId),
              new SqlParameter("projectid",model.ProjectID),
              new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            try
            {
                result = HelperSQL.ExecNonQuery("usp_Spider_Insert_MicroBlog", parms);
            }
            catch (Exception ex)
            {
                LogBLL.Log("usp_Spider_Insert_MicroBlog", parms);
                LogBLL.Error("InsertSiteData", ex);
            }

            return result;
        }
    }
}

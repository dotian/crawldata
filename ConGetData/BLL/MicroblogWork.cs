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
    public class MicroblogWork : CrawlWorkBase
    {
        protected override string InsertSpName
        {
            get
            {
                return "usp_Spider_Insert_MicroBlog";
            }
        }

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

        protected override string GetHttpContent(CrawlTarget target, string siteUrl)
        {
            HttpHelper _hh = new HttpHelper();
            if (siteUrl.Contains("sina"))
            {
                _hh.StrCookie = SystemConst.StrSinaCookie;
            }
            else if (siteUrl.Contains("qq.com"))
            {
                _hh.StrCookie = SystemConst.StrTencentCookie;
            }

            string rawHtml = _hh.Open(siteUrl, target.XmlTemplate.SiteEncoding);
            string html = common.StrDecode(rawHtml);
            Console.WriteLine(html.Length);
            html = Regex.Replace(html, @"\r\n|\r|\n|\t", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return html;
        }

        protected override DataModel GetDataModel(XmlTemplate xmlTemp, string inputMatchHtml, string parentUrl)
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

        protected override SqlParameter[] FormSqlParams(DataModel model)
        {
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
            return parms;
        }
    }
}

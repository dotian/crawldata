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


namespace ConGetData.BLL
{
    public class ForumWork : ISpiderData
    {
        CommonHelper common = new CommonHelper();
        HttpHelper _hh = new HttpHelper();

        public void RunWork(CrawlTarget target)
        {
            if (target.SiteUrl.Contains("<>"))
            {
                string strKeyWordEncode = target.KeyWords.Replace("+", " ");
                strKeyWordEncode = System.Web.HttpUtility.UrlEncode(strKeyWordEncode, Encoding.GetEncoding(target.SiteEncoding));
                target.SiteUrl = target.SiteUrl.Replace("<>", strKeyWordEncode);
            }
            string siteUrlBackup = target.SiteUrl;
            if (target.SiteUrl.Contains("{p}") && target.NextPageCount == 0)
            {
                target.NextPageCount = target.XmlTemplate.PageStart;
            }

            if (target.SiteUrl.Contains("{p}") && target.NextPageCount > 0 && target.NextPageCount <= target.XmlTemplate.PageEnd)
            {
                target.SiteUrl = target.SiteUrl.Replace("{p}", target.NextPageCount.ToString());
            }

            string html = _hh.Open(target.SiteUrl, target.XmlTemplate.SiteEncoding);

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
                    // Console.WriteLine(model.Content);
                    // Console.WriteLine(model.SiteUrl);
                    Console.WriteLine(model.ContentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    Console.WriteLine("------------------------------------------------------------------------");
                    result += InsertSiteData(model);
                }
            }
            if (result > 0)
            {
                // 记录到文件,有多少还可以使用的 , 需要的是 模板Tid,站点SiteId
                Console.WriteLine(target.SiteId + " 抓取成功");
            }
            if (siteUrlBackup.Contains("{p}"))
            {
                if (target.NextPageCount < target.XmlTemplate.PageEnd)
                {
                    target.NextPageCount++;
                    Console.WriteLine("第" + target.NextPageCount + "页");
                    target.SiteUrl = siteUrlBackup;
                    RunWork(target);
                }
                else if (target.NextPageCount == target.XmlTemplate.PageEnd)
                {
                    target.NextPageCount = target.XmlTemplate.PageStart;
                }
            }
            //当前完毕
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
            if (model.SiteUrl.Length < 5 || model.Title.Length <= 3)
                return model;

            //进去,得到内容 和时间
            if (xmlTemp.Layer == "2")
            {
                string innerHtml = _hh.Open(model.SiteUrl, xmlTemp.InnerEncoding);
                innerHtml = Regex.Replace(innerHtml, @"\r\n|\r|\n", "");
                string content = common.GetMatchRegex(xmlTemp.InnerContent, innerHtml);
                model.Content = common.NoHTML(content).Trim();
                string innerDate = common.GetMatchRegex(xmlTemp.InnerDate, innerHtml);
                model.ContentDate = common.GetInnerData(innerDate);
            }
            else if (xmlTemp.Layer == "1")
            {
                string content = common.GetMatchRegex(xmlTemp.InnerContent, inputMatchHtml);
                model.Content = common.NoHTML(content).Trim();
                string innerDate = common.GetMatchRegex(xmlTemp.InnerDate, inputMatchHtml);
                model.ContentDate = common.GetInnerData(innerDate);
            }
            return model;
        }

        public int InsertSiteData(DataModel model)
        {
            //插入数据库
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] { 
                  new SqlParameter("title",model.Title),
                  new SqlParameter("content",model.Content??model.Title),
                  new SqlParameter("contentdate",model.ContentDate),
                  new SqlParameter("author",model.Author??Convert.DBNull),
                  new SqlParameter("pageview",model.PageView<=0?1:model.PageView),
                  new SqlParameter("reply",model.Reply<=0?1:model.Reply),
                  new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                  new SqlParameter("srcUrl",model.SiteUrl),
                  new SqlParameter("siteid",model.SiteId),
                  new SqlParameter("projectid",model.ProjectID)
                };

            try
            {

                result = HelperSQL.ExecNonQuery("usp_Spider_Insert_Forum", parms);

            }
            catch (Exception ex)
            {

                LogBLL.Info("usp_Spider_Insert_Forum" + parms);
                LogBLL.Error("InsertSiteData", ex);
            }

            return result;
        }

    }
}

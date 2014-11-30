using ConGetData.DAL;
using ConGetData.Model;
using LogNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ConGetData.BLL
{
    public abstract class CrawlWorkBase
    {
        protected abstract string InsertSpName { get; }

        protected abstract SqlParameter[] FormSqlParams(DataModel model);

        protected CommonHelper common = new CommonHelper();
        protected HttpHelper _hh = new HttpHelper();

        public void RunWork(CrawlTarget target)
        {
            var siteUrl = ProcessMarksAndPostContent(target);

            string html = GetHttpContent(target, siteUrl);

            MatchContentByTemplate(target, siteUrl, html);

            ProcessPageCount(target);
        }

        protected void RecordSiteFailure(string siteId, string failureMessage, bool ignoreCount = false)
        {
            string failureUpdateSqlTempate = string.Empty;
            if (!ignoreCount)
            {
                failureUpdateSqlTempate += "UPDATE [DataMingDB].[dbo].[SiteList] SET [SiteFailureCount] = SiteFailureCount + 1;";
            }
            failureUpdateSqlTempate += @"INSERT INTO [DataMingDB].[dbo].[SiteListFailureLog]
    ([SiteId]
    ,[Message])
VALUES
    ({0}
    ,'{1}');";

            var sqlCommand = string.Format(failureUpdateSqlTempate,
                siteId,
                failureMessage.Replace("'", "''"));

            try
            {

                HelperSQL.ExecNonQuery(failureUpdateSqlTempate);

            }
            catch (Exception ex)
            {
                LogBLL.Info(string.Format("RecordSiteFailure error, site id:{0}, failureMessage:{1}",
                    siteId,
                    failureMessage));
                LogBLL.Error("RecordSiteFailure", ex);
            }
        }

        protected static string ProcessMarksAndPostContent(CrawlTarget target)
        {
            var siteUrl = target.SiteUrl;
            if (siteUrl.Contains("<>"))
            {
                string strKeyWordEncode = target.KeyWords.Replace("+", " ");
                strKeyWordEncode = HttpUtility.UrlEncode(strKeyWordEncode, Encoding.GetEncoding(target.SiteEncoding));
                siteUrl = siteUrl.Replace("<>", strKeyWordEncode);
            }

            if (siteUrl.Contains("{p}") && target.NextPageCount == -1)
            {
                target.NextPageCount = target.XmlTemplate.PageStart;
            }

            if (siteUrl.Contains("{p}") && target.NextPageCount >= 0 && target.NextPageCount <= target.XmlTemplate.PageEnd)
            {
                siteUrl = siteUrl.Replace("{p}", target.NextPageCount.ToString());
            }

            if (siteUrl.ToUpper().StartsWith("POST:"))
            {
                // Remove POST: in site url.
                siteUrl = siteUrl.Substring(5);

                if (target.PostContent.Contains("<>"))
                {
                    target.PostContent = target.PostContent.Replace("<>",
                        HttpUtility.UrlEncode(target.KeyWords.Replace("+", " "), Encoding.GetEncoding(target.SiteEncoding)).ToUpper());
                }
            }
            else
            {
                target.PostContent = null;
            }

            return siteUrl;
        }

        protected void ProcessPageCount(CrawlTarget target)
        {
            if (target.SiteUrl.Contains("{p}"))
            {
                if (target.NextPageCount < target.XmlTemplate.PageEnd)
                {
                    target.NextPageCount++;
                    Console.WriteLine("第" + target.NextPageCount + "页");
                    RunWork(target);
                }
                else if (target.NextPageCount == target.XmlTemplate.PageEnd)
                {
                    target.NextPageCount = target.XmlTemplate.PageStart;
                }
            }
        }

        protected void MatchContentByTemplate(CrawlTarget target, string siteUrl, string html)
        {
            Regex reg = new Regex(target.XmlTemplate.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(html);

            int result = 0;
            foreach (Match match in mc)
            {
                string inputMatchHtml = match.Groups[1].Value.Trim();
                if (inputMatchHtml.Length == 0)
                    continue;
                DataModel model = GetDataModel(target.XmlTemplate, inputMatchHtml, siteUrl);

                if (model.Content != null)
                {
                    model.SiteId = target.SiteId;
                    model.ProjectID = target.ProjectId;

                    result += InsertSiteData(model);
                }
                else
                {
                    RecordSiteFailure(target.SiteId, "Template id:" + target.Tid + "\r\nSingle node not matched for:" + inputMatchHtml, true);
                }
            }
            if (result > 0)
            {
                // 记录到文件,有多少还可以使用的 , 需要的是 模板Tid,站点SiteId
                Console.WriteLine(target.SiteId + " 抓取成功");
            }
            else
            {
                // No match node
                RecordSiteFailure(target.SiteId, "No result matched from the content:" + html);
            }
        }

        protected int InsertSiteData(DataModel model)
        {
            //插入数据库
            int result = 0;
            SqlParameter[] parms = FormSqlParams(model);

            try
            {
                result = HelperSQL.ExecNonQuery(InsertSpName, parms);

            }
            catch (Exception ex)
            {
                LogBLL.Info(InsertSpName + parms);
                LogBLL.Error("InsertSiteData", ex);
            }

            return result;
        }

        protected virtual string GetHttpContent(CrawlTarget target, string siteUrl)
        {
            string html = null;

            html = _hh.Open(
                siteUrl,
                target.XmlTemplate.SiteEncoding,
                target.PostContent);

            if (string.IsNullOrEmpty(html))
            {
                // html url not available.
                RecordSiteFailure(target.SiteId, "Http result empty. Please refer to the file error log.");
            }

            html = Regex.Replace(html, @"\r\n|\r|\n|\t", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return html;
        }

        protected virtual DataModel GetDataModel(XmlTemplate xmlTemp, string inputMatchHtml, string parentUrl)
        {
            //得到 

            DataModel model = new DataModel();
            string title = common.GetMatchRegex(xmlTemp.Title, inputMatchHtml);
            model.Title = common.NoHTML(title).Trim();
            string siteUrl = common.GetMatchRegex(xmlTemp.SrcUrl, Regex.Replace(inputMatchHtml, "<em>\\[(.+?)\\]", "")).Trim();

            model.SiteUrl = common.GetUrl(siteUrl.Replace("&amp;", "&"), parentUrl);
            if (model.SiteUrl.Length < 5 || model.Title.Length <= 3)
            {
                // site url & title not captured
                return model;
            }

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
    }
}

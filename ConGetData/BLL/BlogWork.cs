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
using System.Web;

namespace ConGetData.BLL
{
    public class BlogWork : CrawlWorkBase
    {
        protected override string InsertSpName
        {
            get
            {
                return "usp_Spider_Insert_Blog";
            }
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

        protected override SqlParameter[] FormSqlParams(DataModel model)
        {
            SqlParameter[] parms = new SqlParameter[] { 
              new SqlParameter("title",model.Title),
              new SqlParameter("content",model.Content==null?model.Title:model.Content),
              new SqlParameter("contentdate",model.ContentDate),
              new SqlParameter("author",model.Author==null?"":model.Author),
              new SqlParameter("srcUrl",model.SiteUrl),
              new SqlParameter("siteid",model.SiteId),
              new SqlParameter("createdate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
              new SqlParameter("projectid",model.ProjectID)
            };
            return parms;
        }
    }
}

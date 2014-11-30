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
    public class ForumWork : CrawlWorkBase
    {
        protected override string InsertSpName
        {
            get
            {
                return "usp_Spider_Insert_Forum";
            }
        }

        protected override SqlParameter[] FormSqlParams(DataModel model)
        {
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
            return parms;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;
namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// deleteTagById 的摘要说明
    /// </summary>
    public class deleteTagById : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tid = Convert.ToInt32(context.Request.Form["tid"]);

            context.Response.Write(deleteTag(tid).ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public int deleteTag(int tid)
        {
            int result = 0;
            result = new TagBLL().DeleteTagByIdManager(tid);
            return result;
        }
    }
}
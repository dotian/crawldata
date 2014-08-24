using System;
using System.Web;
using DataCrawler.BLL.Crawler;
namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandleNewTagr 的摘要说明
    /// </summary>
    public class HandleNewTagr : BaseHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string t = context.Request.Form["t"].ToString();
            string s = context.Request.Form["s"].ToString();
            string k = context.Request.Form["k"].ToString();
            int result = new TagInfoBLL().InsertTagInfoManager(t, s, k);
            context.Response.Write(result);
        }


     
    }
}
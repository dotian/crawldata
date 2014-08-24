using System;
using System.Web;
using System.Collections.Generic;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerLoadTag 的摘要说明
    /// </summary>
    public class HandlerLoadTag : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
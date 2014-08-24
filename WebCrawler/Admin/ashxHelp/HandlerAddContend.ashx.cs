using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerAddContend 的摘要说明
    /// </summary>
    public class HandlerAddContend : BaseHandler
    {

        bool b = false;
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["loginname"]!=null)
            {
                int projectId = Convert.ToInt32(context.Request.Form["projectId"]);
                string ids = context.Request.Form["ids"].ToString();
                string empId = context.Session["loginname"].ToString();
                int result = new ContendBLL().InsertContendManager(projectId, ids, empId);
                b = result > 0;
            }

            context.Response.Write(b);
        }



     
    }
}
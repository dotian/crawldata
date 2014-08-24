using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebCrawler.hankookAshx
{
    /// <summary>
    /// projectviewskip 的摘要说明
    /// </summary>
    public class projectviewskip : IHttpHandler, IRequiresSessionState
    {
        bool b = false;
        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           
            if (context.Session["loginname"]!=null)
            {
                int projectId = Convert.ToInt32(context.Request.Form["projectId"]);
                if (projectId>0)
                {
                    //需要一个用户名, 需要一个 项目id
                    context.Session["projectId"] = projectId;
                    b = true;
                }
            }
            context.Response.Write(b);
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
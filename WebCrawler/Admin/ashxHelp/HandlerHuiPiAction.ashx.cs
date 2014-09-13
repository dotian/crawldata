using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerHuiPiAction 的摘要说明
    /// </summary>
    public class HandlerHuiPiAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string ids = context.Request.Form["sd_id"];
            int result = 0;
            if (ids != null)
            {
                var sd_ids = ids.Split(',').Where(id => !string.IsNullOrEmpty(id)).Select(id => int.Parse(id));

                foreach (int sd_id in sd_ids)
                {
                    try
                    {
                        result += new SiteDataBLL().UpdateSiteDate_ShowStatus_BySd_idManager(sd_id, 0);
                    }
                    catch
                    {
                        // Do nothing
                    }
                }
            }

            context.Response.Write(result);
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
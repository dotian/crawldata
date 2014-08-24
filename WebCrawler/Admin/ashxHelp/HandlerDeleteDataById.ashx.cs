using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;
namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerDeleteDataById 的摘要说明
    /// </summary>
    public class HandlerDeleteDataById :BaseHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            object obj = context.Request.Form["sd_id"];
            if (obj != null)
            {
                //主键sd_Id传过来了，然后 进行删除
                int sd_id = int.Parse(obj.ToString());
                // int sd_id = 0;

                if (sd_id > 0)
                {
                    bool b = DeleteSiteDateBySd(sd_id);
                    context.Response.Write(b);
                }
                else
                {
                    context.Response.Write(false);
                }
            }
            else
            {
                context.Response.Write(false);
            }

            // SD_Id

        }
        public bool DeleteSiteDateBySd(int sd_id)
        {
            int result = new SiteDataBLL().DeleteSiteDateBySd_idManager(sd_id);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteDataBLL).FullName, "DeleteSiteDateBySd_idManager", new object[] { sd_id });
            return result > 0;

        }
    }
}
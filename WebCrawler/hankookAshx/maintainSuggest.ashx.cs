using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataCrawler.BLL.Crawler;
using WebCrawler.Admin.ashxHelp;
using DataCrawler.BLL.Hankook;
namespace WebCrawler.hankookAshx
{
    /// <summary>
    /// maintainSuggest 的摘要说明
    /// </summary>
    public class maintainSuggest :BaseHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                int id = Convert.ToInt32(context.Request.Form["id"]);
                string datatype = context.Request.Form["datatype"].ToString();
                string mess = context.Request.Form["mess"].ToString();
                context.Response.Write(UpdateSuggest(id, datatype, mess));
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("maintainSuggest.ashx",ex);              
            }
        }


        public bool UpdateSuggest(int id, string datatype, string mess)
        {
            int result = new CustomerBLL().UpdateShowDataMess(id, datatype, mess);
            return result > 0;
        }
      
    }
}
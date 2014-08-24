using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;
using System.Text;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// modifyTag 的摘要说明
    /// </summary>
    public class modifyTagById : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tid = Convert.ToInt32(context.Request.Form["id"]);
            string tagname = context.Request.Form["tagname"].ToString();
            // tagname = context.Server.UrlDecode(tagname);
                  
            //鏈嶅姟
            // %E6%9C%8D%E5%8A%A11

             //然后解码
            context.Response.Write(modifyTag(tid, tagname).ToString());
          
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public int modifyTag(int tid, string tagname)
        {
            int result = 0;
            result = new TagBLL().UpdateTagByIdManager(tid, tagname);
            return result;

        }
    }
}
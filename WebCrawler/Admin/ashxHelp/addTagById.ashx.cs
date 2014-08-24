using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;
namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// midifyTagById 的摘要说明
    /// </summary>
    public class addTagById : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tid = Convert.ToInt32(context.Request.Form["id"]);
            string tagname = context.Request.Form["tagname"].ToString();
            context.Response.Write(addtag(tid, tagname).ToString());

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public int addtag(int tid,string tagName)
        {
            // 父类的标签,新标签的名称
            //返回新 标签的Id
            // 父标签为0,表示新建的是1级标签
            return new TagBLL().InsertTagManager(tid, tagName);
        }
    }
}
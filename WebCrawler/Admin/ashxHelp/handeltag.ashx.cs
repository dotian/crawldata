using System;
using System.Web;
using System.Collections.Generic;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// handeltag 的摘要说明
    /// </summary>
    public class handeltag : BaseHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string parms = context.Request.QueryString["pid"];
            if (parms != null)
            {
                string jsonStr = getbatchTagJson(int.Parse(parms.Trim()));
                context.Response.Write(jsonStr);
            }
            else
            {
                context.Response.Write("没有找到项目Id");
            }
        }

       

        public string getbatchTagJson(int projectid)
        {
            List<TagList> list = new TagInfoBLL().GetBatchTagByProjectIdManager(projectid);
               

            string jsonStr = "";
            jsonStr = JsonConvert.SerializeObject(list);

            return jsonStr;

        }
    }
}
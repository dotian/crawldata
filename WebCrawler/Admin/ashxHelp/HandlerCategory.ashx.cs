using System;
using System.Web;

using System.Text;
using System.Collections.Generic;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerCategory 的摘要说明
    /// </summary>
    public class HandlerCategory : BaseHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string classId = context.Request.Form["classId"].ToString();

            context.Response.Write(GetCategoryByClassId(int.Parse(classId)));
                    
         
        }

     
        public string GetCategoryByClassId(int classId)
        {

            List<CategoryInfo> list = new CategoryBLL().GetCategoryListByClassIdManager(classId);
               
            string jsonStr = "";
            jsonStr = JsonConvert.SerializeObject(list);

            return jsonStr;

        }


    }
}
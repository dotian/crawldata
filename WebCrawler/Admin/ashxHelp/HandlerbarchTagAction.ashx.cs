using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;
using DataCrawler.Model.Crawler;
namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerbarchTagAction 的摘要说明
    /// </summary>
    public class HandlerbarchTagAction :BaseHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

           SiteDataBLL siteDataBll = new SiteDataBLL();

            //is_pi 为 true 表示是 批量操作, sd_id 是以逗号分隔的 字符串
            bool is_pi = false;
            if (context.Request.Form["piids"]!=null)
            {
                int piids = Convert.ToInt32(context.Request.Form["piids"]);
                if (piids==1)
                {
                    is_pi = true;
                }
            }
           
            int result = 0;
            if (is_pi)
            {
                string sd_ids = context.Request.Form["sd_id"].ToString();// checkbox Id 批量
                string tagStr = context.Request.Form["tagStr"];
                string[] tagAttr = tagStr.Split('|'); //标签 文本 批量
               List<string>idsList = sd_ids.Split(',').ToList();
               if (tagAttr.Length != 6 || idsList.Count <= 0)
               {
                   context.Response.Write("0");
                   return;
               }

               idsList.RemoveAll(c=>c=="");
               int id = 0;
               foreach (var item in idsList)
               {
                   int.TryParse(item,out id);
                   if (id>0)
                   {
                       DataTag dataTag = new DataTag();
                       dataTag.SD_Id = id;
                       dataTag.Tag1 = tagAttr[0];
                       dataTag.Tag2 = tagAttr[1];
                       dataTag.Tag3 = tagAttr[2];
                       dataTag.Tag4 = tagAttr[3];
                       dataTag.Tag5 = tagAttr[4];
                       dataTag.Tag6 = tagAttr[5];
                       result += siteDataBll.BatchTagListBySd_Id(dataTag);
                   }
               }
            }
            else
            {
                int sd_id = Convert.ToInt32(context.Request.Form["sd_id"]);
                string tagStr = context.Request.Form["tagStr"];
                string[] tagAttr = tagStr.Split('|');
                if (tagAttr.Length != 6 || sd_id <= 0)
                {
                    context.Response.Write("0");
                    return;
                }
                DataTag dataTag = new DataTag();
                dataTag.SD_Id = sd_id;
                dataTag.Tag1 = tagAttr[0];
                dataTag.Tag2 = tagAttr[1];
                dataTag.Tag3 = tagAttr[2];
                dataTag.Tag4 = tagAttr[3];
                dataTag.Tag5 = tagAttr[4];
                dataTag.Tag6 = tagAttr[5];
                result = siteDataBll.BatchTagListBySd_Id(dataTag);
            }
          
            context.Response.Write(result);
        }
    }
}
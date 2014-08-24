using System;
using System.Web;

using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerreloadCurrentTable 的摘要说明
    /// </summary>
    public class HandlerreloadCurrentTable : BaseHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            object pidObj = context.Request.Form["pid"];
            object pObj = context.Request.Form["p"];
            object dataTypeObj = context.Request.Form["dataTYpe"];
            try
            {

                int pid = Convert.ToInt32(pidObj);
                int p = Convert.ToInt32(pObj);
                int dataType = Convert.ToInt32(dataTypeObj);
                string reloadTableHtml = reloadCurentTable(pid, p, dataType);
                context.Response.Write(reloadTableHtml);
            }
            catch
            {
                context.Response.Write("");
            }
        }

        private int PageSize = 13;
        SiteDataBLL bllAction = new SiteDataBLL();
        Help_CreateSiteDataTB helpCreateHtml = new Help_CreateSiteDataTB();
        public string reloadCurentTable(int pid, int currentPage,int dataType)
        {
            QueryDataModelParms queryDataModel = new QueryDataModelParms();
            queryDataModel.pageindex = currentPage;
            queryDataModel.projectId = pid;
            queryDataModel.pagesize = PageSize;
            queryDataModel.sitetype = dataType;
            queryDataModel.showstatus = 0; //显示未审核的数据
            int pageCount = new SiteDataBLL().GetSiteDateCountManager(queryDataModel);
            pageCount = (pageCount - 1) / PageSize + 1;
            List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);
            string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
            string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\"}]";
            return resultJson;
        }
    }
}
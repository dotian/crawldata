using System;
using System.Collections.Generic;
using System.Web;
using DataCrawler.Model.Crawler;
using Newtonsoft.Json;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin.Handler.datadetail
{
    /// <summary>
    /// nextPage_click 的摘要说明
    /// </summary>
    public class nextPage_click : BaseHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            
            string jsonStr = context.Request.Form["jsonStr"].Trim();
            string resultHtml = nextPage_click_1(jsonStr);
            context.Response.Write(resultHtml);

        }

        private int PageSize = 13;
        Help_CreateSiteDataTB helpCreateHtml = new Help_CreateSiteDataTB();
        public string nextPage_click_1(string jsonStr)
        {
            //查询条件,

            List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
            QueryJson queryJson = listQJson[0];
            if (queryJson.pageIndex <= 0)
            {
                queryJson.pageIndex = 1;
            }


            QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);
            int pageCount = new SiteDataBLL().GetSiteDateCountManager(queryDataModel);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteDataBLL).FullName, "GetSiteDateCountManager", new object[] { queryDataModel });
            pageCount = (pageCount - 1) / PageSize + 1;

            List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);
                //(List<SiteDataModel>)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteDataBLL).FullName, "GetSiteDateModelManager", new object[] { queryDataModel });

            string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
            string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, queryDataModel.pageindex, pageCount);

            string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\"}]";
            return resultJson;
        }

        public QueryDataModelParms GetModelParmsByQjson(QueryJson queryJson)
        {
            QueryDataModelParms queryDataModel = new QueryDataModelParms();
            queryDataModel.projectId = queryJson.pid;
            queryDataModel.sitetype = queryJson.dataType;
            queryDataModel.searchKey = queryJson.matchKey;
            queryDataModel.searchType = queryJson.matchRule;
            queryDataModel.startTime = queryJson.timeStart;
            queryDataModel.endTime = queryJson.timeEnd == "" ? DateTime.Now.AddDays(1).ToString() : queryJson.timeEnd;
            queryDataModel.showstatus = queryJson.showStatus;
            queryDataModel.analysis = queryJson.analysis;
            queryDataModel.attention = queryJson.attention;
            queryDataModel.hot = queryJson.hot;
            queryDataModel.pagesize = PageSize;
            queryDataModel.pageindex = queryJson.pageIndex <= 0 ? 1 : queryJson.pageIndex;

            return queryDataModel;
            /*
                pagesize = 13;
                pageindex = 1;
                sitetype = 1;
                searchType = 0;
                searchKey = "";
                startTime = "";
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                showstatus = -1;
                attention = -1;
                analysis = -1;
                hot = -1;
             */
        }
    }
}
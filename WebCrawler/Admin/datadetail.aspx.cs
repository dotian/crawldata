using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Text;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using LogNet;
using System.Configuration;
namespace WebCrawler.Admin
{
    public partial class datadetail : WebCrawler.Public.UI.Page
    {
        private int PageSize = 13;
        Help_CreateSiteDataTB helpCreateHtml = new Help_CreateSiteDataTB();
        SiteDataBLL bllAction = new SiteDataBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(datadetail));
            this.sp_uploadresult.InnerText = "";


            #region !IsPostBack
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["pid"] != null)
                    {
                        QueryDataModelParms queryDataModel = new QueryDataModelParms() { sitetype = 1 };
                        queryDataModel = GetQueryParms(queryDataModel);
                        Bind(queryDataModel);
                    }
                    else
                    {
                        try
                        {
                            Response.Redirect("datadetail.aspx?pid=4&p=1");
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    LogBLL.Error("datadetail.aspx Bind", ex);
                }
            }

            if (Request.QueryString["pid"] != null)
            {
                this.hid_projectId.Value = Request.QueryString["pid"].ToString();
            }
            try
            {
                //在这里的时候,就能够得到,该数据有几个标签
                if (this.hid_batchGropTagStatus.Value != "true")
                {
                    BindSelectTagHtml();
                    this.hid_batchGropTagStatus.Value = "true";//准备好了
                    int projectId = int.Parse(Request.QueryString["pid"]);
                    string[] attrTag = new string[] { };
                    helpCreateHtml.tagHead = new Help_CreateSiteDataTB().GetTagHeadByProjectId(projectId, ref attrTag);
                    this.dv_tag_title.InnerHtml = "<h2>" + helpCreateHtml.tagHead + "</h2>";

                    SiteDataModel item = new SiteDataModel();
                    helpCreateHtml.TagListContent = new Help_CreateSiteDataTB().GetTagContent(item.Tag1, item.Tag2, item.Tag3, item.Tag4, item.Tag5, item.Tag6, attrTag);
                    int width = 80 * (attrTag.Length > 0 ? attrTag.Length : 1);//不能除以0;
                    this.dv_tag_content.InnerHtml = "<div style='width:100%;height:100%;'><div style='width:" + width + "px;margin: 0px auto;'>" + helpCreateHtml.TagListContent + "</div></div>";//</br><div style='width:50px;visibility:hidden;'><span><a href='#' id='sp_tag_t_main' style='text-decoration:underline;'>标签</a></span></div>";
                    this.alt.Style.Add(HtmlTextWriterStyle.Width, (120 + 80 * attrTag.Length).ToString() + "px"); //设定 层的 宽度
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx BindSelectTagHtml", ex);
            }


            #endregion


        }
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex >= 1 ? pageIndex : 1; }
            set { pageIndex = value; }
        }



        #region 绑定打标签的下拉列表
        /// <summary>
        /// 绑定打标签的下拉列表
        /// </summary>
        public void BindSelectTagHtml()
        {
            //这里的Pid 一定不为null;
            try
            {
                int pid = int.Parse(Request.QueryString["pid"].ToString());
                TagBLL tagBLLAction = new TagBLL();
                //得到这个项目的 3个(最多3个一级标签)
                List<TagList> list = tagBLLAction.Get1stTagByProjectIdManager(pid);
                hid_sel_count.Value = list.Count.ToString();
                if (list.Count > 0)
                {
                    sp_tag_1.InnerHtml = helpCreateHtml.CreateSelectHtml(tagBLLAction.GetTagListByTidManager(list[0].Id));
                }
                if (list.Count > 1)
                {
                    sp_tag_2.InnerHtml = helpCreateHtml.CreateSelectHtml(tagBLLAction.GetTagListByTidManager(list[1].Id));
                }
                if (list.Count > 2)
                {
                    sp_tag_3.InnerHtml = helpCreateHtml.CreateSelectHtml(tagBLLAction.GetTagListByTidManager(list[2].Id));
                }
                if (list.Count > 3)
                {
                    sp_tag_4.InnerHtml = helpCreateHtml.CreateSelectHtml(tagBLLAction.GetTagListByTidManager(list[3].Id));
                }
                if (list.Count > 4)
                {
                    sp_tag_5.InnerHtml = helpCreateHtml.CreateSelectHtml(tagBLLAction.GetTagListByTidManager(list[4].Id));
                }
                if (list.Count > 5)
                {
                    sp_tag_6.InnerHtml = helpCreateHtml.CreateSelectHtml(tagBLLAction.GetTagListByTidManager(list[5].Id));
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("BindSelectTagHtml", ex);
            }
        }

        #endregion

        #region 绑定数据显示
        /// <summary>
        /// 绑定数据显示
        /// </summary>
        /// <param name="queryDataModel"></param>
        protected void Bind(QueryDataModelParms queryDataModel)
        {
            try
            {
                if (Request.QueryString["pid"] != null)
                {
                    int pid = int.Parse(Request.QueryString["pid"].ToString());
                    object objPageIndex = Request.QueryString["p"];
                    if (objPageIndex != null)
                    {
                        PageIndex = Convert.ToInt32(objPageIndex);
                    }
                    else
                    {
                        PageIndex = 1;
                    }
                    queryDataModel.pageindex = PageIndex;
                    queryDataModel.projectId = pid;
                    queryDataModel.pagesize = PageSize;
                    queryDataModel.showstatus = 0;

                    int pageCount = new DataCrawler.BLL.Crawler.SiteDataBLL().GetSiteDateCountManager(queryDataModel);
                    pageCount = (pageCount - 1) / PageSize + 1;
                    List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);
                    string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                    this.div_tb_dataList.InnerHtml = alltableHtml;
                    string pagediv = helpCreateHtml.CreateDivPage(pid, 1, pageCount);
                    this.div_page.InnerHtml = pagediv;
                    this.hid_pageCount.Value = pageCount.ToString();
                    string sp_projectContent = new DataCrawler.BLL.Crawler.SiteDataBLL().SelectProjectNameByProjectIdManager(pid, 1);
                    this.sp_projectName.InnerText = sp_projectContent;
                }
                else
                {
                    Response.Redirect("datadetail.aspx?pid=4&p=1");
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 Bind方法  ", ex);
                // Response.Write(ex.Message);
            }

            //查询得到 该项目所有的内容,然后显示出来,,查询条件,读取 隐藏 hidden里面的 内容
        }

        #endregion

        #region GoBack
        protected void btn_goback_click(object sender, EventArgs e)
        {
            try
            {

                QueryDataModelParms queryDataModel = new QueryDataModelParms();
                queryDataModel.sitetype = int.Parse(this.select_dataType.SelectedValue.Trim());
                queryDataModel.projectId = int.Parse(this.hid_projectId.Value.Trim());
                Bind(queryDataModel);
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 btn_goback_click方法  ", ex);
                Response.Write(ex.Message);
            }

        }
        #endregion

        #region 格式化输出 调性,热点
        public string FormatAnalysis(string analysis)
        {
            string result = "";
            switch (analysis.Trim())
            {
                case "0":
                    result = "";
                    break;
                case "1":
                    result = "(正)";
                    break;
                case "2":
                    result = "(中)";
                    break;
                case "3":
                    result = "(负)";
                    break;
                case "4":
                    result = "(争)";
                    break;
                default:
                    result = "(同)";
                    break;
            }

            return result;
        }

        public string FormatHot(string hot)
        {
            string result = "";
            if (hot == "1")
            {
                result = "(热)";
            }
            return result;
        }

        #endregion

        protected QueryDataModelParms GetQueryParms(QueryDataModelParms queryDataModel)
        {
            try
            {
                string starttime = this.txt_timeStart.Value.Trim();
                queryDataModel.startTime = starttime == "" ? "" : DateTime.Parse(starttime).ToString("yyyy-MM-dd");

                string endtime = this.txt_timeEnd.Value.Trim();
                //结束时间 为 29号,查的是 小于 30号的数据

                queryDataModel.endTime = endtime == "" ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") : DateTime.Parse(endtime).AddDays(1).ToString("yyyy-MM-dd 00:00:00");
                queryDataModel.analysis = int.Parse(this.select_analysis.Value);
                queryDataModel.sitetype = int.Parse(this.select_dataType.SelectedValue);
                queryDataModel.searchKey = this.txt_matchKey.Value.Trim();
                queryDataModel.searchType = int.Parse(this.select_matchRule.Value);

                queryDataModel.pagesize = PageSize;
                queryDataModel.pageindex = 1;

                return queryDataModel;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 GetQueryParms方法  ", ex);
                Response.Write(ex.Message);
                return null;
            }

        }

        #region 查询数据

        public QueryDataModelParms GetModelParmsByQjson(QueryJson queryJson)
        {
            try
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
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 GetModelParmsByQjson方法  ", ex);
                Response.Write(ex.Message);
                return null;
            }
        }


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string reloadTable_click(string jsonStr)
        {
            try
            {
                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);

                int pageCount = bllAction.GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;
                List<SiteDataModel> list = bllAction.GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, 1, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
                //显示项目名称
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 img_attention_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string img_attention_click(string jsonStr)
        {
            try
            {
                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);

                int pageCount = bllAction.GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;
                List<SiteDataModel> list = bllAction.GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, 1, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
                //显示项目名称
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 img_attention_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string img_hot_click(string jsonStr)
        {
            try
            {
                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);
                int pageCount = bllAction.GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;
                List<SiteDataModel> list = bllAction.GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, 1, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 img_hot_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }

            //显示项目名称
        }


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string img_queryShowStatus_click(string jsonStr)
        {
            try
            {
                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);
                int pageCount = bllAction.GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;

                List<SiteDataModel> list = bllAction.GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, 1, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 img_queryShowStatus_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }



        protected void QueryShowStatus_Click(object sender, EventArgs e)
        {
            try
            {
                QueryDataModelParms queryDataModel = new QueryDataModelParms();
                queryDataModel = GetQueryParms(queryDataModel);
                int hid_ShowStatus = int.Parse(this.hid_showStatus.Value.Trim());
                queryDataModel.showstatus = hid_ShowStatus;
                Bind(queryDataModel);
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 QueryShowStatus_Click方法  ", ex);
                Response.Write(ex.Message);

            }

        }

        public string IsSubstring(string subStr, int subLength)
        {
            return subStr.Length > subLength ? subStr.Substring(0, subLength) + "..." : subStr;
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            //查询按钮
            try
            {
                QueryDataModelParms queryDataModel = new QueryDataModelParms();
                queryDataModel = GetQueryParms(queryDataModel);
                Bind(queryDataModel);
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 btn_select_Click方法  ", ex);
                Response.Write(ex.Message);

            }

        }

        #endregion

        protected void TextExportExcel(object sender, EventArgs e)
        {
            try
            {
                //先查询,得到List,然后生成 Excel,然后导出
                string fileName = "aaa.zip";//客户端保存的文件名
                string filePath = Server.MapPath("DownLoad/aaa.zip");//路径

                //以字符流的形式下载文件
                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 TextExportExcel方法  ", ex);
                Response.Write(ex.Message);
            }
        }


        #region 操作


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public bool delete_Click(int sd_id)
        {
            //删除
            //showstatus = 99
            try
            {
                int result = bllAction.DeleteSiteDataBySd_idManager(sd_id);
                return result > 0;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 delete_Click方法  ", ex);
                Response.Write(ex.Message);
                return false;
            }

        }

        public bool examine_click_test(int sd_id)
        {
            //审核 showstatus = 2
            try
            {
                int result = bllAction.DeleteSiteDataBySd_idManager(sd_id);
                return result > 0;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 examine_click_test方法  ", ex);
                Response.Write(ex.Message);
                return false;
            }


        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string analysis_Click(int sd_id, int analysis)
        {
            try
            {

                string Html_tr = "";
                int result = bllAction.UpdateSiteDate_Analysis_BySd_idManager(sd_id, analysis);
                if (result > 0)
                {
                    //传一个Sd_id,把这个信息 查出来
                    SiteDataModel sitedataModel = bllAction.GetSiteDateModelBySd_IdManager(sd_id);
                    Html_tr = helpCreateHtml.CreateSiteDataTrHtml(sitedataModel);
                }
                return Html_tr;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 analysis_Click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string hot_Click(int sd_id, int hot)
        {
            //hot =1 表示热
            try
            {
                string Html_tr = "";
                int result = bllAction.UpdateSiteDate_Hot_BySd_idManager(sd_id, hot);
                if (result > 0)
                {
                    //传一个Sd_id,把这个信息 查出来
                    SiteDataModel sitedataModel = bllAction.GetSiteDateModelBySd_IdManager(sd_id);

                    Html_tr = helpCreateHtml.CreateSiteDataTrHtml(sitedataModel);

                }
                return Html_tr;
            }
            catch (Exception ex)
            {

                LogBLL.Error("datadetail.aspx 页 hot_Click方法  ", ex);
                Response.Write(ex.Message);

                return "";
            }

        }

        //关注

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string attention_Click(int sd_id, int attention)
        {
            // attention =1
            try
            {
                string Html_tr = "";

                int result = bllAction.UpdateSiteDate_Attention_BySd_idManager(sd_id, attention);
                if (result > 0)
                {
                    //传一个Sd_id,把这个信息 查出来
                    SiteDataModel sitedataModel = new SiteDataBLL().GetSiteDateModelBySd_IdManager(sd_id);

                    Html_tr = helpCreateHtml.CreateSiteDataTrHtml(sitedataModel);
                }
                return Html_tr;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 attention_Click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }

        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public bool examine_Click(int sd_id, int showstatus)
        {
            //审核,重新绑定当前页的数据 该条数据,进入 已审核,这里不显示
            //showstatus = 2
            try
            {
                int result = bllAction.UpdateSiteDate_ShowStatus_BySd_idManager(sd_id, showstatus);

                return result > 0;

            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 examine_Click方法  ", ex);
                Response.Write(ex.Message);
                return false;
            }


        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string GetNewTable(int p)
        {
            try
            {
                QueryDataModelParms queryDataModel = new QueryDataModelParms();
                queryDataModel.projectId = 1001;
                queryDataModel.startTime = "";
                //结束时间 为 29号,查的是 小于 30号的数据
                queryDataModel.endTime = "";
                queryDataModel.analysis = 0;
                queryDataModel.sitetype = 1;
                queryDataModel.searchKey = "";
                queryDataModel.searchType = 0;

                queryDataModel.pagesize = PageSize;
                queryDataModel.pageindex = p;
                List<SiteDataModel> list = bllAction.GetSiteDateModelManager(queryDataModel);

                return helpCreateHtml.CreateSiteDataTBHtml(list);
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 GetNewTable方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }


        }

        #endregion

        #region 首页
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string firstPage_click(string jsonStr)
        {
            //查询条件,
            try
            {
                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);
                int pageCount = new SiteDataBLL().GetSiteDateCountManager(queryDataModel);

                pageCount = (pageCount - 1) / PageSize + 1;

                List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, 1, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 firstPage_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }

        #endregion

        #region 上一页

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string lastPage_click(string jsonStr)
        {
            //查询条件,
            try
            {
                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                if (queryJson.pageIndex <= 0)
                {
                    queryJson.pageIndex = 1;
                }

                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);
                int pageCount = new SiteDataBLL().GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;
                List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, queryDataModel.pageindex, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 lastPage_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }

        }

        #endregion

        #region 下一页
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string nextPage_click(string jsonStr)
        {
            //查询条件,
            try
            {


                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];
                if (queryJson.pageIndex <= 0)
                {
                    queryJson.pageIndex = 1;
                }


                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);
                int pageCount = new SiteDataBLL().GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;

                List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);
                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(queryDataModel.projectId, queryDataModel.pageindex, pageCount);

                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 nextPage_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }
        #endregion

        #region 尾页
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string finalPage_click(int pid, int currentPage)
        {
            //查询条件,

            try
            {
                QueryDataModelParms queryDataModel = new QueryDataModelParms();
                queryDataModel.pageindex = currentPage;
                queryDataModel.projectId = pid;
                queryDataModel.pagesize = PageSize;

                int pageCount = new SiteDataBLL().GetSiteDateCountManager(queryDataModel);
                pageCount = (pageCount - 1) / PageSize + 1;
                List<SiteDataModel> list = new SiteDataBLL().GetSiteDateModelManager(queryDataModel);

                string alltableHtml = helpCreateHtml.CreateSiteDataTBHtml(list);
                string pagediv = helpCreateHtml.CreateDivPage(pid, currentPage, pageCount);
                string resultJson = "[{\"alltableHtml\":\"" + alltableHtml + "\",\"pagediv\":\"" + pagediv + "\",\"pageCount\":\"" + pageCount + "\"}]";
                return resultJson;
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 finalPage_click方法  ", ex);
                Response.Write(ex.Message);
                return "";
            }
        }

        #endregion

        #region 数据导入导出

        //数据导出
        protected void btn_Export_click(object sender, EventArgs e)
        {

            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + "data.xml";//客户端保存的文件名

                string serverPath = Server.MapPath("~/admin/DataModel/Export/");
                string filePath = serverPath + fileName;
                string jsonStr = this.hid_queryParms.Value.Trim();

                List<QueryJson> listQJson = (List<QueryJson>)JsonConvert.DeserializeObject(jsonStr, typeof(List<QueryJson>));
                QueryJson queryJson = listQJson[0];

                if (queryJson.pageIndex <= 0)
                {
                    queryJson.pageIndex = 1;
                }
                QueryDataModelParms queryDataModel = GetModelParmsByQjson(queryJson);

                queryDataModel.pagesize = bllAction.GetSiteDateCountManager(queryDataModel);

                int maxExportSize = 0;
                maxExportSize = int.Parse(ConfigurationManager.AppSettings["MaxExportSize"].ToString());
                if (queryDataModel.pagesize >= maxExportSize)
                {
                    queryDataModel.pagesize = maxExportSize;
                }
                queryDataModel.pageindex = 1;

                List<SiteDataModel> list = bllAction.GetSiteDateModelManager(queryDataModel);
                if (list.Count <= 0)
                {
                    sp_uploadresult.InnerText = "没有读取到数据";
                    return;
                }
                ExcelCommon excelCommon = new ExcelCommon();
                //xml模板的 文件夹路径
                ExcelCommon.ServerMapPath = Server.MapPath("~/UserControl/");

                bool b = excelCommon.Export_XMLExcel(queryDataModel.projectId, list, filePath);

                //DoOneTimeExport(list, filePath);
                //生成文件名称,文件路径

                if (b)
                {
                    string exportPath = serverPath + "data.xml";
                    if (File.Exists(filePath))
                    {
                        if (File.Exists(exportPath))
                        {
                            File.Delete(exportPath);
                        }
                        File.Copy(filePath, exportPath);
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch
                        {
                        }
                    }

                    FileInfo fileInfo = new FileInfo(exportPath);
                    if (fileInfo.Exists)
                    {
                        const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                        byte[] buffer = new byte[ChunkSize];
                        Response.Clear();
                        System.IO.FileStream iStream = System.IO.File.OpenRead(exportPath);
                        long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                        while (dataLengthToRead > 0 && Response.IsClientConnected)
                        {
                            int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                            Response.OutputStream.Write(buffer, 0, lengthRead);
                            Response.Flush();
                            dataLengthToRead = dataLengthToRead - lengthRead;
                        }
                        iStream.Close();
                        Response.Close();
                    }
                    sp_uploadresult.InnerText = "导出成功!";
                }
                else
                {
                    sp_uploadresult.InnerText = "导出失败!";
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 btn_Export_click方法  ", ex);
                Response.Write(ex.Message);
                //  Response.Redirect("ApplicationErroy.aspx");

            }

        }

        public static object obj = new object();

        //下载模板
        protected void btn_downloadTemplate_click(object sender, EventArgs e)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_") + "datamodel.xls";//客户端保存的文件名
                string filePath = Server.MapPath("~/admin/DataModel/datamodel.xls");//路径

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                if (fileInfo.Exists == true)
                {
                    const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                    byte[] buffer = new byte[ChunkSize];

                    Response.Clear();
                    System.IO.FileStream iStream = null;
                    lock (obj)
                    {
                        iStream = System.IO.File.OpenRead(filePath);
                    }

                    long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("datamodel.xls"));
                    while (dataLengthToRead > 0 && Response.IsClientConnected)
                    {
                        int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                        Response.OutputStream.Write(buffer, 0, lengthRead);
                        Response.Flush();
                        dataLengthToRead = dataLengthToRead - lengthRead;
                    }
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                LogBLL.Error("datadetail.aspx 页 btn_downloadTemplate_click方法  ", ex);
                Response.Write(ex.Message);
            }

        }


        protected void btn_importExcel_Click(object sender, EventArgs e)
        {

            //导入数据
            int projectId = 0;
            int siteType = 0;
            try
            {
                projectId = int.Parse(this.hid_projectId.Value.Trim());
                if (projectId <= 0)
                {
                    sp_uploadresult.InnerText = "没有获取到项目";
                    return;
                }

                // Use selected site type as default value
                siteType = int.Parse(this.select_dataType.SelectedValue.Trim());
            }
            catch (Exception ex)
            {
                sp_uploadresult.InnerText = "导入发生内部错误,导入失败";

                LogBLL.Error("datadetail.aspx 页 btn_importExcel_Click方法  ", ex);
                Response.Write(ex.Message);
            }

            if (fileUpload1.HasFile)
            {
                //判断文件是否小于20Mb
                if (fileUpload1.PostedFile.ContentLength < 20485760) //20M
                {
                    try
                    {
                        /*注意->这里为什么不是:FileUpLoad1.PostedFile.FileName
                      * 而是:FileUpLoad1.FileName?
                      * 前者是获得客户端完整限定(客户端完整路径)名称
                      * 后者FileUpLoad1.FileName只获得文件名.
                      */
                        //上传文件并指定上传目录的路径
                        string savepath = Server.MapPath("~/admin/DataModel/Import/") + fileUpload1.FileName;
                        if (File.Exists(savepath))
                        {
                            File.Delete(savepath);
                        }
                        fileUpload1.PostedFile.SaveAs(savepath);
                        sp_uploadresult.InnerText = savepath;
                        List<SiteDataModel> list = new ExcelCommon().DoOneTimeImport(savepath, siteType, projectId);
                        int result = bllAction.InsertImportSiteDataByProjectIdManager(list);
                        sp_uploadresult.InnerText = string.Format("上传文件解析成功, 上传成功数据:{0}条,识别数据:{1}条,插入失败数据:{2}条", result, list.Count, list.Count - result);

                    }
                    catch (Exception ex)
                    {
                        sp_uploadresult.InnerText = "出现错误,上传失败";
                        LogBLL.Error("datadetail.aspx 页 btn_importExcel_Click方法  ", ex);
                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    sp_uploadresult.InnerText = "上传文件不能大于20MB!";
                }
            }
            else
            {
                sp_uploadresult.InnerText = "尚未选择文件!";
            }
        }

        #endregion
    }
}
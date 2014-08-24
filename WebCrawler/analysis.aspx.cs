using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using DataCrawler.Model.Hankook;
using DataCrawler.BLL.Hankook;
using System.IO;
using LogNet;
namespace WebCrawler
{
    public partial class analysis : WebCrawler.Public.UI.ForePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    object objProjectId = Session["projectId"];
                    if (objProjectId == null)
                    {
                        Response.Redirect("index.aspx");
                    }
                    string imgLogoPath = Server.MapPath("img/small_project_" + objProjectId + ".jpg");
                    if (!File.Exists(imgLogoPath))
                    {
                        this.div_logo.Visible = false;
                    }
                    if (Session["permissions"] == null || Convert.ToInt32(Session["permissions"]) != 4)
                    {
                        this.img_retplist.Visible = false;
                    }
                    PrepareBind();
                    Bind();
                }
                catch (Exception ex)
                {

                    LogNet.LogBLL.Error("analysis.aspx Page_Load", ex);
                }
            }
        }

        public int PageSize = 20;

        public void Bind()
        {

            // 数据类型,页码数,关键字,开始日期,结束日期
            try
            {

                this.hid_attention.Value = "0";
                this.hid_showstatus.Value =  "0";

                QueryHankookArgs queryArgs = GetQueryParms();
                queryArgs.pageIndex = 1;
                BindData(queryArgs);
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("analysis.aspx Bind", ex);
            }
        }

        
        public void BindData(QueryHankookArgs queryArgs)
        {
            try
            {
                CustomerBLL customerBLL = new CustomerBLL();
                int rescordCount = customerBLL.GetShowDataCountManager(queryArgs);
                List<ShowDataInfo> list = customerBLL.GetShowDataListManager(queryArgs);

                this.AspNetPager1.PageSize = PageSize;
                this.AspNetPager1.RecordCount = rescordCount;
                // this.AspNetPager1. = "212121";
                this.rep_data.DataSource = list;
                this.rep_data.DataBind();
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("analysis.aspx BindData", ex);
            }
        }

        public void PrepareBind()
        {
            this.txt_startdate.Value = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
            this.txt_enddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
           
            if (Request.QueryString["type"] == null)
            {
                Response.Redirect("analysis.aspx?type=sitedata&c=1&a=1");
            }
            this.hid_projectId.Value = Session["projectId"].ToString();
        }

        public QueryHankookArgs GetQueryParms()
        {
            QueryHankookArgs queryhankook = new QueryHankookArgs();
            if (Request.QueryString["type"] != null)
            {
                queryhankook.datatype = Request.QueryString["type"].ToString();
            }
            else
            {
                queryhankook.datatype = "sitedata";
            }
            if (Request.QueryString["a"] != null)
            {
                queryhankook.analysis = int.Parse(Request.QueryString["a"].ToString());
            }
            else
            {
                queryhankook.analysis = 0;
            }
            if (Request.QueryString["c"] != null)
            {
                queryhankook.sitetype = int.Parse(Request.QueryString["c"].ToString());
            }
           

            queryhankook.ProjectId = int.Parse(this.hid_projectId.Value.Trim());
            queryhankook.start = this.txt_startdate.Value.Trim();
            queryhankook.end = this.txt_enddate.Value.Trim();
            queryhankook.file1 = this.file1.SelectedIndex + 1; //SelectedIndex 索引 从0开始,变成 从1开始
            queryhankook.file2 = this.file2.SelectedIndex;
            queryhankook.file3 = this.file3.Value.Trim();
            queryhankook.pagesize = PageSize;
            return queryhankook;
        }


        protected void btn_search_click(object sender, EventArgs e)
        {
            Bind();


        }

        protected void btn_attention_click(object sender, EventArgs e)
        {
            //关注
         
             QueryHankookArgs queryArgs = GetQueryParms();
                queryArgs.pageIndex = 1;
                queryArgs.attention = 1;
                BindData(queryArgs);
              //查找所有 关注的数据
                this.hid_attention.Value = "1";
        }

        protected void btn_dofinish_click(object sender, EventArgs e)
        {
            //处理中
            int attention = 0;
            int.TryParse(this.hid_attention.Value, out attention);
            QueryHankookArgs queryArgs = GetQueryParms();
            queryArgs.pageIndex = 1;
            queryArgs.showstatus = 1;
            queryArgs.attention = attention;
            this.hid_showstatus.Value = "1";
            BindData(queryArgs);
            //查找状态为1 的数据
        }

        protected void btn_finish_click(object sender, EventArgs e)
        {
            //处理完成
            int attention = 0;
            int.TryParse(this.hid_attention.Value, out attention);
            //查找状态为2 的数据
            QueryHankookArgs queryArgs = GetQueryParms();
            queryArgs.pageIndex = 1;
            queryArgs.showstatus = 2;
            queryArgs.attention = attention;
            BindData(queryArgs);
            this.hid_showstatus.Value = "2";

        }
        protected void btn_download_click(object sender, EventArgs e)
        {
            //下载数据

            //查出 所有的数据, 然后下载出来,"编号","标题","帖子链接","作者","媒体名","日期","调性","标签"

             //先得到文件存放的地址, 然后进行输出

            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff_") + "data.csv";//客户端保存的文件名
                string serverPath = Server.MapPath("~/ExportData/");
                string filePath = serverPath + fileName;

                int attention = 0, showstatus = 0;
                int.TryParse(this.hid_attention.Value, out attention);
                int.TryParse(this.hid_showstatus.Value, out showstatus);


                QueryHankookArgs queryArgs = GetQueryParms();
                queryArgs.pageIndex = 1;
                queryArgs.showstatus = showstatus;
                queryArgs.attention = attention;

                CustomerBLL customerBLL = new CustomerBLL();
                int rescordCount = customerBLL.GetShowDataCountManager(queryArgs);
                if (rescordCount <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('获取到数据条数为0);", true);
                    return;
                }
                queryArgs.pagesize = rescordCount;
                List<ShowDataInfo> list = customerBLL.GetShowDataListManager(queryArgs);
                //生成csv
                bool b = new ExcelCommon().Export_HankookCsv(list, filePath);
                if (b)
                {
                    string exportPath = serverPath + "data.csv";
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
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("data.csv"));
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
                }

            }
            catch (Exception ex)
            {
                LogBLL.Error("analysis.aspx 页 btn_download_click方法  ", ex);
                Response.Write(ex.Message);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            //得到 查询 参数
            try
            {
                QueryHankookArgs queryArgs = GetQueryParms();
                queryArgs.pagesize = PageSize;
                queryArgs.sitetype = 1;
                queryArgs.pageIndex = this.AspNetPager1.CurrentPageIndex;
                BindData(queryArgs);
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("customer.aspx AspNetPager1_PageChanged", ex);
            }
        }

        public string IsSubString(string s, int size)
        {
            // 绑定的时候这样绑定
            // <%# IsSubString(DataBinder.Eval(Container.DataItem, "SiteUrl").ToString(), 20)%>
            return s.Length >= size ? s.Substring(0, size) + "..." : s;

        }

    }
}
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

namespace WebCrawler
{
    public partial class customer : WebCrawler.Public.UI.ForePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    object objProjectId = Session["projectId"];
                    if ( objProjectId== null)
                    {
                        Response.Redirect("index.aspx");
                        return;
                    }
                    if (Session["permissions"] == null||Convert.ToInt32(Session["permissions"])!=4)
                    {
                        this.img_retplist.Visible = false;
                    }
                    string imgLogoPath = Server.MapPath("img/small_project_"+objProjectId+".jpg");
                    if (!File.Exists(imgLogoPath))
                    {
                        this.div_logo.Visible = false;
                    }
                   
                    this.hid_projectId.Value = objProjectId.ToString();
                    PrepareBind();
                    CheckExitsContendId();
                    Bind();
                }
                catch (Exception ex)
                {
                    LogNet.LogBLL.Error("customer.aspx Page_Load", ex);
                }
            }
        }

        public int PageSize = 20;

        public void Bind()
        {

            // 数据类型,页码数,关键字,开始日期,结束日期
            try
            {
                QueryHankookArgs queryArgs = GetQueryParms();
                queryArgs.pageIndex = 1;
                BindData(queryArgs);
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("customer.aspx Bind", ex);
            }

        }

        public void CheckExitsContendId()
        {
            if (Request.QueryString["contendId"] != null)
            {
                int contendId = 0;
                int.TryParse(Request.QueryString["contendId"].ToString(), out contendId);
                //判断这个 竞争社项目 , 这个人是不是可以查看
                int permissions = Convert.ToInt32(Session["permissions"]);//权限
                int projectId = Convert.ToInt32(Session["projectId"]);//项目
                if (permissions == 3)
                {
                    bool exists = customerBLL.GetExistContentIdManager(projectId, contendId);
                    if (!exists)
                    {
                        //不存在 就跳转
                        Session["loginname"] = null; //登录名
                        Session["projectId"] = null; //项目
                        Session["permissions"] = null;//权限
                        Response.Redirect("index.aspx");
                    }
                }
            }
        }

        CustomerBLL customerBLL = new CustomerBLL();
        public void BindData(QueryHankookArgs queryArgs)
        {
            try
            {
                if (queryArgs.contendId>0)
                {
                    queryArgs.ProjectId = queryArgs.contendId;
                }
                int rescordCount = customerBLL.GetShowDataCountManager(queryArgs);
                List<ShowDataInfo> list = customerBLL.GetShowDataListManager(queryArgs);

                this.AspNetPager1.PageSize = PageSize;
                this.AspNetPager1.RecordCount = rescordCount;

                this.rep_data.DataSource = list;
                this.rep_data.DataBind();
            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("customer.aspx BindData", ex);
            }
        }

        public void PrepareBind()
        {
            this.txt_startdate.Value = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
            this.txt_enddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            if (Request.QueryString["type"] == null)
            {
                Response.Redirect("customer.aspx?type=sitedata&c=1&a=1");
            }
            
            int projectId = Convert.ToInt32(Session["projectId"]);
            List<ContendTB> contendList = new CustomerBLL().GetContendTbListByProjectIdManager(projectId);

            if (contendList.Count>0)
            {
                this.a_contendFhref.HRef = "customer.aspx?type=sitedata&c=2&a=1&contendId=" + contendList[0].ContendId;
            }
            else
            {
                this.a_contendFhref.HRef = "#";
            }
           
            this.rep_contend.DataSource = contendList;
            this.rep_contend.DataBind();
            
            //读取Url, 如果含有 project, 所得到的的项目Id, 要么是这个账号的项目, 要么是权限为4的账号项目
        }

        public QueryHankookArgs GetQueryParms()
        {
            QueryHankookArgs queryhankook = new QueryHankookArgs();
            try
            {
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
                if (Request.QueryString["contendId"] != null)
                {
                    int contendId = 0;
                    int.TryParse(Request.QueryString["contendId"].ToString(), out contendId);
                    queryhankook.contendId = contendId;
                }
                else
                {
                    queryhankook.contendId = 0; //项目 关键字
                }
                queryhankook.ProjectId = int.Parse(this.hid_projectId.Value.Trim());
                queryhankook.start = this.txt_startdate.Value.Trim();
                queryhankook.end = this.txt_enddate.Value.Trim();
                queryhankook.file1 = this.file1.SelectedIndex + 1; //SelectedIndex 索引 从0开始,变成 从1开始
                queryhankook.file2 = this.file2.SelectedIndex;
                queryhankook.file3 = this.file3.Value.Trim();
                queryhankook.pagesize = PageSize;
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("customer.aspx GetQueryParms", ex);
            }
            return queryhankook;
        }


        protected void btn_search_click(object sender, EventArgs e)
        {
            Bind();
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

                //绑定一个项目的竞争社 前4种

              
                //绑定的数据

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

        #region 注释
        /*
            <li><span>img</span><label><a href="customer.aspx?type=sitedata&c=2&a=1&keys=Michelin">米其林 Michelin</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=sitedata&c=2&a=1&keys=Bridgestone">普利司通 Bridgestone</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=sitedata&c=2&a=1&keys=Pirelli">倍耐力 Pirelli</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=sitedata&c=2&a=1&keys=Goodyear">固特异 Goodyear</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=news&keys=Kumho">锦湖 Kumho</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=news&keys=Yokohama">优科豪马 Yokohama</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=news&keys=Continental">马牌 Continental</a></label></li>
            <li><span>img</span><label><a href="customer.aspx?type=news&keys=Dunlop">邓禄普 Dunlop</a></label></li>
         */
        #endregion

    }
}
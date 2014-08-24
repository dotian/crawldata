using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin
{
    public partial class news_sitelist : WebCrawler.Public.UI.Page
    {

        private int PageSize = 25;
        private int siteType = 2; //表示新闻
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(news_sitelist));
            if (!IsPostBack)
            {
                Bind();
            }
        }


        public void Bind()
        {
            try
            {
                this.AspNetPager1.PageSize = PageSize;
                this.AspNetPager1.RecordCount = new SiteListBLL().RecordCountSiteListBySiteTypeManager(siteType, 0, "");
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "RecordCountSiteListBySiteTypeService", new object[] { siteType, 0, "" });

                List<SiteList> list = new SiteListBLL().GetSiteListBySiteTypeManager(siteType, PageSize, 1, 0, "");
                //(List<SiteList>)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "GetSiteListBySiteTypeManager", new object[] { siteType, PageSize, 1, 0, "" });

                this.rep_siteListBySiteType.DataSource = list;
                this.rep_siteListBySiteType.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            try
            {
                string searchKey = this.txt_selectKey.Value.Trim();
                int searchType = 0;
                if (searchKey != "")
                {
                    searchType = int.Parse(this.sel_SearchType.Value);
                }
                this.AspNetPager1.RecordCount = new SiteListBLL().RecordCountSiteListBySiteTypeManager(siteType, searchType, searchKey);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "RecordCountSiteListBySiteTypeService", new object[] { siteType, searchType, searchKey });

                List<SiteList> list = new SiteListBLL().GetSiteListBySiteTypeManager(siteType, PageSize, 1, searchType, searchKey);
                //(List<SiteList>)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "GetSiteListBySiteTypeManager", new object[] { siteType, PageSize, 1, searchType, searchKey });

                this.rep_siteListBySiteType.DataSource = list;
                this.rep_siteListBySiteType.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            try
            {
                int currentPage = this.AspNetPager1.CurrentPageIndex;
                string searchKey = this.txt_selectKey.Value.Trim();
                int searchType = 0;
                if (searchKey != "")
                {
                    searchType = int.Parse(this.sel_SearchType.Value);
                }
                List<SiteList> list = new SiteListBLL().GetSiteListBySiteTypeManager(siteType, PageSize, currentPage, searchType, searchKey);
                //(List<SiteList>)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "GetSiteListBySiteTypeManager", new object[] { siteType, PageSize, currentPage, searchType, searchKey });

                this.rep_siteListBySiteType.DataSource = list;
                this.rep_siteListBySiteType.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public bool InsertSiteList(string siteName, string plateName, string siteUrl, int siteRank)
        {
            try
            {
                int result = new SiteListBLL().InsertSiteListManager(siteName, plateName, siteUrl, siteRank, siteType);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "InsertSiteListManager", new object[] { siteName, plateName, siteUrl, siteRank, siteType });

                return result > 0;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
        }


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public bool UpdateSiteListBySiteId(int siteId, string siteName, string plateName, string siteUrl, int siteRank)
        {
            try
            {
                int result = new SiteListBLL().UpdateSiteListBySiteIdManager(siteId, siteName, plateName, siteUrl, siteRank);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "UpdateSiteListBySiteIdManager", new object[] { siteId, siteName, plateName, siteUrl, siteRank });

                return result > 0;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public bool DeleteSiteListBySiteId(int siteId)
        {
            try
            {
                int result = new SiteListBLL().DeleteSiteListBySiteIdManager(siteId);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.SiteListBLL).FullName, "DeleteSiteListBySiteIdManager", new object[] { siteId });

                return result > 0;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
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
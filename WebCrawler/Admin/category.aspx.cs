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
using System.IO;

namespace WebCrawler.Admin
{
    public partial class category : WebCrawler.Public.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    if (Request.QueryString["cateId"] != null)
                    {
                        int cateId = int.Parse(Request.QueryString["cateId"]);
                        //显示这个的 分类
                        Bind(cateId);
                    }
                    else
                    {
                        Bind(0);
                    }

                    this.sel_category.Items.Clear();
                    this.sel_category.Items.Insert(0, new ListItem("--请选择--", "0"));
                }
                catch (Exception ex)
                {

                    LogNet.LogBLL.Error("category.aspx Page_Load", ex);
                }
            }
        }

        public void Bind(int cateId)
        {
            try
            {
                List<CategoryInfo> list = new DataCrawler.BLL.Crawler.CategoryBLL().GetCategoryListManager();
                //(List<CategoryInfo>)EventSubmit(typeof(DataCrawler.BLL.Crawler.CategoryBLL).FullName, "GetCategoryListManager", new object[] { });

                sel_category.DataTextField = "CategoryName";
                sel_category.DataValueField = "CateId";
                this.sel_category.DataSource = list;
                this.sel_category.DataBind();
                if (cateId > 0)
                {
                    this.sel_category.Value = cateId + "";


                    List<SiteList> siteList = new DataCrawler.BLL.Crawler.CategoryBLL().GetSiteListByCateIdManager(cateId);
                    //(List<SiteList>)EventSubmit(typeof(DataCrawler.BLL.Crawler.CategoryBLL).FullName, "GetSiteListByCateIdManager", new object[] { cateId });
                    this.rep_sitelist.DataSource = siteList;
                    this.rep_sitelist.DataBind();

                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message); 
            }
           
        }


        protected void btn_select_Click(object sender, EventArgs e)
        {
            string searcyType = this.sel_SearchType.Value;
            string searchKey = this.txt_selectKey.Value.Trim();
            //查询出 对应的站点
        }

        protected void sel_category_change(object sender, EventArgs e)
        {
         
        }
    }
}
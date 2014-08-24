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
    /// <summary>
    /// 竞争社
    /// </summary>
    public partial class contend:Public.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    //记下当前页面
                try
                {
                    Session["LoginAfterUrl"] = "contend.aspx";

                    Bind("");
                    this.sel_ContendProject.Items.Clear();
                    List<ProjectList> list = contendManager.GetProjectInfoManager("");
                    this.sel_ContendProject.DataSource = list;
                    this.sel_ContendProject.DataTextField = "ProjectName";
                    this.sel_ContendProject.DataValueField = "ProjectId";
                    this.sel_ContendProject.DataBind();
                    this.sel_ContendProject.Items.Insert(0, new ListItem("--请选择--", ""));
                }
                catch (Exception ex)
                {

                    LogNet.LogBLL.Error("contend.aspx  Page_Load",ex);
                }
              
            }
        }
        ContendBLL contendManager = new ContendBLL();
        public void Bind(string projectname)
        {
            List<ProjectList> list = contendManager.GetProjectInfoManager(projectname);
            this.rep_contendList.DataSource = list;
            this.rep_contendList.DataBind();
        }
        protected void btn_select_Click(object sender, EventArgs e)
        {

            string projectName = this.txt_selectKey.Value.Trim();
            Bind(projectName);

        }
       

    }
}
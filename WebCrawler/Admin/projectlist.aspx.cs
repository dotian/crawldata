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
    public partial class projectlist : Public.UI.Page
    {
        // ProjectListBLL actionBLL = new ProjectListBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(projectlist));
            if (!IsPostBack)
            {
                //判断账户类型
                //首先是登录,这个已经判断过了,接下来是新加账户的类型
                //低级用户只能看到自己的项目
                //高级用户,可以看到所有的

                Bind();
            }
        }
        protected void btn_select_Click(object sender, EventArgs e)
        {
            //查询
            try
            {
                int searchType = int.Parse(this.sel_SearchType.Value);
                string searchKey = this.txt_selectKey.Value.Trim();
                string loginName = Session["loginname"].ToString();
                List<ProjectList> list = new ProjectListBLL().GetProjectListManager(searchType, searchKey, 1, loginName);
       
                rep_projectlist.DataSource = list;
                rep_projectlist.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void Bind()
        {
            try
            {
                //判断 用户的权限
                int searchType = int.Parse(this.sel_SearchType.Value);
                string searchKey = this.txt_selectKey.Value.Trim();
                string loginName = Session["loginname"].ToString();
                List<ProjectList> list = new DataCrawler.BLL.Crawler.ProjectListBLL().GetProjectListManager(0, "", 1, loginName);
 
                rep_projectlist.DataSource = list;
                rep_projectlist.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public int a_stopProject_click(int projectId)
        {
           int rsult =  new ProjectListBLL().UpdateProjectListStatusByProjectIdManager(projectId,0);
           return rsult;
        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public int a_startProject_click(int projectId)
        {
            int rsult = new ProjectListBLL().UpdateProjectListStatusByProjectIdManager(projectId, 1);
            return rsult;
        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public int a_deleteProject_click(int projectId)
        {
            int rsult = new ProjectListBLL().UpdateProjectListStatusByProjectIdManager(projectId, 99); 
            return rsult;
        }
    }
}
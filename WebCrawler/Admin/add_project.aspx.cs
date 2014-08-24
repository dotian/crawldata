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
    public partial class add_project : WebCrawler.Public.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(add_project));
            if (!IsPostBack)
            {
                if (Session["loginname"]!=null)
                {
                    this.txt_EmpName.Value = Session["loginname"].ToString();
                }
                else
                {
                    //记下当前页面
                    Session["LoginAfterUrl"] = "add_project.aspx";
                     Response.Redirect("login.aspx");//调回到登录页面
                }
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public int addProject_click(string pname, string matchRule, int matchType, string rssKey, string enddate, string empName, string sp_forum, string sp_news, string sp_blog, string sp_microblog)
        {
            int result = 0;
            try
            {
                //txtRssKey
                ProjectList projectList = new ProjectList();
                projectList.ProjectName = pname;
                projectList.MatchingRule = matchRule;
                projectList.MatchingRuleType = matchType;
                projectList.RssKey = rssKey;
                projectList.CreateDate = DateTime.Now;
                projectList.EndDate = Convert.ToDateTime(enddate);
                projectList.EmpId = empName;

                 result = new ProjectListBLL().InsertProjectListManager(projectList, sp_forum, sp_news, sp_blog, sp_microblog);

            }
            catch (Exception)
            {
                return 0;
            }
            return result;
        }


       
    }
}
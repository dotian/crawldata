using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataCrawler.Model.Hankook;
using DataCrawler.BLL.Hankook;
namespace WebCrawler
{
    public partial class projectview : WebCrawler.Public.UI.ForePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["permissions"] == null || Session["loginname"]==null)
            {
                //跳抓到登陆页面
                Response.Redirect("index.aspx");
                return;
            }
            if (!IsPostBack)
            {
                Bind();      
            }
        }
        CustomerBLL customerBll = new CustomerBLL();  
        public void Bind()
        {
            int projectId = Convert.ToInt32(Session["projectId"]);
            int permissions = Convert.ToInt32(Session["permissions"]);

            if (permissions == 3)
            {
                //权限为 3的只有一个项目,客户临时账号
                Response.Redirect("customer.aspx?type=sitedata&c=1&a=1");
                 return;
            }
            if (permissions == 4)
            {
                //权限为 4的情况有两种 ,其一是管理员 admin的,可以看到所有运行的项目
                //其二是 员工的, 只能看到员工自己建的项目

                string uerName = Session["loginname"].ToString();
                //传进去一个账号
                List<ProjectInfo> list = customerBll.GetProjectListManager(uerName);
                this.rep_projectview.DataSource = list;
                this.rep_projectview.DataBind();
            }

          
        }
    }
}
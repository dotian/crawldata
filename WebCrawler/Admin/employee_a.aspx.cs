using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCrawler.Admin
{
    public partial class employee_a : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //判断登录状态,如果没有足够的权限,就让他回到首页
             
                if (Session["loginname"]==null)
                {
                    return;
                }
                if (Session["loginname"].ToString()!="admin")
                {
                    Response.Write("您不是管理员用户,请用系统管理员帐号登录!");
                }
                else
                {
                    hid_adminPermission.Value = "1";
                }

            }
        }
       
    }
}
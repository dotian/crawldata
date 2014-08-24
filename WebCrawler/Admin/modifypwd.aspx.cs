using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin
{
    public partial class ModifyPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //登录的帐号
            if (!IsPostBack)
            {
                if (Session["loginname"]==null)
                {
                    Session["LoginAfterUrl"] = "modifypwd.aspx";
                    Response.Redirect("login.aspx");
                }
            }
        }
         
        protected void btn_modifyPwd_Click(object sender, EventArgs e)
        {

            //修改密码
            string oriPwd = this.txt_oriPwd.Value.Trim();
            //得到 原始密码,新密码,验证码
            string pwd = this.txt_pwd.Value.Trim();
            string code = this.txt_code.Value.Trim();
            string loginName = Session["loginname"].ToString();
            EmployeeBLL empBll = new EmployeeBLL();
            if (Session["code"].ToString()!=code)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('验证码输入有误');", true);
                return;
            }
           
                //验证原始密码
              int loginResult = empBll.LoginActionManager(loginName, oriPwd);
              if (loginResult==1)
              {
                  //登录成功,然后修改密码
                  int midfyResult = empBll.UpdateAccountPwdManager(loginName, pwd);
                  if (midfyResult==1)
                  {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('修改成功');", true);
                  }
                  else
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('修改失败');", true);
                  }
              }
              else
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('密码错误');", true);
                  return;
              }
        }
    }
}
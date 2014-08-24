using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;
using WebCrawler.Admin.ashxHelp;
using DataCrawler.BLL.Hankook;
namespace WebCrawler.hankookAshx
{
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : BaseHandler
    {
        bool b = false;
        int projectId = -1;
        int permissions = -1;
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request.Form["username"].ToString();
            string pwd = context.Request.Form["pwd"].ToString();
            string code = context.Request.Form["code"].ToString();
            string sessionCode = context.Session["code"].ToString();

            string loginMess = LoginResult(username, pwd, code, sessionCode);
            if (b)
            {
                context.Session["loginname"] = username;
                context.Session["projectId"] = projectId; //项目
                context.Session["permissions"] = permissions;//权限
            }
            context.Response.Write(loginMess);
        }

        public string LoginResult(string username, string pwd, string code, string sessionCode)
        {
            string loginResult = "";
            if (code == sessionCode)
            {
                //EmployeeBLL
                int result = new CustomerBLL().GetCustomerByCustomerIdManager(username, pwd, ref projectId, ref permissions);
                if (result == 1)
                {
                    b = true;
                 
                    //记下用户的 权限,还有项目Id
                   
                    loginResult = "登录成功";
                }
                else if (result == 3)
                {
                    loginResult = "登录失败,账号被冻结";
                }
                else
                {
                    b = false;
                    loginResult = "用户名和密码不匹配";
                }
            }
            else
            {
                loginResult = "验证码错误";
            }

            return loginResult;
        }
    }
}
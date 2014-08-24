using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin.ashxHelp
{
    /// <summary>
    /// HandlerLogin 的摘要说明
    /// </summary>
    public class HandlerLogin : BaseHandler
    {
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
            }
            context.Response.Write(loginMess);
        }

        bool b = false;
        public string LoginResult(string username,string pwd,string code,string sessionCode)
        {
            string loginResult = "";
            if (code == sessionCode)
            {
                //EmployeeBLL
                int result = new EmployeeBLL().LoginActionManager(username, pwd);

                
                if (result==1)
                {
                    b = true;
                    loginResult = "登录成功";
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
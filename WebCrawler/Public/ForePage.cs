using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Diagnostics;

namespace WebCrawler.Public.UI
{
    /// <summary>
    /// 舆情监控系统 页面基类
    /// </summary>
    public class ForePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (Session["loginname"] != null)
            {

            }
            else
            {
               // Session["loginname"] = "admin";
               //Session["projectId"] = 4;
                 Response.Redirect("index.aspx");
            }
            base.OnLoad(e);
        }


        /// <summary>
        ///  初始化函数。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                ViewStateUserKey = Session.SessionID;
            }
            catch
            {
              
            }
        }


    }
}
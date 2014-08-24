using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.IO;
using DataCrawler.BLL.Crawler;
using DataCrawler.Model.Crawler;



namespace WebCrawler
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
    

        public Global()
        {
            InitializeComponent();

            ModelArgs.RC_Forum_CateId = int.Parse(ConfigurationManager.AppSettings["RC_Forum_CateId"]);
            ModelArgs.RC_News_CateId = int.Parse(ConfigurationManager.AppSettings["RC_News_CateId"]);
            ModelArgs.RC_Blog_CateId = int.Parse(ConfigurationManager.AppSettings["RC_Blog_CateId"]);
        }	
		
        protected void Application_Start(object sender, EventArgs e)
        {
          
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 20;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
         
          
        }

        #region Web 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion
    }
}
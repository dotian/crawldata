using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using RionSoft.IBRS.Core.Util;

namespace Herbalife.HelpDesk.UI.Public
{
	/// <summary>
	/// Warning 的摘要说明。
	/// </summary>
	public partial class Warning : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
				LoadErrorMessage();
				this.detail.Visible=false;
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void showdetail_ServerClick(object sender, System.EventArgs e)
		{
			if(this.detail.Visible)
			{
				this.showdetail.Value = "显示详细 ▼";
				this.detail.Style["display"] = "none";
				this.detail.Visible=false;
			}
			else
			{
				this.showdetail.Value = "隐藏详细 ▲";
				this.detail.Style["display"] = "";
				txtMessage.Height = 200;
				this.detail.Visible=true;
			}
		}

		private void LoadErrorMessage()
		{
			string msgcode = Request.QueryString["msgcode"];
			if (!IBRSStringUtil.IsEmpty(msgcode))
			{
				SetErrorLevel(LogLevel.Warning);
				lblError.Text = IBRSConfigManager.GetInstance().GetMessage(msgcode);
				txtMessage.Text = IBRSConfigManager.GetInstance().GetMessage(msgcode);
			}
			else if (Session["exception"] != null && Session["exception"] is System.Exception)
			{
				Exception ex = (Exception)Session["exception"];
				if (ex is IBRSException)
				{
					SetErrorLevel((ex as IBRSException).ErrorLevel);
				}
				else
				{
					SetErrorLevel(LogLevel.Error);
				}
				lblError.Text = ex.Message;
				txtMessage.Text = ex.ToString();
			}
			Response.Write("<script language='javascript'>if(parent && parent.EnableColse){parent.EnableColse();}</script>");
		}

		private void SetErrorLevel(LogLevel errorLevel)
		{
			switch(errorLevel)
			{
				case LogLevel.Debug:
					lblErrorLevel.Text = "调试";
					showdetail.Visible = false;
					break;
				case LogLevel.Infomation:
					lblErrorLevel.Text = "信息";
					showdetail.Visible = false;
					break;
				case LogLevel.Warning:
					lblErrorLevel.Text = "警告";
					showdetail.Visible = false;
					Session["redirect"] = System.Configuration.ConfigurationManager.AppSettings["LogoutAdd"];
					break;
				case LogLevel.Error:
					lblErrorLevel.Text = "错误";
					showdetail.Visible = true;
					break;
				case LogLevel.Fatal:
					lblErrorLevel.Text = "致命错误";
					showdetail.Visible = true;
					break;
				default:
					lblErrorLevel.Text = "信息";
					showdetail.Visible = false;
					break;
			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (Session["exception"] != null
				&& Session["exception"] is IBRSException
				&& (Session["exception"] as IBRSException).ErrorLevel == LogLevel.Warning)
			{
				// 表示登录失败（临时解决方案）
                string logoutAdd = System.Configuration.ConfigurationManager.AppSettings["LogoutAdd"] + "?applicationname=" + System.Configuration.ConfigurationManager.AppSettings["ApplicationID"];
				Response.Write("<script>if(window.opener) window.opener.parent.location.href='"+logoutAdd+"'; else window.parent.location.href='"+logoutAdd+"';</script>");
			}
			else if(Session["redirect"] != null)
			{
				Response.Redirect(Session["redirect"].ToString());
			}
		}
	}
}

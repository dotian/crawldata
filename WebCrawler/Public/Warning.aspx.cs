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
	/// Warning ��ժҪ˵����
	/// </summary>
	public partial class Warning : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				LoadErrorMessage();
				this.detail.Visible=false;
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void showdetail_ServerClick(object sender, System.EventArgs e)
		{
			if(this.detail.Visible)
			{
				this.showdetail.Value = "��ʾ��ϸ ��";
				this.detail.Style["display"] = "none";
				this.detail.Visible=false;
			}
			else
			{
				this.showdetail.Value = "������ϸ ��";
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
					lblErrorLevel.Text = "����";
					showdetail.Visible = false;
					break;
				case LogLevel.Infomation:
					lblErrorLevel.Text = "��Ϣ";
					showdetail.Visible = false;
					break;
				case LogLevel.Warning:
					lblErrorLevel.Text = "����";
					showdetail.Visible = false;
					Session["redirect"] = System.Configuration.ConfigurationManager.AppSettings["LogoutAdd"];
					break;
				case LogLevel.Error:
					lblErrorLevel.Text = "����";
					showdetail.Visible = true;
					break;
				case LogLevel.Fatal:
					lblErrorLevel.Text = "��������";
					showdetail.Visible = true;
					break;
				default:
					lblErrorLevel.Text = "��Ϣ";
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
				// ��ʾ��¼ʧ�ܣ���ʱ���������
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

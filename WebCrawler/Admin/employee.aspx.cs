using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Text;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using LogNet;
using System.Configuration;

namespace WebCrawler.Admin
{
    public partial class employee : WebCrawler.Public.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        EmployeeBLL BllManager = new EmployeeBLL();
        public void Bind()
        {
            try
            {
                this.txt_empId.Value = "";
                this.txt_empName.Value = "";
                this.sel_permissions.Value = "1";
                this.txt_createdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                List<Employee> list = BllManager.GetEmployeeListManager();
                this.rep_empIdList.DataSource = list;
                this.rep_empIdList.DataBind();
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("employee.aspx Bind", ex);
            }
        }
        protected void btn_select_Click(object sender, EventArgs e)
        {
            string searchkey = this.txt_selectKey.Value.Trim();
            int searchType = int.Parse(this.sel_empType.Value);

            if (searchkey == "")
            {
                Bind();
            }
            else
            {
                List<Employee> list = BllManager.GetEmployeeListBySearchManager(searchkey, searchType);
                this.rep_empIdList.DataSource = list;
                this.rep_empIdList.DataBind();
            }

        }


        protected void btn_addEmp_onclick(object sender, EventArgs e)
        {
            //新增账号
            try
            {


                int num = int.Parse(this.hid_valedate.Value);
                if (num == 1)
                {
                    if (Session["loginname"].ToString() != "admin")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('用户权限不足,请使用管理员账号操作!');", true);
                        return;
                    }
                    string userName = this.txt_empId.Value.Trim();
                    string pwd = this.txt_newpwd.Value.Trim();
                    string empName = this.txt_empName.Value.Trim();
                    int permission = int.Parse(this.sel_permissions.Value);
                    int result = new EmployeeBLL().AddAccountManager(userName, pwd, empName, permission);
                    if (result == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('添加帐号失败,该帐号已存在!');", true);
                    }
                    else if (result == 1)
                    {

                        Bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('添加成功!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('添加帐号失败!!');", true);
                    }
                }

            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("employee.aspx btn_addEmp_onclick", ex);
            }
        }


        protected void btn_updateEmp_onclick(object sender, EventArgs e)
        {
            //修改账号
            try
            {
                int num = int.Parse(this.hid_valedate.Value);
                if (num == 1)
                {
                    if (Session["loginname"].ToString() != "admin")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('用户权限不足,请使用管理员账号操作!');", true);
                        return;
                    }
                    string userName = this.txt_empId.Value.Trim();
                    string pwd = this.txt_newpwd.Value.Trim();
                    string empName = this.txt_empName.Value.Trim();
                    int permission = int.Parse(this.sel_permissions.Value);

                    int result = new EmployeeBLL().UpdateAccountManager(userName, pwd, empName, permission);
                    if (result == 1)
                    {
                        Bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('修改成功!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('修改失败!');", true);
                    }
                }

            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("employee.aspx btn_updateEmp_onclick", ex);
            }
        }
        protected void btn_deleteEmp_onclick(object sender, EventArgs e)
        {
            //删除账号
            try
            {
                int num = int.Parse(this.hid_valedate.Value);
                if (num == 1)
                {
                    if (Session["loginname"].ToString() != "admin")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('用户权限不足,请使用管理员账号操作!');", true);
                        return;
                    }
                    string userName = this.txt_empId.Value.Trim();
                    int result = new EmployeeBLL().DeleteAccountManager(userName);
                    if (result == 1)
                    {
                        Bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('删除成功!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('删除失败!');", true);
                    }
                    Bind();
                }
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("employee.aspx btn_deleteEmp_onclick", ex);
            }
        }


        public string FormatPromession(string promession)
        {
            if (promession == "1")
            {
                return "后台低级用户";
            }
            else
            {
                return "后台高级用户";
            }
        }

    }
}
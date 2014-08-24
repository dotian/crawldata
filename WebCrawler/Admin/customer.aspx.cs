using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;

namespace WebCrawler.Admin
{
    public partial class customer : WebCrawler.Public.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepInit();
                Bind();
            }
        }
        EmployeeBLL BllManager = new EmployeeBLL();
        public void Bind()
        {
            try
            {
                this.sel_employee.Items.Clear();
                //查询出select 的后台账号

                this.txt_customerId.Value = "";
                this.txt_createdate.Value = DateTime.Now.ToString("yyyy-MM-dd");

                List<Employee> list = BllManager.GetEmployeeListManager();
                this.sel_employee.DataTextField = "EmpName";
                this.sel_employee.DataValueField = "EmpId";
                this.sel_employee.DataSource = list;
                this.sel_employee.DataBind();
                this.sel_employee.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("customer.aspx  Bind", ex);
            }


        }

        private void BindRepInit()
        {
            try
            {
                List<EmployeeCustomer> listEc = BllManager.GetEmployeeCustomerListManager(0, "");
                this.rep_empCustomerList.DataSource = listEc;
                this.rep_empCustomerList.DataBind();
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("customer.aspx  BindRepInit", ex);
            }

        }


        protected void btn_select_Click(object sender, EventArgs e)
        {
            try
            {
                string searchkey = this.txt_selectKey.Value.Trim();
                int searchType = int.Parse(this.sel_empType.Value);

                List<EmployeeCustomer> listEc = BllManager.GetEmployeeCustomerListManager(searchType, searchkey);
                this.rep_empCustomerList.DataSource = listEc;
                this.rep_empCustomerList.DataBind();
            }
            catch (Exception ex)
            {

                LogNet.LogBLL.Error("customer.aspx  btn_select_Click", ex);
            }


        }
        protected void btn_addCustomer_onclick(object sender, EventArgs e)
        {

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
                    string customerId = this.txt_customerId.Value.Trim();
                    string pwd = this.txt_newpwd.Value.Trim();
                    int selectIndex = this.sel_employee.SelectedIndex;
                    string empName = this.sel_employee.Items[selectIndex].Text;
                    string empId = this.sel_employee.Items[selectIndex].Value;
                    int permission = int.Parse(this.sel_permissions.Value);

                    int result = new EmployeeBLL().AddEmployeeCustomerManager(customerId, pwd, empName, permission, empId);

                    if (result == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('添加失败,账号已存在!');", true);
                    }
                    else if (result == 1)
                    {
                        this.txt_customerId.Value = "";
                        BindRepInit();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('添加成功!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('添加失败!');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("btn_addCustomer_onclick ", ex);
            }
        }

        protected void btn_updateCustomer_onclick(object sender, EventArgs e)
        {
        }
        protected void btn_deleteCustomer_onclick(object sender, EventArgs e)
        {
        }

        public string FormatPromession(string promession)
        {
            if (promession == "3")
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
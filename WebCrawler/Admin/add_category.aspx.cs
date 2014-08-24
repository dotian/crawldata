using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataCrawler.Model.Crawler;
namespace WebCrawler.Admin
{
    public partial class add_category : WebCrawler.Public.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(add_category));
            if (!IsPostBack)
            {
                if (Session["loginname"] != null)
                {
                    this.txt_EmpName.Value = Session["loginname"].ToString();
                }
                else
                {
                        //记下当前页面
                    Session["LoginAfterUrl"] = "add_category.aspx";
                   // Response.Redirect("login.aspx");//调回到登录页面
                }
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public int addCategory_click(string cateName,string empId,int classId)
        {
            try
            {
                int categoryId = new DataCrawler.BLL.Crawler.CategoryBLL().InsertCategoryManager(cateName, empId, classId);
                //(int)EventSubmit(typeof(DataCrawler.BLL.Crawler.CategoryBLL).FullName, "InsertCategoryManager", new object[] { cateName, empId, classId });
                return categoryId;
            
            }
            catch
            {
                return 0;
            }
           
            
                
        }


    }
}
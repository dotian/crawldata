using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace WebCrawler.Admin
{
    public partial class taglist  : Public.UI.Page
    {
        Help_CreateSiteDataTB createHtmlAction = new Help_CreateSiteDataTB();
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(taglist));
            if (!IsPostBack)
            {
                HttpCookie cookie = new HttpCookie("pid");
                cookie.Value = null;
                BindProjectList();
                //绑定标签
                red.InnerHtml = createHtmlAction.CreateTagList();
            }
            //绑定项目标签
            blue.InnerHtml = createHtmlAction.CreateProjectTagList();
        }
      
        protected void BindProjectList()
        {
            try
            {
                List<ProjectTagRelation> list = new TagInfoBLL().GetProjectByRunStatusManager();
                this.sel_projectList.Items.Clear();
                this.sel_projectList.DataSource = list;
                sel_projectList.DataValueField = "ProjectId";
                sel_projectList.DataTextField = "ProjectName";
                this.sel_projectList.DataBind();
                this.sel_projectList.Items.Insert(0, new ListItem("---请选择---", ""));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public int AllotProjectTag(int projectId, string tagIds)
        {
                    //现在有了限制,只允许出现 最多6个 大类
             return new TagInfoBLL().AllotProjectTagManager(projectId, tagIds);
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string Get_blueInnerHtml()
        {
            return createHtmlAction.CreateProjectTagList();
        }

         [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string GetAllTagByProjectId(int pojectId)
        {
            List<TagList> list = new TagInfoBLL().GetBatchTagByProjectIdManager(pojectId);
            List<int> listId = new List<int>();
            foreach (TagList item in list)
            {
                listId.Add(item.Id);
            }
            //拼接得到
            //返回 json
            string jsonStr = "";
            jsonStr = JsonConvert.SerializeObject(listId);
            return jsonStr;
        }

    }
}
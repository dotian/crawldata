using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using DataCrawler.Model.Crawler;
using DataCrawler.BLL.Crawler;
using DataCrawler.Model.Hankook;
using DataCrawler.BLL.Hankook;


using Newtonsoft.Json;

namespace WebCrawler
{
    public partial class view : WebCrawler.Public.UI.ForePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ( Session["ProjectId"]==null)
                {
                    Response.Redirect("index.aspx");
                }
                this.txt_start.Value = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
                this.txt_end.Value = DateTime.Now.ToString("yyyy-MM-dd");
               
                Bind();  
            }
        }

      
        public void Bind()
        {
            ReportBLL reportBll = new ReportBLL();

            string startdate = this.txt_start.Value.Trim();
            string enddate = this.txt_end.Value.Trim();

            int pojectId = int.Parse(Session["ProjectId"].ToString());

            List<TopicReport> reportList_htsqlypm = reportBll.GetReport_htsqlypmManager(startdate, enddate, pojectId);
            this.rep_htsqlypm.DataSource = reportList_htsqlypm;
            this.rep_htsqlypm.DataBind();

            List<TopicReport> reportList_fmhtsqlypm = reportBll.GetReport_fmhtsqlypmManager(startdate, enddate, pojectId);
            this.rep_fmhtsqlypm.DataSource = reportList_fmhtsqlypm;
            this.rep_fmhtsqlypm.DataBind();

            List<TopicReport> reportList_cfhtpm = reportBll.GetReport_cfhtpmManager(startdate, enddate, pojectId);
            this.rep_cfhtpm.DataSource = reportList_cfhtpm;
            this.rep_cfhtpm.DataBind();

            List<TopicReport> reportList_fmcfhtpm = reportBll.GetReport_fmcfhtpmManager(startdate, enddate, pojectId);
            this.rep_fmcfhtpm.DataSource = reportList_fmcfhtpm;
            this.rep_fmcfhtpm.DataBind();


            List<PieData> list_gmjslly = reportBll.GetReport_gmjsllyManager(startdate, enddate, pojectId);
            this.txt_columData_gmjslly.Value = JsonConvert.SerializeObject(list_gmjslly);

            List<PieData> list_gdxhtsl = reportBll.GetReport_gdxhtslManager(startdate, enddate, pojectId);
            this.txt_columData_gdxhtsl.Value = JsonConvert.SerializeObject(list_gdxhtsl);


            List<HuaTiInfo> list_htslqst = reportBll.GetReport_htslqstManager(startdate, enddate, pojectId);
            this.txt_columData_htslqst.Value = JsonConvert.SerializeObject(list_htslqst);

            List<HuaTiInfo> list_fmhtslqst = reportBll.GetReport_fmhtslqstManager(startdate, enddate, pojectId);
            this.txt_columData_fmhtslqst.Value = JsonConvert.SerializeObject(list_fmhtslqst);

            List<HTQSB_Info>list_htqsb = reportBll.GetReport_htqsbManager(startdate, enddate, pojectId);
            this.rep_htqsb.DataSource = list_htqsb;
            this.rep_htqsb.DataBind();

            List<HTQSB_Z_Info> List_htqst_z = new List<HTQSB_Z_Info>();
            List<HTQSB_F_Info> List_htqst_f = new List<HTQSB_F_Info>();

            foreach (HTQSB_Info item in list_htqsb)
            {
                HTQSB_Z_Info z_info = new HTQSB_Z_Info();
                z_info.ContentDate = item.ContentDate;
                z_info.News_Z_Num = item.News_Z_Num;
                z_info.Blog_Z_Num = item.Blog_Z_Num;
                z_info.Forum_Z_Num = item.Forum_Z_Num;
                List_htqst_z.Add(z_info);


                HTQSB_F_Info f_info = new HTQSB_F_Info();
                f_info.ContentDate = item.ContentDate;
                f_info.News_F_Num = item.News_F_Num;
                f_info.Blog_F_Num = item.Blog_F_Num;
                f_info.Forum_F_Num = item.Forum_F_Num;
                f_info.Microblog_F_Num = item.Microblog_F_Num;
                List_htqst_f.Add(f_info);
            }

            this.txt_columData_zmhtqsb.Value = JsonConvert.SerializeObject(List_htqst_z);
            this.txt_columData_fmhtqsb.Value = JsonConvert.SerializeObject(List_htqst_f);

        }

     
        protected void btn_Query_click(object sender, EventArgs e)
        {
            //查询
            Bind();
        }

        protected void btn_Back_click(object sender, EventArgs e)
        {
            //返回

            Response.Redirect("customer.aspx");
        }

        public string IsSubString(string s, int size)
        {
            // 绑定的时候这样绑定
            // <%# IsSubString(DataBinder.Eval(Container.DataItem, "SiteUrl").ToString(), 20)%>
            return s.Length >= size ? s.Substring(0, size) + "..." : s;
        }

        public string IsSubStr(string strShow, int length, int trip)
        {
            string show = strShow.Length > length ? strShow.Substring(0, length - trip) + "..." : strShow;
            return show.Replace("\"", "&qout;").Replace("'", "&qout;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
        
    }
}
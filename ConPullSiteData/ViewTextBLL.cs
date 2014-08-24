using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using RionSoft.IBRS.Business.DAL;

namespace ConPullSiteData
{
    public class ViewTextBLL
    {
        public void ViewWorkManager()
        {

            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "SELECT [Name] FROM sys.views ";
            DataTable dt = serverDal.SelectData(sql);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            StringBuilder sbulider = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string helptext = GetViewStr(row["Name"].ToString());
                sbulider.Append(helptext);
            }

            File.WriteAllText(@"D:\发布网站\发布View\view.sql", sbulider.ToString(), Encoding.UTF8);
            Console.WriteLine("视图发布完毕");

        }


        private string sqlViewHeader = @"
  /****** Object:  View [dbo].[@VIEW]    Script Date: 01/21/2014 11:18:14 ******/
 IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[@VIEW]'))
 DROP VIEW [dbo].[@VIEW]
 GO" + "\n";

        public string SqlViewHeader
        {
            get { return sqlViewHeader; }
            set { sqlViewHeader = value; }
        }

        private static string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");




        public string GetViewStr(string viewName)
        {
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(SqlViewHeader.Replace("@VIEW", viewName).Replace("@DateTime", datetime));
            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "exec sp_helptext '" + viewName + "'";

            DataTable dt = serverDal.SelectData(sql);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return "";
            }

            foreach (DataRow row in dt.Rows)
            {
                sbulider.Append(row["Text"].ToString());
            }
            sbulider.Append("GO\n");

            return sbulider.ToString();
        }

    }
}

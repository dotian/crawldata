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
    public class TableTextBLL
    {
        public void TableWorkManager()
        {

            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "select [Name] from dbo.sysobjects where OBJECTPROPERTY(id, N'IsUserTable') = 1 order by name ";
            DataTable dt = serverDal.SelectData(sql);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            StringBuilder sbulider = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string helptext = GetTableStr(row["Name"].ToString());
                sbulider.Append(helptext);
            }

            File.WriteAllText(@"D:\发布网站\发布Table\table.sql", sbulider.ToString(), Encoding.UTF8);
            Console.WriteLine("表发布完毕");
        }


        private string sqlTableHeader = @"
 /****** Object:  Table [dbo].[@Table]    Script Date: @DateTime ******/
 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[@Table]') AND type in (N'U'))
 DROP TABLE [dbo].[@Table]
 GO" + "\n";

        public string SqlTableHeader
        {
            get { return sqlTableHeader; }
            set { sqlTableHeader = value; }
        }

        private static string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");




        public string GetTableStr(string viewName)
        {
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(SqlTableHeader.Replace("@Table", viewName).Replace("@DateTime", datetime));
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

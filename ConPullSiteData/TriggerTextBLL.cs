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
    public class TriggerTextBLL
    {
        public void TriggerWorkManager()
        {

            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "SELECT [Name] FROM sys.triggers ";
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

            File.WriteAllText(@"D:\发布网站\发布Trigger\trigger.sql", sbulider.ToString(), Encoding.UTF8);
            Console.WriteLine("触发器发布完毕");

        }


        private string sqlTriggerHeader = @"
 /****** Object:  Trigger [@Trigger]    Script Date: @DateTime ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[@Trigger]'))
DROP TRIGGER [dbo].[@Triggert]
GO" + "\n";

        public string SqlTriggerHeader
        {
            get { return sqlTriggerHeader; }
            set { sqlTriggerHeader = value; }
        }

        private static string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");




        public string GetViewStr(string viewName)
        {
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(SqlTriggerHeader.Replace("@Trigger", viewName).Replace("@DateTime", datetime));
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

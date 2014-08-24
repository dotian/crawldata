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
    public class ProcTextBLL
    {

        public void ProcWorkManager()
        {

            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "select [Name] from dbo.sysobjects where OBJECTPROPERTY(id, N'IsProcedure') = 1 order by name";
            DataTable dt = serverDal.SelectData(sql);

            if (dt==null||dt.Rows.Count<=0)
            {
                return;
            }
            StringBuilder sbulider = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string helptext = GetProcStr(row["Name"].ToString());
                sbulider.Append(helptext);
            }

            File.WriteAllText(@"D:\发布网站\发布Proc\proc.sql", sbulider.ToString(),Encoding.UTF8);
            Console.WriteLine("存储过程发布完毕");

        }


        private string sqlprocHeader = @"
 /****** Object:  StoredProcedure [dbo].[@ProcName]    Script Date: @DateTime ******/
 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[@ProcName]') AND type in (N'P', N'PC')) 
 DROP PROCEDURE [dbo].[@ProcName]
 GO"+"\n";

        public string SqlprocHeader
        {
            get { return sqlprocHeader; }
            set { sqlprocHeader = value; }
        }

        private static string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        

        public string GetProcStr(string procName)
        {
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(SqlprocHeader.Replace("@ProcName", procName).Replace("@DateTime",datetime));
            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "exec sp_helptext '" + procName + "'";

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

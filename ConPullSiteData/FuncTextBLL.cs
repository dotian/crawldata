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
    public class FuncTextBLL
    {
        public void FuncWorkManager()
        {

            IBRSCommonDAL serverDal = new IBRSCommonDAL();
            string sql = "SELECT [Name] FROM sys.objects WHERE  type in (N'FN', N'IF', N'TF', N'FS', N'FT')";
            DataTable dt = serverDal.SelectData(sql);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            StringBuilder sbulider = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string helptext = GetFuncStr(row["Name"].ToString());
                sbulider.Append(helptext);
            }

            File.WriteAllText(@"D:\发布网站\发布Func\func.sql", sbulider.ToString(), Encoding.UTF8);
            Console.WriteLine("函数发布完毕");

        }


        private string sqlFuncHeader = @"
 /****** Object:  UserDefinedFunction [dbo].[@FUNC]    Script Date: @DateTime ******/
 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[@FUNC]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
 DROP FUNCTION [dbo].[@FUNC]
 GO" + "\n";

        public string SqlFuncHeader
        {
            get { return sqlFuncHeader; }
            set { sqlFuncHeader = value; }
        }

        private static string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");




        public string GetFuncStr(string viewName)
        {
            StringBuilder sbulider = new StringBuilder();
            sbulider.Append(SqlFuncHeader.Replace("@FUNC", viewName).Replace("@DateTime", datetime));
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

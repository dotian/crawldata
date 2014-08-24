using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ConGetData.DAL
{
    public static class SystemConst
    {
        private static string connStr;
        public static string ConnStr
        {
            get
            {
                return connStr ?? ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }
            set
            {
                connStr = value;
            }
        }

        private static string strCookie;
        public static string StrCookie
        {
            get
            {
                return strCookie ?? ConfigurationManager.AppSettings["MicroblogCookie"].ToString();
                //return strCookie ?? ConfigurationManager.ConnectionStrings["MicroblogCookie"].ToString();
            }
            set { strCookie = value; }
        }
    }
}

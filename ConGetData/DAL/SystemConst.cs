using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using ConGetData.BLL;

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

        private static string strSinaCookie;
        public static string StrSinaCookie
        {
            get
            {
                return strSinaCookie ?? ConfigurationManager.AppSettings["MicroblogCookie"].ToString();
            }
            set { strSinaCookie = value; }
        }

        private static string strTencentCookie;
        public static string StrTencentCookie
        {
            get
            {
                if (tencentCookieUpdateTime.AddHours(24) < DateTime.Now)
                {
                    //strTencentCookie = HttpHelper.GetTencentCookie();
                }

                return strTencentCookie;
            }
            set { strTencentCookie = value; }
        }

        private static DateTime tencentCookieUpdateTime;
    }
}

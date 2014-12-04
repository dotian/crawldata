using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WriteSpiderAgain.ServiceDAL
{
    public static class SystemConst
    {
        private static string connStr;
        public static string ConnStr
        {
            get {
                return connStr?? ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }
            set
            {
                connStr = value;
            }
        }
    }
}

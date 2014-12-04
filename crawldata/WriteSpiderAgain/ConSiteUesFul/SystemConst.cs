using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WriteSpiderAgain.ServiceDAL
{
    public static class SystemConst
    {
        public static string ConnStr
        {
            get {
                return ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using log4net;

namespace WriteSpiderAgain
{
    public class Page
    {
        public static ILog log = LogManager.GetLogger("MyLogger");

        public static void InitLog()
        {
            XmlConfigurator.Configure();
        }
        
    }
}

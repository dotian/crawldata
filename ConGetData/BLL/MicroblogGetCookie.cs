using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.IO;
using ConGetData.DAL;
using ConGetData.ConMicroblog;

namespace ConGetData.BLL
{
    public  class MicroblogGetCookie
    {
       
        public void GetCookieWork()
        {
            //每隔3小时 登陆一次,看一看cookie
              new WorkManager().Work();
              Console.WriteLine("Microblog Cookie  获取完毕");
              LogNet.LogBLL.Info("Microblog Cookie  获取完毕:\t" + ModelArgs.CookieStr);
              SystemConst.StrCookie = ModelArgs.CookieStr;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.BLL;
using ConGetData.DAL;
using ConGetData.Model;
using System.Text.RegularExpressions;
using ConGetData.ConMicroblog;

namespace TestConsole
{
    class Program
    {
        HttpHelper httpHelper = new HttpHelper();
        CommonHelper commonHelper = new CommonHelper();

        static void Main(string[] args)
        {
            // setup
            ServiceLocator.SetDalType<MockServiceDAL>();

            ConGetData.ConMicroblog.ModelArgs.InitModeArgs();

            // new MicroblogGetCookie().GetCookieWork();
            LogicBLL logicAction = new LogicBLL();
            var targetList = logicAction.GetCrawtarget();

            for (int i = 0; i < targetList.Count; i++)
            {
                CrawlTargetArgs CrawlTargetArgs = new CrawlTargetArgs(new ThreadCounter(), targetList[i]);
                MainWork.CrawlWorkAction(CrawlTargetArgs);
            }
        }

        private void testHttpHelper()
        {
            var result = httpHelper.Open("http://club.autohome.com.cn/bbs/thread-c-692-31469545-1.html", "gb2312");
        }
    }
}

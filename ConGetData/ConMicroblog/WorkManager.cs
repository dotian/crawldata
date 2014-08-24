using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
namespace ConGetData.ConMicroblog
{
    public class WorkManager
    {

        public void Work()
        {
            try
            {
                Console.WriteLine("开始获取Microblog Cookie,请等待...");
                Close();
                Thread.Sleep(5000);
                Start();
                Thread.Sleep(ModelArgs.SleepTime);
                Close();
                GetCookie();
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }



        public void Start()
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            Info.WorkingDirectory = ModelArgs.ProcessWorkingDirectory;
            Info.FileName = ModelArgs.ProcessFileName;


            Info.WindowStyle = ModelArgs.ProcessWindowStyle;
            System.Diagnostics.Process Proc;

            try
            {
                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch
            {
            }
        }

        public void Close()
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process item in ps)
                {
                    //Console.WriteLine(item.ProcessName);
                    if (item.ProcessName == "WinGetMocroblogCookie")
                    {
                        item.Kill();
                    }
                }
            }
            catch
            {
            }
        }

        List<string> list = new List<string>();
        public void GetCookie()
        {

            string cookiePath = ModelArgs.ProcessWorkingDirectory + "\\" + ModelArgs.WriteFilePath;
            string cookie = File.ReadAllText(cookiePath);
          
            bool b = list.Contains(cookie);
            if (!b&&  cookie!="")
            {
                list.Add(cookie);
                ModelArgs.CookieStr = cookie;
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":\t" + list.Count);

        }
    }
}

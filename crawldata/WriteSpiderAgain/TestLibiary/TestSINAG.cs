using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ZTestLibiary
{
    class TestSINAG
    {
        string t_url = "http://www.tfengyun.com/monitor.php?action=topic_content&topic=%E4%BA%AC%E4%B8%9C";
        public string t_cookie = "saeut=180.159.3.163.1398699000516454; PHPSESSID=9845d5f49da9aa10cb8511d454164d9e; ck_member_id=41623; ck_member_uid=3087668437; ck_member_loginid=wbfy369996; ck_member_level=0; ck_member_screen_name=%25E9%2593%25AD%25E4%25BA%25A6%25E5%25B9%25BB; Hm_lvt_2b05fe4bc9d0ca253f9b19c951b547c4=1398699005; Hm_lpvt_2b05fe4bc9d0ca253f9b19c951b547c4=1398699136";

        private static string reqUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; InfoPath.2)";
        public string Open(string url, Encoding encoding)
        {
            string revData = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url).AbsoluteUri);

                req.Headers.Add("Cooker", t_cookie);
                req.UserAgent = reqUserAgent;
                req.Timeout = 10000;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), encoding);
                revData = sr.ReadToEnd();

            }
            catch (Exception e)
            {
                revData = "";
            }
            return revData;
        }


        public void test()
        {
            string message = Open(t_url, Encoding.UTF8);
            File.WriteAllText("T_code.txt",message);
            Console.WriteLine(message.Length);


        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Threading;
using System.IO.Compression;

namespace ConGetData.BLL
{
    public class HttpHelper
    {
        private static string reqUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; InfoPath.2)";
        public string StrCookie = "";
        public string Open(string url, string encoStr)
        {
            string revData = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.UserAgent = reqUserAgent;
                req.Referer = url;
                req.Headers.Add("Cookie", StrCookie);
                req.Timeout = 10000;

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                bool isGzip = false;

                if (res.Headers.AllKeys.Any(k => k == "Content-Encoding"))
                {
                    var encoding = res.Headers["Content-Encoding"];
                    isGzip = encoding.ToLower() == "gzip";
                }

                //var charset = res.Headers["Content-Type"].Split(';').First(h => h.ToLower().Contains("charset"));
                //encoStr = charset.Substring(charset.IndexOf("=") + 1);

                Stream responseStream = res.GetResponseStream();

                if (isGzip)
                {
                    responseStream = new GZipStream(res.GetResponseStream(), CompressionMode.Decompress);
                }

                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding(encoStr));
                revData = sr.ReadToEnd();
                sr.Close();
                res.Close();
            }
            catch
            {
                revData = "";
            }

            return revData;
        }


        public bool Open(string url, Encoding encoding, ref string revData)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url).AbsoluteUri);
                req.UserAgent = reqUserAgent;
                req.Referer = "";
                req.Headers.Add("Cookie", StrCookie);
                req.Timeout = 10000;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), encoding);
                revData = sr.ReadToEnd();
                return true;
            }
            catch (Exception e)
            {
                revData = e.Message;
            }
            return false;

        }




        public void test()
        {
            string url = "http://www.mobiledigit.net/bbs/forumdisplay.php?fid=67";
            Console.WriteLine(Open(url, "gb2312"));
            Console.WriteLine("完毕");
        }

        public void tst2()
        {
            string url = "http://www.mobiledigit.net/bbs/forumdisplay.php?fid=67";
            string revdate = "";
            Open(url, Encoding.GetEncoding("gb2312"), ref revdate);
            Console.WriteLine(revdate);
            Console.WriteLine("完毕");
        }



    }
}

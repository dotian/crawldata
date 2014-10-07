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
        private static string reqUserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.124 Safari/537.36";
        public string StrCookie = "";

        public string Open(string url, string encoStr, string postContent = null)
        {
            string revData = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.UserAgent = reqUserAgent;
                req.Referer = url;

                if (!string.IsNullOrEmpty(StrCookie))
                {
                    req.Headers.Add("Cookie", StrCookie);
                }

                req.Timeout = 10000;
                var encoding = Encoding.GetEncoding(encoStr);

                if (!string.IsNullOrEmpty(postContent))
                {
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    //req.Headers.Add("Cookie: KtrD_2132_saltkey=cLAW7auJ;"); // POST:http://www.gslzw.com/search.php?searchsubmit=yes
                    byte[] data = encoding.GetBytes(postContent);
                    using (Stream stream = req.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                bool isGzip = false;

                if (res.Headers.AllKeys.Any(k => k == "Content-Encoding"))
                {
                    var responseEncoding = res.Headers["Content-Encoding"];
                    isGzip = responseEncoding.ToLower() == "gzip";
                }

                //var charset = res.Headers["Content-Type"].Split(';').First(h => h.ToLower().Contains("charset"));
                //encoStr = charset.Substring(charset.IndexOf("=") + 1);

                Stream responseStream = res.GetResponseStream();

                if (isGzip)
                {
                    responseStream = new GZipStream(res.GetResponseStream(), CompressionMode.Decompress);
                }

                StreamReader sr = new StreamReader(responseStream, encoding);
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
    }
}

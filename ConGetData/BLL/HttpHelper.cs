using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Threading;
using System.IO.Compression;
using LogNet;

namespace ConGetData.BLL
{
    public class HttpHelper
    {
        private static string reqUserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.124 Safari/537.36";
        public string StrCookie = string.Empty;

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

                req.Timeout = 20000;
                var encoding = Encoding.GetEncoding(encoStr);

                if (!string.IsNullOrEmpty(postContent))
                {
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    byte[] data = encoding.GetBytes(postContent);
                    using (Stream stream = req.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                revData = GetResponseContent(res, encoding);
            }
            catch (Exception ex)
            {
                LogBLL.Error("Http error with url: " + url, ex);
                revData = "";
            }

            return revData;
        }

        public static string GetResponseContent(HttpWebResponse res, Encoding encoding)
        {
            bool isGzip = false;

            if (res.Headers.AllKeys.Any(k => k == "Content-Encoding"))
            {
                var responseEncoding = res.Headers["Content-Encoding"];
                isGzip = responseEncoding.ToLower() == "gzip";
            }

            var charset = res.Headers["Content-Type"].Split(';').FirstOrDefault(h => h.Trim().ToLower().StartsWith("charset"));
            if (charset != null)
            {
                encoding = Encoding.GetEncoding(charset.Substring(charset.IndexOf("=") + 1));
            }

            Stream responseStream = res.GetResponseStream();

            if (isGzip)
            {
                responseStream = new GZipStream(res.GetResponseStream(), CompressionMode.Decompress);
            }

            StreamReader sr = new StreamReader(responseStream, encoding);
            string revData = sr.ReadToEnd();
            sr.Close();
            res.Close();
            return revData;
        }
    }
}

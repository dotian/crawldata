using System;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Threading;
using System.Text.RegularExpressions;


namespace WriteSpiderAgain
{
    /// <summary>
    /// HttpHelper class
    /// </summary>
    public class HttpHelper
    {
        private static string reqUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; InfoPath.2)";
        public bool Open(string url, Encoding encoding, ref string revData)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url).AbsoluteUri);
                req.UserAgent = reqUserAgent;
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

    }
}

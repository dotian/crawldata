using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;

namespace ConGetData.BLL
{
    public class CommonHelper
    {
        /// <summary>
        /// 得到(.+?)匹配项,匹配到的数据
        /// </summary>
        /// <param name="regexStr"></param>
        /// <param name="inputHtml"></param>
        /// <returns></returns>
        public string GetMatchRegex(string regexStr, string inputHtml)
        {
            if (!string.IsNullOrEmpty(regexStr))
            {
                Regex regex = new Regex(regexStr, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (regex.IsMatch(inputHtml))
                {
                    MatchCollection mc = regex.Matches(inputHtml);
                    if (mc.Count > 0)
                    {
                        return mc[0].Groups[1].Value;
                    }
                }

            }
            return "";
        }




        /// <summary>
        /// 这里是 访问新浪微博 对中文进行转码 使用
        /// </summary>
        /// <param name="encodeStr"></param>
        /// <returns></returns>
        public string StrDecode(string strHTML)
        {
            strHTML = strHTML.Replace(@"\/", "/").Replace(@"\""", "\"").Replace(@"\n", "").Replace(@"\r", "").Replace(@"\t", "");
            string[] temp = strHTML.Split(new string[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder biuilder = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length == 4)
                {
                    temp[i] = ((char)Convert.ToInt32(temp[i], 16)).ToString();
                }
                else if (i > 1)
                {
                    string m = temp[i].Substring(4);
                    temp[i] = ((char)Convert.ToInt32(temp[i].Substring(0, 4), 16)).ToString() + m;
                }
            }

            string result = string.Join("", temp);
            strHTML = Regex.Replace(result, @"\r\n|\n|\t", "", RegexOptions.IgnoreCase);
            return strHTML;
        }

        public string NoHTML(string Htmlstring) //去除HTML标记   
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script.+?</script>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);

            // Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"\r\n|\n|\t", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
            Htmlstring.Replace(@"\r", "").Replace(@"\n", "");
            Htmlstring = Htmlstring.Replace(" ", "");
            return Htmlstring;
        }

        public string GetUrl(string strUrl, string parentUrl)
        {


            string resultUrl = "";
            #region 得到站点Url
            try
            {
                if (strUrl != "")
                {
                    if (strUrl.IndexOf("http://") != -1)
                    {
                        resultUrl = strUrl;
                    }
                    else
                    {
                        if (strUrl.IndexOf('/') == 0)
                        {
                            if (parentUrl.IndexOf('?') > 0)
                            {
                                int count = Regex.Matches(strUrl, "/").Count;
                                int parentCount = Regex.Matches(parentUrl, "/").Count;
                                if (parentCount == 3)
                                {
                                    parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));
                                }
                                if (count > 1)
                                {
                                    parentUrl = parentUrl.Substring(0, parentUrl.IndexOf('?'));
                                    for (int i = 0; i < parentCount - 2; i++)
                                    {
                                        parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));
                                    }
                                }
                                else
                                {
                                    parentUrl = parentUrl.Substring(0, parentUrl.IndexOf('?'));
                                    for (int i = 0; i < count; i++)
                                    {
                                        parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));
                                    }
                                }
                            }
                            else
                            {
                                int count = Regex.Matches(strUrl, "/").Count + 1;
                                for (int i = 0; i < count; i++)
                                {
                                    if (Regex.Matches(parentUrl, "/").Count <= 2)
                                    {
                                        break;
                                    }
                                    parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));
                                }
                            }
                            parentUrl = parentUrl + "/";

                            resultUrl = parentUrl + strUrl.Substring(1);
                        }
                        else if (strUrl.IndexOf("../") == 0)
                        {
                            parentUrl = parentUrl.Substring(0, parentUrl.Length - 1);
                            int count = Regex.Matches(strUrl, "../").Count;
                            for (int i = 0; i < count; i++)
                            {
                                parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));

                            }
                            parentUrl = parentUrl + "/";
                            resultUrl = parentUrl + strUrl.Replace("../", "");
                        }
                        else if (strUrl.IndexOf('?') > 0)
                        {
                            if (parentUrl.Contains("?"))
                            {
                                parentUrl = parentUrl.Substring(0, parentUrl.IndexOf('?'));
                                parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/') + 1);
                            }
                            else
                            {
                                parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/') + 1);
                            }

                            //域名
                            resultUrl = parentUrl + strUrl;
                        }
                        else if (parentUrl.Substring(parentUrl.LastIndexOf('/') + 1).IndexOf(".") > 0)
                        {
                            parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/') + 1);
                            //域名
                            resultUrl = parentUrl + strUrl;
                        }
                        else if (strUrl.IndexOf('/') > 0)
                        {
                            //截取域名
                            int count = Regex.Matches(strUrl, "/").Count;
                            for (int i = 0; i < count; i++)
                            {
                                parentUrl = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));
                            }
                            parentUrl = parentUrl + "/";
                            resultUrl = parentUrl + strUrl;
                        }

                        else
                        {
                            resultUrl = parentUrl + strUrl;
                        }
                    }
                }
                else
                {
                    return "";
                }

            }
            catch
            {

                resultUrl = ConvertToAbsoluteURL(parentUrl, strUrl);
            }
            #endregion

            return resultUrl;
        }

        public DateTime GetInnerData(string innerdate)
        {
            DateTime time;
            if (innerdate.Length < 3)
            {
                //格式是 HH:mm
                time = DateTime.Now;
                return time;
            }
            else if (innerdate.Contains("-"))
            {
                // 格式为 MM-dd HH:mm
                if (innerdate.Split('-').Length <= 2)
                {
                    //格式 可能是 MM-dd hh:mm  07-12 07:50 ,我们要 补上年份
                    innerdate = DateTime.Now.ToString("yyyy-") + innerdate;
                    DateTime.TryParse(innerdate, out time);
                }
                DateTime.TryParse(innerdate, out time);
                if (time.Ticks > 100000)
                {
                    return time;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            else if (innerdate.Length >= 10)
            {
                //格式为 yyyy-MM-dd
                DateTime.TryParse(innerdate, out time);
                if (time.Ticks > 100000)
                {
                    return time;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            else
            {
                time = DateTime.Now;
                return time;
            }
        }

        public void Test()
        {
            string strSiteEntry = "http://bbs.ourgame.com/bbs_list.asp?Subject_ID=497";

            string url = "/showtopic-3929209.aspx";

            ///Management/Bi-Ci/1722946-1.html
            //http://bbs.lianzhong.com/showtopic-3939443.aspx

            string urlResult = GetUrl(url, strSiteEntry);
            Console.WriteLine(urlResult);
        }
        /// <summary>
        /// 將可能為相對路徑轉成絕對路徑
        /// </summary>
        /// <param name="baseURL">網站</param>
        /// <param name="checkURL">相對路徑/絕對路徑</param>
        /// <returns></returns>
        public static string ConvertToAbsoluteURL(string baseURL, string checkURL)
        {
            Uri result;
            var res = Uri.TryCreate(new Uri(baseURL, UriKind.Absolute), new Uri(checkURL, UriKind.RelativeOrAbsolute), out  result);
            if (res)
            {
                return result.AbsoluteUri;
            }
            else
            {
                return checkURL;
            }
        }

        /// <summary>
        /// 輸入網址原始碼，透過refex 將所有網址找出來
        /// </summary>
        /// <returns> 所有抓取到的網址 </returns>
        public static string[] GetLinkUrlsFromPath(string htmlSource)
        {
            List<string> res = new List<string>();
            var regex = new Regex(@"(?:href\s*=)(?:[\s""']*)(?!#|mailto|location.|javascript)(?<PARAM1>.*?)(?:[\s>""'])",
                RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(htmlSource);
            foreach (Match match in matches)
            {
                //將href=" 濾掉     
                // res.Add(match.Groups[0].Value.Substring(6, match.Groups[0].Value.Length-7));   
                res.Add(match.Groups["PARAM1"].Value);
            } return res.ToArray();
        }

        /// <summary>
        /// 從網路上取得原始碼
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetSourceFromUrl(string url)
        {
            WebClient client = new WebClient();
            //以防萬一 模擬自己為瀏覽器  
            client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.56 Safari/536.5");
            client.Headers.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            client.Headers.Add("Accept-Encoding: identity");
            client.Headers.Add("Accept-Language: zh-TW,en;q=0.8");
            client.Headers.Add("Accept-Charset: utf-8;q=0.7,*;q=0.3");
            client.Headers.Add("ContentType", "application/x-www-form-urlencoded");
            return client.DownloadString(url);
        }

        public void test21()
        {

            string txtLink = "http://bbs.cq.qq.com/life/17/";

            string str = "t-786971-1.htm";
            Console.WriteLine(ConvertToAbsoluteURL(txtLink, str));

            Console.WriteLine("完毕");
        }


    }
}

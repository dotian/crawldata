using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using WriteSpiderAgain.EntityModel;
using System.Diagnostics;

namespace WriteSpiderAgain
{
   public class CommonHelper
    {
        HttpHelper _hh = new HttpHelper();
       static string CapText(Match m)
       {
           return "";
       }
       
        /// <summary>
        /// 匹配 对应的 节点 
        /// </summary>
        /// <param name="root">xml模版</param>
        /// <param name="noteName">得到节点名称所对应的内容</param>
        /// <param name="inputMatchHtml">待匹配的html</param>
        /// <param name="strPatton">xml模版的节点信息不能精确匹配的时候,strPatton 用于二次匹配的正则表达式</param>
        /// <param name="StrRestlt">返回匹配的结果</param>
        public string GetMatchRegex(string templateRegex, string inputMatchHtml, string strPatton)
        {
            if (!string.IsNullOrEmpty(templateRegex))
            {
                Regex regex = new Regex(templateRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection mc = regex.Matches(inputMatchHtml);
                if (mc.Count > 0)
                {
                    if (strPatton == null || strPatton == "")
                    {
                        //一次匹配 就得到了结果
                        return mc[0].Groups[1].Value;
                    }
                    //需要精确匹配
                    regex = new Regex(strPatton, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    string strInit = mc[0].Groups[0].Value;
                    mc = regex.Matches(strInit);
                    if (mc.Count > 0)
                    {
                        return mc[0].Groups[1].Value.Trim();
                    }
                }
            }
            return "";
        }

       /// <summary>
       /// 得到(.+?)匹配项,匹配到的数据
       /// </summary>
       /// <param name="regexStr"></param>
       /// <param name="inputHtml"></param>
       /// <returns></returns>
        public string GetMatchRegex(string regexStr,string inputHtml)
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

       public string NoHTML(string Htmlstring) //去除HTML标记   
       {
           //删除脚本   
           Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
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

       public void Get()
       {
           string mess = " http://www.cs.com.czq/zqgg/201307/t20130718_4070429.html";
           string result = NoHTML(mess);

           Trace.WriteLine("输出result:\t" + result);
       }
       
    }
}

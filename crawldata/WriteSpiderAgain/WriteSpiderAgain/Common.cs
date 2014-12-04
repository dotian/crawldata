using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using WriteSpiderAgain.EntityModel;

namespace WriteSpiderAgain
{
   public class Common
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

      

       public string NoHTML(string Htmlstring) //去除HTML标记   
       {
           //删除脚本   
           Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
           //删除HTML   
           Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
           Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
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

       public void OptMainNode(TemplateModel templateModel, ref string strSite, string strEncoing)
        {
          
           //这个方法 是用来做什么的呢? 值得考究一下

            string strHTML = "";
            bool bRev = _hh.Open(strSite, Encoding.GetEncoding(strEncoing), ref strHTML);
            if (bRev == false) return;

            strHTML = Regex.Replace(strHTML, "<!--.+?-->", new MatchEvaluator(CapText), RegexOptions.Singleline | RegexOptions.IgnoreCase);

            Regex reg = new Regex(templateModel.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mcMain = reg.Matches(strHTML);
            if (mcMain.Count > 0)
            {
                int forumindex = 0;

                foreach (Match mForum in mcMain)
                {
                    //reg = new Regex(nodeMainPage.Attributes[0].Value, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    reg = new Regex(templateModel.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    MatchCollection mc = reg.Matches(mForum.Groups[1].Value);
                    if (mc.Count == 0)
                        continue;

                    string fsite = "";
                    Regex reg1 = new Regex("(http://.+?)/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    MatchCollection mc1 = reg1.Matches(strSite);
                    if (mc1.Count > 0)
                    {
                        fsite = mc1[0].Groups[1].Value + "/";
                    }

                    string path = "forum" + forumindex + ".txt";
                    forumindex++;
                  
                    fsite += mc[0].Groups[1].Value;
                    strHTML = "";
                    bRev = _hh.Open(fsite, Encoding.GetEncoding(strEncoing), ref strHTML);
                    if (!bRev) continue;

                    reg = new Regex(templateModel.Node, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    mc = reg.Matches(strHTML);
                    if (mc.Count > 0)
                    {
                      
                        System.IO.FileStream f = System.IO.File.Create(path);
                        f.Close();

                        System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding(strEncoing));

                    
                        foreach (Match m in mc)
                        {
                            if (m.Groups[1].Value.Trim().Length == 0) continue;

                          
                            Regex reg2 = new Regex(templateModel.SrcUrlRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            MatchCollection mc2 = reg2.Matches(m.Groups[1].Value);
                            if (mc2.Count > 0) //url
                            {
                              
                            }
                            else
                                continue;

                            reg2 = new Regex(templateModel.TitleRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                            
                            
                            reg2 = new Regex(templateModel.AuthorRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                           
                          
                            reg2 = new Regex(templateModel.ContentDateRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            mc2 = reg2.Matches(m.Groups[1].Value);
                           
                           
                            reg2 = new Regex(templateModel.RepliesRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                         
                           
                            reg2 = new Regex(templateModel.ViewsRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            mc2 = reg2.Matches(m.Groups[1].Value);
                           

                            reg2 = new Regex(templateModel.ContentDateRegex, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            mc2 = reg2.Matches(m.Groups[1].Value);
                         
                        }

                        f2.Close();
                        f2.Dispose();
                    }
                }
            }

        }

    }
}

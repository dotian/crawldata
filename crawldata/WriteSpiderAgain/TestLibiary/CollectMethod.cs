using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WriteSpiderAgain.ManagerBLL;
using WriteSpiderAgain.EntityModel;
using WriteSpiderAgain.ServiceDAL;
using System.Diagnostics;
using WriteSpiderAgain;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ZTestLibiary
{
    /// <summary>
    /// 为了方便测试,提取出来的方法
    /// </summary>
    public class CollectMethod
    {

        ServiceDALAction listDal = new ServiceDALAction();
        OriginalWork originalWork = new OriginalWork();
        HttpHelper _hh = new HttpHelper();
        CommonHelper _ch = new CommonHelper();
    
        /// <summary>
        /// 匹配回复数
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="inputMatchHtml"></param>
        /// <returns></returns>
        public string GetReply(string regex, string inputMatchHtml)
        {
            string strReplies = _ch.NoHTML(_ch.GetMatchRegex(regex, inputMatchHtml));

            if (strReplies.IndexOf('/') >= 0)
            {
                strReplies = strReplies.Substring(0, strReplies.IndexOf('/'));
            }

            return strReplies;
        }

        /// <summary>
        /// 匹配浏览数
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="inputMatchHtml"></param>
        /// <returns></returns>
        public string GetViews(string regex, string inputMatchHtml)
        {
            string strView = _ch.NoHTML(_ch.GetMatchRegex(regex, inputMatchHtml));

            if (strView.IndexOf('/') >= 0)
            {
                strView = strView.Substring(strView.IndexOf('/') + 1);
            }

            return strView;
        }


        /// <summary>
        /// 得到xml
        /// </summary>
        public void GetFormatXml()
        {
            Trace.WriteLine("--GetFormatXml开始--");
            string formatXml = File.ReadAllText("../../TsetFile/formatXml.txt");
            string result = formatXml.Replace("<", "&lt;").Replace(">", "&gt;");
            Trace.WriteLine("输出formatXml:\t" + result);
            Trace.WriteLine("--GetFormatXml完毕--");

        }

        private static string path = @"F:\2013Year\PersonalDevelop\WinApplication\RegxFormTemplate\RegxFormTemplate\Template\";
      

        /// <summary>
        /// 更新模板
        /// </summary>
        public void TestUpdateTemplate(int tid)
        {
            
            string content = File.ReadAllText(path + tid + ".xml");
             
            string sql = " update template set templatecontent = @content,UpdateDate =@updatedate   where tid = " + tid;

            SqlParameter[] parms = new SqlParameter[] { 
             new SqlParameter("content",content),
              new SqlParameter("updatedate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            int result = HelperSQL.ExecNonQuery(sql, parms, System.Data.CommandType.Text);
            Trace.WriteLine("输出 更新模板result:\t" + result);

            UpdateSiteType(tid);
            Trace.WriteLine("--完毕--");
        }


        public CrawlTarget GetUrl()
        {
            string sql = "select top(1) SiteId,SiteUrl,SiteEncoding,Tid from view_Mining_SiteTemplate order by tid asc ";
            CrawlTarget c = new CrawlTarget();

            SqlDataReader reader = null;
            try
            {
                reader = HelperSQL.ExecuteReader(sql);
                while (reader.Read())
                {
                    c.SiteId = Field.GetInt32(reader,"SiteId").ToString();
                    c.SiteEncoding = Field.GetString(reader, "SiteEncoding");
                    c.KeyWords = Field.GetInt32(reader, "Tid").ToString();
                    c.SiteUrl = Field.GetString(reader, "SiteUrl");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception:\t" + ex);
              
            }
            finally
            {
                if (reader!=null)
                {
                    reader.Close();
                }
            }
            return c;
        }

        public int UpdateNoFound(string tid)
        {
            string sql = " update Template set UpdateDate = getdate(),remark = '找不到或者已转移' where tid = " + tid;
            int result = HelperSQL.ExecNonQuery(sql);
            Trace.WriteLine("输出 更新站点UpdateNoFound:\t" + result);
            return result;
        }

        public void test()
        {
            CrawlTarget t = GetUrl();

            Console.WriteLine(t.SiteUrl);
        }



        public  void UpdateSiteType(int tid)
        {
            Trace.WriteLine("--UpdateSiteType开始--");
              string sql = "update SiteList set sitetype = '1' where tid = " + tid;
              int result = HelperSQL.ExecNonQuery(sql);
              Trace.WriteLine("输出 更新站点result:\t" + result);
              Trace.WriteLine("--UpdateSiteType完毕--");
        }



    }
}

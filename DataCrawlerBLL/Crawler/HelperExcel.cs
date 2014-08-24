using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
namespace DataCrawler.BLL.Crawler
{
    public class HelperExcel
    {
        public System.Data.DataTable ExcelToDataTable(string strConn)
        {
            OleDbConnection oleDbConnection = new OleDbConnection(strConn);

            System.Data.DataTable dataTable = new System.Data.DataTable();
            string selectCommandText = "select * from [data$]";
            System.Data.DataTable result = null;
            try
            {
                oleDbConnection.Open();
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, strConn);
                oleDbDataAdapter.Fill(dataTable);

                result = dataTable;
            }
            catch
            {
                throw;
            }
            finally
            {
                oleDbConnection.Close();
                oleDbConnection.Dispose();
            }
            return result;
        }

        public string GetConnStr(string filePath)
        {
            string text = filePath.Substring(filePath.LastIndexOf(".") + 1);
            if (text.ToLower() == "xls")
            {
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            }
            if (text.ToLower() == "xlsx")
            {
                return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
            }
            throw new Exception("文件格式错误或者路径" + filePath + "不存在");
        }


        private void test()
        {
            string path = @"e:\12_03.xlsx";
            string connStr = GetConnStr(path);
            System.Data.DataTable tb = ExcelToDataTable(connStr);
            Console.WriteLine(tb.Rows.Count);

            StringBuilder sbulider = new StringBuilder();
            foreach (DataRow row in tb.Rows)
            {
                string title = row[0].ToString();
                if (title=="")
                {
                    continue;
                }
                string url = row[1].ToString();
                string siteName = row[2].ToString();
                string time = row[3].ToString();
                string analysis = row[4].ToString();
                string tag = row[5].ToString();
                if (analysis.Contains("正"))
                {
                    analysis = "2";
                }
                else if (analysis.Contains("负"))
                {
                    analysis = "3";
                }
                else
                {
                    analysis = "3";
                }
               
                string mess = "";
                if (tag == "品牌评价")
                {
                    mess = s_pppj;
                }
                else if (tag == "静音")
                {
                    mess = s_jy;
                }
                else if (tag == "品牌活动")
                {
                    //品牌活动
                    mess = s_pphd;
                }
                else if (tag == "抓地力")
                {
                    mess = s_zdl;
                }
                else if (tag == "胎噪")
                {
                    mess = s_tz;
                }
                else if (tag == "品牌资讯")
                {
                    mess = s_ppzx;
                }
                else if (tag == "鼓包")
                {
                    mess = s_gb;
                }
                else if (tag == "裂缝")
                {
                    mess = s_lv;
                }
                //Console.WriteLine(url);
                //Console.WriteLine(siteName);
                //Console.WriteLine(time);
                //Console.WriteLine(analysis);
                //Console.WriteLine(tag);

                mess = mess.Replace("#Url", url).Replace("#Title", title).Replace("#time", time).Replace("#SiteName", siteName).Replace("#Analysis", analysis);
                mess = Regex.Replace(mess, @"\r\n|\r|\n", "");
                sbulider.Append(mess + "\r\n");
            }
            Console.WriteLine(sbulider.ToString());
           // File.WriteAllText("11_5.sql",sbulider.ToString());
            Console.WriteLine("完毕");
        }

        /// <summary>
        /// 品牌评价
        /// </summary>
        string s_pppj = " insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'68,','#Title','#time',0,'品牌评价,','%u54C1%u724C%u8BC4%u4EF7,','#SiteName',0)";

        /// <summary>
        /// 品牌资讯
        /// </summary>
        string s_ppzx = " insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'20,','#Title','#time',0,'品牌资讯,','%u54C1%u724C%u8D44%u8BAF,','#SiteName',0)";
     
        /// <summary>
      /// 静音
      /// </summary>
        string s_jy = " insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'115,','#Title','#time',0,'静音,','%u9759%u97F3,','#SiteName',0)";
      /// <summary>
      /// 品牌活动
      /// </summary>
        string s_pphd = " insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'20,','#Title','#time',0,'品牌活动,','%u54C1%u724C%u6D3B%u52A8,','#SiteName',0)";
        /// <summary>
        /// 抓地力
        /// </summary>
        string s_zdl = "insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'4,','#Title','#time',0,'抓地力,','%u6293%u5730%u529B,','#SiteName',0)";
        /// <summary>
        /// 胎噪
        /// </summary>
        string s_tz = "insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'3,','#Title','#time',0,'胎噪,','%u6293%u5730%u529B,','#SiteName',0)";
       
        /// <summary>
        /// 裂纹
        /// </summary>
        string s_lv = "insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'2,','#Title','#time',0,'裂纹,','%u88C2%u7EB9,','#SiteName',0)";
      
          /// <summary>
        /// 鼓包
        /// </summary>
          string s_gb= "insert into dbo.sitedata(SrcUrl,projectId,analysis,destroy,assort,Title,CreateDate,SiteId,sort,descriptions,SiteName,hot) values('#Url',70,#Analysis,3,'1,','#Title','#time',0,'鼓包,','%u9F13%u5305%20,','#SiteName',0)";
      
       


    }
}

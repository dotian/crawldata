using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConGetData.Model;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using ConGetData.DAL;
using LogNet;
using ConGetData.BLL;
using System.Data;

namespace ConGetData.UnitTest
{  
     //单元测试类
    class UnitTest2
    {
        //用于测试 还有多少站点是 可以打的开的

        string sql1 = "select distinct Tid from SiteList where  tid not in (select T_IdStr from T_unitTB)  order by tid asc ";

        string sql2 = "select top(1) SiteUrl from SiteList where tid = ";

        public void test()
        {
            HelperSQL.connectionString = "server=127.0.0.1;database=DataMiningDB;uid=sa;pwd=sa;";
            DataTable dt = HelperSQL.SelectData(sql1, null, System.Data.CommandType.Text);


            List<string> tidList = new List<string>();
            
           foreach (DataRow row in dt.Rows)
	        {
                tidList.Add(row["Tid"].ToString());

              
               // 
	        }
            HttpHelper _hh = new HttpHelper();
           foreach (string tid in tidList)
           {
                 string url = getUrl(tid);
                 try
                 {
                     string html = _hh.Open(url,"gb2312");

                     if (html.Length>20000)
                     {
                         //大于2万, 说明可以打开
                         string content = tid+"\t还有用\n";
                         File.AppendAllText("log.txt", content);

                     }
                     else
                     {
                         string content = tid + "\t已经失效\n";
                         File.AppendAllText("log.txt", content);
                     }
                 }
                 catch
                 {
                     string content = tid + "\t已经失效\n";
                     File.AppendAllText("log.txt", content);
                 }
                

           }
         


            Console.WriteLine("finished!");
        }

        public void Test2()
        {
            string path = @"H:\2014Year\ConGetData\ConGetData\bin\Debug\log.txt";
            string[] aar = File.ReadAllLines(path);
            for (int i = 0; i < aar.Length; i++)
            {
                if (aar[i].Contains("还有用"))
                {
                    Console.WriteLine(aar[i]);
                }
            }
            Console.WriteLine("finished!");
        }

        private string getUrl(string tid)
        {
            string sql = "select top(1) SiteUrl from SiteList where tid =" + tid;
            HelperSQL.connectionString = "server=127.0.0.1;database=DataMiningDB;uid=sa;pwd=sa;";
            object obj = HelperSQL.ExecuteScalar(sql,null, CommandType.Text);

            return obj.ToString();
        }

        public void test4()
        {
            string url = "http://www.qihoo.com/wenda.php?r=search/index&kw=%BA%AB%CC%A9%C2%D6%CC%A5&do=search&area=2&src=wenda_tab&sort=pdate&page={p}&time=month&type=bbs";
            Console.WriteLine(url.Replace("&","&amp;"));
          
        }

        public void test5()
        {
            string url = "http://www.sogou.com/web?query=%E9%9F%A9%E6%B3%B0%E8%BD%AE%E8%83%8E&amp;tsn=3&amp;page={p}&amp;p=40040100&amp;dp=1";
            Console.WriteLine(url.Replace("&amp;", "&"));
        }


    }
}

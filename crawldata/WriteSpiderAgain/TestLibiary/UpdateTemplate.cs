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
    public class UpdateTemplate
    {

        CommonHelper com = new CommonHelper();

        public void TestAnalysicTemplate()
        {
            Trace.WriteLine("--TestAnalysicTemplate开始--");
            string content = File.ReadAllText("../../TsetFile/原本blogs_88035.xml");
            TemplateModel template = new TemplateModel(content);

            Trace.WriteLine("输出template.Node:\t" + template.Node);
            Trace.WriteLine("输出template.TitleRegex:\t" + template.TitleRegex);
            Trace.WriteLine("输出template.ContentRegex:\t" + template.ContentRegex);
            Trace.WriteLine("输出template.CreateDateRegex:\t" + template.CreateDateRegex);
            Trace.WriteLine("输出template.SrcUrlRegex:\t" + template.SrcUrlRegex);

            Trace.WriteLine("--TestAnalysicTemplate结束--");
        }



        public void test()
        {
            string content = "<p class=\"content\">(.+?)<!----></p>";

            string html = System.IO.File.ReadAllText("../../TextFile1.txt");

            string red = com.NoHTML(html);
            com.GetMatchRegex(content, html);
            Trace.WriteLine("输出red:\t" + red);
            Trace.WriteLine("--完毕--");
        }


        public void TestGetNewsContent()
        {
            string messhtml = File.ReadAllText("../../TsetFile/newsContent.txt");
            string contentregex = "\"></a></div>(.+?)...?</span";
            Console.WriteLine(contentregex);
            string matchResult = com.GetMatchRegex(contentregex, messhtml);
            matchResult = com.NoHTML(matchResult);
            Trace.WriteLine("输出red:\t" + matchResult);
            Trace.WriteLine("--TestGetNewsContent完毕--");
        }

        public void TestGetContentDate()
        {
            //</span> <a href="http://weibo.com/1980182217/zCsqU7mwX" title="2013-06-21 15:56" date="1371801375000" class="date" node-type="feed_list_item_date"
            string messhtml = File.ReadAllText("../../TsetFile/TestWorkSina_4th_html.txt");
            string contentdateregex = "</span> <a href=.+?\" title=\"(.+?)\" date";
            string matchResult = com.GetMatchRegex(contentdateregex, messhtml);

            Trace.WriteLine("输出ContentDate:\t" + matchResult);
            DateTime dtime = Convert.ToDateTime(matchResult);
            Trace.WriteLine("输出dtime:\t" + dtime.ToString("yyyyMMdd hh:mm:ss"));
            Trace.WriteLine("--TestGetContentDate完毕--");
        }

        public void GetFormatXml()
        {
            Trace.WriteLine("--GetFormatXml开始--");
            string formatXml = File.ReadAllText("../../TsetFile/formatXml.txt");
            string result = formatXml.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&nbsp;"," ");
            Trace.WriteLine("输出formatXml:\t" + result);
            Trace.WriteLine("--GetFormatXml完毕--");
        }

        public void Test()
        {
            string time = "2013/7/16 11:07:20";
            DateTime dt = Convert.ToDateTime(time);
            Trace.WriteLine("输出:\t" + dt.ToString("yyyy-MM-dd HH:mm:ss"));
        }
      
    }
}

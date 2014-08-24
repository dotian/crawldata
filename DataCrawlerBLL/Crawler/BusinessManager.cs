using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;



using DataCrawler.DAL.Crawler;
using DataCrawler.Model.Crawler;
using System.Diagnostics;

namespace DataCrawler.BLL.Crawler
{
    public class BusinessManager
    {
        
        protected void Test()
        {
            strRegular = "A+B+C";
            strMatchContent = "这一段文字包括了A,包括了B,包括了C,也包含D和E ,结果是false";
            bool matchResult = MatchRegular(strRegular, strMatchContent);

            Trace.WriteLine("输出matchResult:\t" + matchResult);
            Trace.WriteLine("--完毕--");

        }
        protected bool MatchRegular(string regular, string matchContent)
        {
            strRegular = regular;
            strMatchContent = matchContent;

            strContainsArray = regular.Substring(0, regular.IndexOf('-')).Split('+');
            bool b = RegularMatchAction(0, true);

            strContainsArray = regular.Substring(regular.IndexOf('-') + 1).Split('-');
            bool c = RegularMatchAction(0, false);

            return (b && c);

        }
        protected string[] strContainsArray = new string[] { };

        protected string strRegular = "A+B+C";
            //"A+B+C-D-E";
        protected string strMatchContent = "这一段文字包括了A,包括了B,包括了C,也包含D和E ,结果是false";

        protected bool RegularMatchAction(int i, bool containsType)
        {
            if (i > strContainsArray.Length - 1)
            {
                return true;
            }
            bool contains = strMatchContent.Contains(strContainsArray[i]);

            if (!contains && containsType == true)
            {
                // 规则 包含,结果 不包含,返回 false
                return false;
            }
            else if (contains && containsType == false)
            {
                //规则 不包含,但是 包含了,返回false;
                return false;
            }
            else
            {
                //继续下一轮的判断
                return RegularMatchAction(++i, containsType);
            }
        }


        protected void TestSplitContains()
        {
           
            //得到包含的
            if (strRegular.Contains("-"))
            {
                strContainsArray = strRegular.Substring(0, strRegular.IndexOf('-')).Split('+');
            }
            else
            {
                strContainsArray = strRegular.Split('+');
            }
           
           
            for (int i = 0; i < strContainsArray.Length; i++)
            {
                Trace.WriteLine(strContainsArray[i]);
            }
        }

        protected void TestSplitNotContains()
        {
            //得到 不包含的
            strContainsArray = strRegular.Substring(strRegular.IndexOf('-') + 1).Split('-');
            for (int i = 0; i < strContainsArray.Length; i++)
            {
                Trace.WriteLine(strContainsArray[i]);
            }
        }

        /**/

        protected string AnalysicParms(string parmsData, string args)
        {
            int a = parmsData.IndexOf(args) + args.Length + 1;
            int b = parmsData.LastIndexOf(args);
            string res = "";
            if (b > a)
            {
                res = parmsData.Substring(a, b - a).Trim(); ;
            }
            return res;
        }


        public void Tes1t()
        {
            string app = "ABC";
            Console.WriteLine(app.CompareTo("ABABC"));

        }


        public void test_1()
        {
            string mess = "这一段文字包括了A,包括了B,包括了C,也包含D和E";
            string rule = "A+B-C"; // 包含A,或者包含 B,但是不包含C

            //A
            //A+B
            //A-B
            //A+B-C
            //(A+B)&(C+D)
            //(A-B)&(C-D)-E
            // (title like A and title not like B) and (title like C and title not like D) and title not like E

            //A+B+(C&D-E)
                 
           // title like A or title like B or (title like C and title like D and title not like E)


        }



    }
}

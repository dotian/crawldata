using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConSiteUesFul
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--开始所有任务--");
            try
            {
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main方法毁灭级别异常:" + ex.Message);
            }
           
            Console.WriteLine("--按回车键退出--");

            Console.ReadLine();

        }
    }
}

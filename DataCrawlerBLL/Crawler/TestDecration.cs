using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCrawler.BLL.Crawler
{
    public class TestDecration
    {
        public List<AccountInfo> GetList()
        {
            List<AccountInfo> list = new List<AccountInfo>();
            list.Add(new AccountInfo(1,(decimal)2.1501));
            list.Add(new AccountInfo(2, (decimal)1.0301));
            list.Add(new AccountInfo(3, (decimal)1.2001));
            list.Add(new AccountInfo(4, (decimal)1.5001));
            list.Add(new AccountInfo(5, (decimal)1.3001));
            list.Add(new AccountInfo(6, (decimal)1.0801));
            return list;
        }
        public void TestOrderBy()
        {
            List<AccountInfo> list = GetList();
            var quer = (from c in list where c.Id > 0 select c).ToList();

            List<AccountInfo> listAsc = quer.OrderByDescending(c => c.Balance).ToList();

            Console.WriteLine(listAsc.Count);
            foreach (AccountInfo item in listAsc)
            {
                Console.WriteLine("编号:" + item.Id + "  余额:" + item.Balance + "");
            }
            Console.WriteLine("完毕");
        }

        public void Test()
        {
            string mStr = "论坛Id:";
            int id = 1;
            int id2 = 2;

            string s1 = "|" + id + "|";
            mStr = mStr.Replace(s1, "");
            mStr += s1;
            string s2 = "|" + id2 + "|";

            mStr = mStr.Replace(s2, "");
            mStr += s2;

            Console.WriteLine(mStr);
            mStr = mStr.Replace(s1, "");
            Console.WriteLine(mStr);
        }
    }

    public class AccountInfo
    {
        public AccountInfo() { }
        public AccountInfo(int id,decimal balance) {
            this.Id = id;
            this.Balance = balance;
        }
        public int Id { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
    }

    
}

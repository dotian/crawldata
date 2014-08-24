using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace DataCrawler.BLL.Crawler
{
    public class Md5Helper
    {
        /// <summary>
        /// md5加密算法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            System.Array.Copy(res, temp, res.Length);
            return new string(temp);
        }

        /// <summary>
        /// 得到加密以后的字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5PwdEncrypt(string input, ref string key)
        {
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                int random = r.Next(1, 100);
                if (random>50)
                {
                    key += (char)r.Next(48, 57+1);
                }
                else
                {
                    key += (char)r.Next(97, 122 + 1);
                }
            }
            string mess = GetMD5Hash(input) + key;
            mess = GetMD5Hash(mess);
            //然后将字符集变为数字
            string pwdKey = "";
            for (int i = 0; i < mess.Length; i++)
            {
                pwdKey += (int)mess[i];
            }
            return pwdKey;
        }

         


        /// <summary>
        /// 密码比较
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="oriPwd"></param>
        /// <returns></returns>
        public static bool CompareMd5pwd(string input, string key, string oriPwd)
        {
            input = GetMD5Hash(input) + key;
            input = GetMD5Hash(input);
            string pwdKey = "";
            for (int i = 0; i < input.Length; i++)
            {
                pwdKey += (int)input[i];
            }
            return pwdKey == oriPwd;//值相等

        }

        /// <summary>
        /// 测试方法
        /// </summary>
        private void test()
        {
            string key="";
            string input = "apple";
            string pwd = GetMd5PwdEncrypt(input,ref key);
            Console.WriteLine(key);
            Console.WriteLine(pwd);
            bool b = CompareMd5pwd(input, key, pwd);
            Console.WriteLine(b);

        }
       
    }

}

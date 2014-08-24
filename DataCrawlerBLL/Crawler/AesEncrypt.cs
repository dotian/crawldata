using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DataCrawler.BLL.Crawler
{
    /// <summary>
    /// AES加密解密类
    /// </summary>
    public class AesEncrypt
    {
        public AesEncrypt() { }           
        private const string DefaultKey32 = "66576672667266686661672367236723";//256位(32)
        private const string DefaultKey24 = "6657667266726668.6661672";        //192位(24)
        private const string DefaultKey16 = "6657667266726668";                //128位(16)

        /// <summary>
        /// AES加密函数(使用默认长度为32的key)
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, DefaultKey32);
        }

        private void Test()
        {
            string pwd = "apple";
            string pwd_k = Encrypt(pwd);
            Console.WriteLine(pwd_k);
            string decode = Decrypt(pwd_k);
            Console.WriteLine(decode);
        }

        /// <summary>
        /// 数组AES加密函数
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="keys">长度必须是16,24,或32</param>
        /// <returns></returns>
        public static byte[] EncryptByte(byte[] toEncrypt, string keys)
        {
            if ((keys != null) && (Array.IndexOf(new Int32[] { 16, 24, 32 }, keys.Length) != -1))
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(keys);
                byte[] toEncryptArray = toEncrypt;
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return resultArray;
            }
            return new byte[0];
        }

        /// <summary>
        /// AES加密函数
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="keys">长度必须是16,24,或32</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string keys)
        {
            byte[] bb = EncryptByte(Encoding.UTF8.GetBytes(toEncrypt), keys);
            if (bb.Length == 0)
            {
                return string.Empty;
            }
            return Convert.ToBase64String(bb, 0, bb.Length);
        }

        /// <summary>
        /// AES解密函数(使用默认长度为32的key)
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            return Decrypt(toDecrypt, DefaultKey32);
        }

        /// <summary>
        /// AES解密函数
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="keys">长度必须是16,24,或32</param>
        /// <returns></returns>
        public static byte[] DecryptByte(byte[] toDecrypt, string keys)
        {
            if ((keys != null) && (Array.IndexOf(new Int32[] { 16, 24, 32 }, keys.Length) != -1))
            {
                //byte[] keyArray = UTF8Encoding.UTF8.GetBytes(keys);
                byte[] keyArray = Encoding.UTF8.GetBytes(keys);
                byte[] toEncryptArray = toDecrypt;
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return resultArray;
            }
            return new byte[0];
        }

        /// <summary>
        /// AES解密函数
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="keys">长度必须是16,24,或32</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string keys)
        {
            byte[] bb = DecryptByte(Convert.FromBase64String(toDecrypt), keys);
            if (bb.Length == 0)
            {
                return string.Empty;
            }
            return Encoding.UTF8.GetString(bb);
        }


        /// <summary>
        /// 使用登录密码 和 密钥 生成 最终 32位的密钥
        /// </summary>
        /// <param name="pwdStr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string SecretKey(string pwdStr,int k)
        {
          //  string pwdStr = "apple";
            while (pwdStr.Length < 8)
            {
                pwdStr += "#";
            }
            char[] pwdCharArr = pwdStr.ToCharArray();
            string laterStr = "";
            for (int i = 0; i < pwdCharArr.Length; i++)
            {
                int m = k ^ (int)pwdCharArr[i];
                laterStr += m;
            }
            return laterStr;
        }
    }
}

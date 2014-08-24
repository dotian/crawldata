using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WebMiningBLL
{
    public class TestAction
    {
        public void Test1()
        {
            string mess = Convert.ToString(223, 16);//d6
            Console.Write(mess);
            mess = Convert.ToString(232, 16);//d6
            Console.Write(mess);
            mess = Convert.ToString(246, 16);//d6
            Console.Write(mess);
        }

        public void T()
        {
            decimal t = 0.03555255245M;

            Console.WriteLine(t.ToString("##.##%"));

        }

     
        public void GetPwd()
        {
            string pwdStr = "admin001"; 
            Console.WriteLine("原始密码:"+pwdStr);
            while (pwdStr.Length<8)
	        {
                pwdStr+="#";
	        }
            Console.WriteLine("补足6位之后的密码:"+pwdStr);
            char[]pwdCharArr = pwdStr.ToCharArray();
          
            Random r = new Random();
            int k = 1873;
                //(int)(r.NextDouble() * 11111);
            Console.WriteLine("密钥:" + k);

            string laterStr = "";
            for (int i = 0; i < pwdCharArr.Length; i++)
			{
                int m = k ^ (int)pwdCharArr[i];
                laterStr += m;
			}
            Console.WriteLine("第1次加密之后的密钥:" + laterStr);
               //"TangZhongxin@163.com_23456789012"
            //9pvHK1Vzm5qrH7QL1P3skw==
            //Fzj2dlpz33wzCXjLjQuTPA==
            //HPQZBDJpzIM9F5mndCUD1g==_1873
            //HPQZBDJpzIM9F5mndCUD1g==_1873
            string result = AesEncrypt.Encrypt(pwdStr, laterStr)+ "_" + k;

            Console.WriteLine("第2次加密之后:" + result);
           
            //密钥,除去后面5位
            //取出 key
           // result = result.Substring(result.Length - 4, 4);

           // Console.WriteLine(result);

            //解密

            string mess1 = result.Substring(0, result.Length - 5);
            string pwd2 = AesEncrypt.Decrypt("HPQZBDJpzIM9F5mndCUD1g==", "18401825182518531844190619061906");
            Console.WriteLine(pwd2);
                //

        }

    }
    /// <summary>

    /// AES加密解密类

    /// </summary>

    public class AesEncrypt
    {
        public AesEncrypt() { }           //"66576672667266686661672367236723
        private const string DefaultKey32 = "TangZhongxin@163.com_23456789012";//256位(32)
        private const string DefaultKey24 = "TangZhongxin@163.com_234";        //192位(24)
        private const string DefaultKey16 = "TangZhongxin@163";                //128位(16)

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



                //return Encoding.UTF8.GetString(resultArray);

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
    }
 
}

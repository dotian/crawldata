using ConGetData.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ConGetData.ConMicroblog
{
    public static class TencentCookieHelper
    {
        public static CookieCollection GetTencentCookie()
        {
            string userAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.124 Safari/537.36";
            string verifyCode;
            string verifySession;
            string accountCode;
            string qqAccount = "451308127";
            string qqPassword = "zsgy8888";
            string loginSig = "YBWgVzgvSwM*353jswAlAjQG1GJcJ037Cd1JImhnAnFHwIUS93xb4Gpold0VD-J0";

            string urlForVerifyCode = "https://ssl.ptlogin2.qq.com/check?regmaster=&appid=46000101&js_ver=10099&js_type=1&u1=http%3A%2F%2Ft.qq.com&r=0.2472850245411683";
            urlForVerifyCode += "&uin=" + qqAccount;
            urlForVerifyCode += "&login_sig=" + loginSig;
            HttpWebRequest reqForVerifyCode = (HttpWebRequest)WebRequest.Create(urlForVerifyCode);
            reqForVerifyCode.UserAgent = userAgent;
            reqForVerifyCode.Headers.Add("Accept-Encoding", "gzip, deflate");
            reqForVerifyCode.Accept = "text/html, application/xhtml+xml, */*";

            HttpWebResponse resVerifyCode = (HttpWebResponse)reqForVerifyCode.GetResponse();
            var verifyCodeResStr = HttpHelper.GetResponseContent(resVerifyCode, Encoding.UTF32);
            var paras = ExtractResponseParas(verifyCodeResStr);
            if (paras[0] != "0")
            {
                throw new Exception("First param should be 0.");
            }
            verifyCode = paras[1];
            accountCode = paras[2];
            verifySession = paras[3];

            string sslLoginUrl = "https://ssl.ptlogin2.qq.com/login?pt_vcode_v1=0&pt_rsa=0&u1=http%3A%2F%2Ft.qq.com&ptredirect=1&h=1&t=1&g=1&from_ui=1&ptlang=2052&js_ver=10100&js_type=1&pt_uistyle=23&low_login_enable=1&low_login_hour=720&aid=46000101&daid=6&";
            sslLoginUrl += "&u=" + qqAccount;
            sslLoginUrl += "&verifycode=" + verifyCode;
            sslLoginUrl += "&pt_verifysession_v1=" + verifySession;
            sslLoginUrl += "&action=3-9-" + (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("0"); //1414828245108
            sslLoginUrl += "&p=" + EncodePassword(qqPassword, accountCode, verifyCode);
            sslLoginUrl += "&login_sig=" + loginSig;

            HttpWebRequest reqSSLLogin = (HttpWebRequest)WebRequest.Create(sslLoginUrl);
            reqSSLLogin.UserAgent = userAgent;
            reqSSLLogin.Headers.Add("Accept-Encoding", "gzip, deflate");
            reqSSLLogin.Accept = "*/*";
            reqSSLLogin.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            HttpWebResponse resSSLLogin = (HttpWebResponse)reqSSLLogin.GetResponse();
            var resLoginStr = HttpHelper.GetResponseContent(resSSLLogin, Encoding.UTF32);

            if (!resLoginStr.Contains("登录成功"))
            {
                throw new Exception("Login failed.");
            }

            var paraSSLLogin = ExtractResponseParas(resLoginStr);
            string loginUrl = paraSSLLogin[2];

            HttpWebRequest reqFinalLogin = (HttpWebRequest)WebRequest.Create(loginUrl);
            foreach (Cookie c in resSSLLogin.Cookies)
            {
                reqFinalLogin.CookieContainer.Add(c);
            }

            HttpWebResponse resFinalLogin = (HttpWebResponse)reqFinalLogin.GetResponse();
            var resFinalStr = HttpHelper.GetResponseContent(resFinalLogin, Encoding.UTF32);

            return resFinalLogin.Cookies;
        }

        private static string EncodePassword(string oriPsw, string accountCode, string verifyCode)
        {
            var str1 = GetMD5Hash(oriPsw);

            var str2 = TranslateToHex(GetMD5Hash(str1 + ParseHexToString(accountCode)));
            var str3 = TranslateToHex(GetMD5Hash(str2 + verifyCode));

            return str3;
        }

        private static string ParseHexToString(string hexInput)
        {
            var charArray = new char[hexInput.Length / 4];

            for (int i = 0; i < hexInput.Length / 4; i++)
            {
                string hex = hexInput.Substring(i * 4, 4).Substring(2);
                int temp = int.Parse(hex, NumberStyles.HexNumber);
                charArray[i] = (char)temp;
            }

            return new String(charArray);
        }

        private static string GetMD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] inputArray = new byte[input.Length];
            var inputCharArray = input.ToArray();

            for (int i = 0; i < inputCharArray.Length; i++)
            {
                inputArray[i] = (byte)inputCharArray[i];
            }

            byte[] res = md5.ComputeHash(inputArray, 0, input.Length);
            char[] temp = new char[res.Length];
            System.Array.Copy(res, temp, res.Length);
            return new string(temp);
        }

        private static string TranslateToHex(string str2)
        {
            var temp = str2.ToArray();
            string result = string.Empty;
            for (int i = 0; i < temp.Length; i++)
            {
                result += ((int)temp[i]).ToString("X2");
            }
            return result;
        }

        private static string[] ExtractResponseParas(string res)
        {
            int index1 = res.IndexOf("(");
            int index2 = res.LastIndexOf(")");
            return res.Substring(index1 + 1, index2 - index1 - 1).Split(',').Select(s => s.Trim('\'', ' ')).ToArray();
        }
    }
}

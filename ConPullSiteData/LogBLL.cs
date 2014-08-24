using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using log4net;
using log4net.Config;
namespace LogNet
{
    public class LogBLL
    {
        public static ILog log = LogManager.GetLogger("MyLogger");

        public static void InitLog()
        {
            XmlConfigurator.Configure();
        }
        public static void Debug(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
            log = null;
        }
        public static void Debug(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsDebugEnabled)
            {
                log.Debug(message, ex);
            }
            log = null;
        }

        public static void Error(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
            log = null;
        }
        public static void Error(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
            log = null;
        }

        public static void Fatal(string message)
        {

            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
            log = null;
        }
        public static void Fatal(string message, Exception ex)
        {

            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsFatalEnabled)
            {
                log.Fatal(message, ex);
            }
            log = null;
        }
        public static void Info(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
            log = null;
        }
        public static void Info(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
            log = null;
        }
        public static void Warn(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
            log = null;
        }
        public static void Warn(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
            if (log.IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
            log = null;
        }

        private void test()
        {
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("catename",1 ),
                     new SqlParameter("empname", 2),
                      new SqlParameter("classid",3 )
                                };
            foreach (object item in parms)
            {
                Console.WriteLine(item.ToString());
            }


        }

        public static void Log(string argStoredProcedure)
        {
            try
            {
                Info(string.Concat(new string[]
			    {
				    "存储过程：[",
				    argStoredProcedure,
				    "]"
			    }));
            }
            catch
            {

            }


        }
        public static void Log(string argStoredProcedure, SqlParameter[] argPara)
        {
            try
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < argPara.Length; i++)
                {
                    SqlParameter obj = argPara[i];
                    if (obj != null)
                    {
                        arrayList.Add(obj.SqlValue.ToString());
                    }
                }
                Info(string.Concat(new string[]
			    {
				    "存储过程：[",
				    argStoredProcedure,
				    "];参数：['",
				    string.Join("','", (string[])arrayList.ToArray(typeof(string))),
				    "']"
			    }));
            }
            catch
            {
            }
        }
    }
}

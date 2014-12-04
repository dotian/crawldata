using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace WriteSpiderAgain.ServiceDAL
{
    public class Field
    {
        // 屏蔽构造函数
        private Field() { }

        #region const define
        public const int EXP_RETURN = -1;
        private const string NULL_STRING = "";
        private const long NULL_INT64 = 0;
        private const int NULL_INT32 = 0;
        private const short NULL_INT16 = 0;
        private const float NULL_FLOAT = 0.00F;
        private const Decimal NULL_DECIMAL = 0.00M;
        private static readonly DateTime NULL_DATETIME = new DateTime(0);
        private const bool NULL_BOOL = false;
        public const string NULL_PRARAM_STR = "";
        public const int NULL_PARAM_INT = 0;
        #endregion

        #region 基于列索引
        static public string GetString(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return Field.NULL_STRING;
            return rec.GetString(fldnum);
        }

        static public decimal GetDecimal(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_DECIMAL;
            return rec.GetDecimal(fldnum);
        }

        static public int GetInt(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_INT32;
            return rec.GetInt32(fldnum);
        }

        static public bool GetBoolean(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_BOOL;
            return rec.GetBoolean(fldnum);
        }

        static public byte GetByte(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return 0;
            return rec.GetByte(fldnum);
        }

        static public DateTime GetDateTime(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_DATETIME;
            return rec.GetDateTime(fldnum);
        }

        static public double GetDouble(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return 0;
            return rec.GetDouble(fldnum);
        }

        static public float GetFloat(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_FLOAT;
            return rec.GetFloat(fldnum);
        }

        static public Guid GetGuid(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return Guid.Empty;
            return rec.GetGuid(fldnum);
        }

        static public int GetInt32(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_INT32;
            return rec.GetInt32(fldnum);
        }

        static public Int16 GetInt16(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_INT16;
            return rec.GetInt16(fldnum);
        }

        static public long GetInt64(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum)) return NULL_INT64;

            object o = rec.GetInt64(fldnum) as object;
            if (o is Int64)
                return rec.GetInt64(fldnum);
            else
            {
                System.Diagnostics.Debug.WriteLine("return value is not int64 " + o.GetType());
                return 0;
            }
        }

        #endregion

        #region 基于列名称
        static public string GetString(IDataRecord rec, string fldname)
        {
            return GetString(rec, rec.GetOrdinal(fldname));
        }

        static public decimal GetDecimal(IDataRecord rec, string fldname)
        {
            return GetDecimal(rec, rec.GetOrdinal(fldname));
        }

        static public int GetInt(IDataRecord rec, string fldname)
        {
            return GetInt(rec, rec.GetOrdinal(fldname));
        }

        static public bool GetBoolean(IDataRecord rec, string fldname)
        {
            return GetBoolean(rec, rec.GetOrdinal(fldname));
        }

        static public byte GetByte(IDataRecord rec, string fldname)
        {
            return GetByte(rec, rec.GetOrdinal(fldname));
        }

        static public DateTime GetDateTime(IDataRecord rec,
            string fldname)
        {
            return GetDateTime(rec, rec.GetOrdinal(fldname));
        }

        static public double GetDouble(IDataRecord rec, string fldname)
        {
            return GetDouble(rec, rec.GetOrdinal(fldname));
        }

        static public float GetFloat(IDataRecord rec, string fldname)
        {
            return GetFloat(rec, rec.GetOrdinal(fldname));
        }

        static public Guid GetGuid(IDataRecord rec, string fldname)
        {
            return GetGuid(rec, rec.GetOrdinal(fldname));
        }

        static public Int32 GetInt32(IDataRecord rec, string fldname)
        {
            return GetInt32(rec, rec.GetOrdinal(fldname));
        }

        static public Int16 GetInt16(IDataRecord rec, string fldname)
        {
            return GetInt16(rec, rec.GetOrdinal(fldname));
        }

        static public Int64 GetInt64(IDataRecord rec, string fldname)
        {
            return GetInt64(rec, rec.GetOrdinal(fldname));
        }
        #endregion

        /// <summary>
        /// 得到默认的输出参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetOutPutParam(IDataParameter param, string defaultValue)
        {
            if (param.Value is System.DBNull || param.Value == null)
                return defaultValue;
            else
                return param.Value.ToString();
        }

        public static int GetOutPutParam(IDataParameter param, int defaultValue)
        {
            if (param.Value is System.DBNull || param.Value == null || param.Value == DBNull.Value)
                return defaultValue;
            else
                return (int)param.Value;
        }
        public static double GetOutPutParam(IDataParameter param, double defaultValue)
        {
            if (param.Value is System.DBNull || param.Value == null || param.Value == DBNull.Value)
                return defaultValue;
            else
                return (double)param.Value;
        }

        public static DateTime GetOutPutParam(IDataParameter param, string defaultValue, bool isDate)
        {
            if (param.Value is System.DBNull || param.Value == null)
                return DateTime.MinValue;
            else
                return DateTime.Parse(param.Value.ToString());
        }


        public static int GetReturnPram(IDataParameter param)
        {
            if (param.Value is System.DBNull || param.Value == null || param.Value == DBNull.Value)
                return Field.EXP_RETURN;
            else
                return (int)param.Value;
        }

    } 
}

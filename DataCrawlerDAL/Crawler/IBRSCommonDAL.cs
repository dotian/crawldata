using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace RionSoft.IBRS.Business.DAL
{
    public class IBRSCommonDAL:IDisposable
    {
        //写上数据访问层的 方法

        protected static readonly string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        protected static SqlConnection mDbConnection = null;
        protected static SqlDataAdapter mDbDataAdapter = null;
        protected static SqlCommand mDbCommand = null;

        public static string logPath = "";
        public void Dispose()
        {
            if (mDbConnection!=null)
            {
                try
                {
                    mDbConnection.Close();
                }
                catch
                {
                }
            }
        }

        public IBRSCommonDAL()
        {
            try
            {
                if (mDbConnection== null)
                {
                    mDbConnection = new SqlConnection(connStr);
                } 
                if (mDbConnection.State == ConnectionState.Broken)
                {
                    mDbConnection.Close();
                    mDbConnection.Open();
                }
                if (mDbConnection.State == ConnectionState.Closed)
                {
                     mDbConnection.Open();
                }

                mDbCommand = new SqlCommand();
                mDbCommand.CommandTimeout = 120;
                mDbDataAdapter = new SqlDataAdapter();
                mDbCommand.Connection = mDbConnection;
                mDbDataAdapter.SelectCommand = mDbCommand;
            }
            catch
            {
                
            }
        }

        public DataTable SelectData(string argSqlText)
        {
          
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandType = CommandType.Text;
            DataTable dataTable = new DataTable();
            mDbCommand.CommandText = argSqlText;
            mDbDataAdapter.SelectCommand = mDbCommand;
            mDbDataAdapter.Fill(dataTable);
            return dataTable;
        }
       

        /// <summary>
        /// 查询存储过程
        /// </summary>
        /// <param name="argStoredProcedure">存储过程名</param>
        /// <param name="argPara">参数列表</param>
        /// <returns></returns>
        public DataTable SelectData(string argStoredProcedure, SqlParameter[] argPara)
        {
            DataTable dataTable = new DataTable();
                mDbCommand.Parameters.Clear();
                mDbCommand.CommandTimeout = 6000;
                mDbCommand.CommandType = CommandType.StoredProcedure;
                if (argPara != null)
                {
                    mDbCommand.Parameters.AddRange(argPara);
                }
                mDbCommand.CommandText = argStoredProcedure;
                mDbDataAdapter.SelectCommand = mDbCommand;
                mDbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public int ExecuteNonQuery(string argSqlText)
        {
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandType = CommandType.Text;
            mDbCommand.CommandText = argSqlText;
            return mDbCommand.ExecuteNonQuery();
        }
        public int ExecuteNonQuery(string argStoredProcedure, SqlParameter[] argPara)
        {
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandText = argStoredProcedure;
            if (argPara!=null)
            {
                mDbCommand.Parameters.AddRange(argPara);
            }
            mDbCommand.CommandType = CommandType.StoredProcedure;
            return mDbCommand.ExecuteNonQuery();
        }
        public int ExecuteNonQuery(string argSqlText, SqlParameter[] argPara,CommandType cmdType)
        {
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandText = argSqlText;
            if (argPara != null)
            {
                mDbCommand.Parameters.AddRange(argPara);
            }
            mDbCommand.CommandType = cmdType;
            return mDbCommand.ExecuteNonQuery();
        }


        public object ExecuteScalar(string argSqlText)
        {
          
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandType = CommandType.Text;
            mDbCommand.CommandText = argSqlText;
            return mDbCommand.ExecuteScalar();
          
        }
        public object ExecuteScalar(string argSqlText, SqlParameter[] argPara,CommandType cmdType )
        {
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandText = argSqlText;
            if (argPara != null)
            {
                mDbCommand.Parameters.AddRange(argPara);
            }
            mDbCommand.CommandType = cmdType;
            return mDbCommand.ExecuteScalar();

        }
        public object ExecuteScalar(string argStoredProcedure, SqlParameter[] argPara)
        {
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandText = argStoredProcedure;
            if (argPara != null)
            {
                mDbCommand.Parameters.AddRange(argPara);
            }
            mDbCommand.CommandType = CommandType.StoredProcedure;
            return mDbCommand.ExecuteScalar();
        }

        public static object obj = new object();
       
    }
}

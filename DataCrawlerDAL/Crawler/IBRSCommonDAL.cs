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
using System.Threading;
namespace RionSoft.IBRS.Business.DAL
{
    public class IBRSCommonDAL : IDisposable
    {
        protected static readonly string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        protected static SqlConnection mDbConnection = null;
        protected SqlDataAdapter mDbDataAdapter = null;
        protected SqlCommand mDbCommand = null;

        private static bool IsFattyOnBridge;

        public static string logPath = "";
        public void Dispose()
        {
            if (mDbConnection != null)
            {
                try
                {
                    LogNet.LogBLL.Info("Close sql connection.");
                    mDbConnection.Close();
                }
                catch (Exception ex)
                {
                    LogNet.LogBLL.Error(ex.Message);
                }
            }
        }

        public IBRSCommonDAL()
        {
            try
            {
                if (mDbConnection == null)
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
            catch(Exception ex)
            {
                LogNet.LogBLL.Error(ex.Message);
            }
        }

        public DataTable SelectData(string argSqlText)
        {
            WaitForConnectionAvailable();
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandType = CommandType.Text;
            DataTable dataTable = new DataTable();
            mDbCommand.CommandText = argSqlText;
            mDbDataAdapter.SelectCommand = mDbCommand;

            WaitForConnectionAvailable();
            InternalFillForConcurrency(mDbDataAdapter, dataTable);

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

            WaitForConnectionAvailable();
            InternalFillForConcurrency(mDbDataAdapter, dataTable);

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
            if (argPara != null)
            {
                mDbCommand.Parameters.AddRange(argPara);
            }
            mDbCommand.CommandType = CommandType.StoredProcedure;
            return mDbCommand.ExecuteNonQuery();
        }
        public int ExecuteNonQuery(string argSqlText, SqlParameter[] argPara, CommandType cmdType)
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
        public object ExecuteScalar(string argSqlText, SqlParameter[] argPara, CommandType cmdType)
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

        private static void InternalFillForConcurrency(SqlDataAdapter adapter, DataTable dt)
        {
            adapter.Fill(dt);
            IsFattyOnBridge = false;
        }

        private static void WaitForConnectionAvailable(int timeout = 2000)
        {
            int time = 0;
            int interval = 100;

            while (time < timeout)
            {
                if (!IsFattyOnBridge)
                {
                    IsFattyOnBridge = true;
                    return;
                }

                time += interval;
                Thread.Sleep(interval);
            }

            throw new Exception(string.Format("Connection is not available after {0} milliseconds.", timeout));
        }
    }
}

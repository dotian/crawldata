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
    public class IBRSCommonDAL
    {
        //写上数据访问层的 方法

        protected static readonly string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        protected static SqlConnection mDbConnection = null;
        protected static SqlDataAdapter mDbDataAdapter = null;
        protected static SqlCommand mDbCommand = null;

        public static string logPath = "";
     
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
            catch (Exception ex)
            {
                LogNet.LogBLL.Error("IBRSCommonDAL 无参构造函数异常", ex);
            }
        }

        public DataTable SelectData(string argSqlText)
        {
            //Log(LogLevel.Debug, argSqlText);
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandType = CommandType.Text;
            DataTable dataTable = new DataTable();
            mDbCommand.CommandText = argSqlText;
            mDbDataAdapter.Fill(dataTable);
            return dataTable;
        }
     
        public DataTable SelectData(string argStoredProcedure, SqlParameter[] argPara)
        {


            mDbCommand.Parameters.Clear();
            DataTable dataTable = null;
            mDbCommand.CommandTimeout = 6000;
            mDbCommand.CommandType = CommandType.StoredProcedure;
            if (argPara != null)
            {
                mDbCommand.Parameters.AddRange(argPara);
            }
            mDbCommand.CommandText = argStoredProcedure;
            SqlDataReader oleDbDataReader = mDbCommand.ExecuteReader();
            int fieldCount = oleDbDataReader.FieldCount;
            if (dataTable == null)
            {
                dataTable = new DataTable();
                for (int k = 0; k < fieldCount; k++)
                {
                    dataTable.Columns.Add(oleDbDataReader.GetName(k), oleDbDataReader.GetFieldType(k));
                }
            }
            else
            {
                if (dataTable.Columns.Count != fieldCount)
                {
                    dataTable.Columns.Clear();
                    for (int l = 0; l < fieldCount; l++)
                    {
                        dataTable.Columns.Add(oleDbDataReader.GetName(l), oleDbDataReader.GetFieldType(l));
                    }
                }
                else
                {
                    for (int m = 0; m < fieldCount; m++)
                    {
                        if (!dataTable.Columns[m].DataType.Equals(oleDbDataReader.GetFieldType(m)))
                        {
                            dataTable.Columns[m].DataType = oleDbDataReader.GetFieldType(m);
                        }
                    }
                }
            }
            object[] array = new object[fieldCount];
            while (oleDbDataReader.Read())
            {
                oleDbDataReader.GetValues(array);
                dataTable.Rows.Add(array);
            }
            oleDbDataReader.Close();
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

        public object ExecuteScalar(string argSqlText)
        {
            mDbCommand.Parameters.Clear();
            mDbCommand.CommandType = CommandType.Text;
            mDbCommand.CommandText = argSqlText;
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

    }
}

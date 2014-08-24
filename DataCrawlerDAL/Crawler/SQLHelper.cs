using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Sql;
using System.Data.SqlClient;
namespace DataCrawler.DAL.Crawler
{
    public class SQLHelper
    {
        public virtual int ExecuteNonQuery(string connStr, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            IDbConnection conn = new SqlConnection(connStr);
            IDbCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.Connection = conn;
            cmd.CommandType = commandType;

            AttachParameters(cmd, commandParameters);
            conn.Open();
            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            return retval;
        }
        public virtual int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");


            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            return retval;
        }

        public virtual object ExecuteScalar(string connStr, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            IDbConnection conn = new SqlConnection(connStr);
            IDbCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.Connection = conn;
            cmd.CommandType = commandType;

            AttachParameters(cmd, commandParameters);
            conn.Open();
            object retval = cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            return retval;
        }
        public virtual object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");


            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            object retval = cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            return retval;
        }

        public virtual DataTable ExecuteDataTable(string connStr, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dt = new DataTable();

            IDbConnection conn = new SqlConnection(connStr);
            IDbCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.Connection = conn;
            cmd.CommandType = commandType;
            AttachParameters(cmd, commandParameters);
            conn.Open();
            SqlDataAdapter dap = new SqlDataAdapter((SqlCommand)cmd);
            dap.Fill(dt);
            cmd.Parameters.Clear();
            conn.Close();
            return dt;
        }
        public virtual DataTable ExecuteDataTable(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dt = new DataTable();
            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            SqlDataAdapter dap = new SqlDataAdapter((SqlCommand)cmd);
            dap.Fill(dt);

            cmd.Parameters.Clear();
            return dt;
        }




        protected virtual void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true; //内部打开的数据库连接，则在方法退出前必须关闭连接
                connection.Open();
            }
            else
            {
                mustCloseConnection = false; //如果是外部打开的对象，则数据库连接的关闭有外部处理
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // 如果提供了一个有效的Transaction对象,则把该Transaction对象付给Command对象
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        protected virtual void AttachParameters(IDbCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (IDataParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
    }
}

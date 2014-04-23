using System;
using System.Data;
using System.Text.RegularExpressions ;
using System.Xml;
using System.IO;
using System.Collections ;
using System.Data.Common;
using System.Data.SQLite;

namespace Action.Sqlite
{
    public class SQLiteHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;

        public SQLiteHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">要执行的sql语句</param>
        /// <returns>查询结果数据集</returns>
        public DataSet ExecuteDataSet( string commandText)
        {
            DataSet ds = new DataSet();
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                cn.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
            }
            return ds;
        }




        /// <summary>
        /// 得到一个SQLiteDataReader
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public SQLiteDataReader ExecuteReader(string commandText)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(commandText, con);

            cmd.CommandText = commandText;
            SQLiteDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }




        /// <summary>
        /// 执行sql语句（单条sql语句）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">sql命令</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText)
        {
            int result = 0;
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                cn.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            return result;
        }




        /// <summary>
        /// 使用事务执行sql命令（多条sql语句）
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandTextArray">sql命令数组</param>
        /// <returns>true:成功；false:失败</returns>
        public  bool ExecuteNonQueryWithTran(ArrayList commandTextArray)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteTransaction tran = con.BeginTransaction();
                SQLiteCommand comm = con.CreateCommand();
                comm.Transaction = tran;
                try
                {
                    foreach (string strSql in commandTextArray)
                    {
                        comm.CommandText = strSql;
                        comm.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    comm.Dispose();
                }
            }
        }



        /// <summary>
        /// 返回单个值
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public  object ExecuteScalar(string commandText)
        {
            object result = System.DBNull.Value;
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = commandText;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                result = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            return result;
        }
    }

}
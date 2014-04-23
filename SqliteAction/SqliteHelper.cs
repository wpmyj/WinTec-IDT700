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
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">Ҫִ�е�sql���</param>
        /// <returns>��ѯ������ݼ�</returns>
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
        /// �õ�һ��SQLiteDataReader
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">��ѯ���</param>
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
        /// ִ��sql��䣨����sql��䣩
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandText">sql����</param>
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
        /// ʹ������ִ��sql�������sql��䣩
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="commandTextArray">sql��������</param>
        /// <returns>true:�ɹ���false:ʧ��</returns>
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
        /// ���ص���ֵ
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
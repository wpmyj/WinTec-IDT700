using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace Action.SqlServer
{
    /// <summary>
    /// ���ݿ���ز���
    /// </summary>
    public class SqlHelper
    {
        private string conStr;
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        public string ConStr
        {
            get { return conStr; }
            set { conStr = value; }
        }




        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="serverAdd">���ݿ��ַ</param>
        /// <param name="dataBaseName">���ݿ���</param>
        /// <param name="uid">��½��</param>
        /// <param name="pwd">��½����</param>
        public SqlHelper(string connStr)
        {
            this.ConStr = connStr;
        }



        /// <summary>
        /// ������ݿ�����û�д򿪣��������
        /// </summary>
        /// <param name="conn">a SqlConnection object</param>
        public void ConnOpen(SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }




        /// <summary>
        /// ���dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSql"></param>
        /// <param name="strTableName"></param>
        public void Fill(DataSet ds, string strSql, string strTableName)
        {
            SqlDataAdapter sda = new SqlDataAdapter(strSql, ConStr);
            sda.Fill(ds, strTableName);
        }




        //��������������������������������������������������������������������������������������������������������������������������==  
        //�����������������������������������������������������ݿ�ײ����������������������������������������������������������������

        //��������������������������������������������������������������������������������������������������������������������������==  
        
        
        
        /// <summary>
        /// ִ��ExecuteNonQuery
        /// </summary>
        /// <param name="cmdType">Sql�������</param>
        /// <param name="cmdText">Sql���</param>
        /// <param name="cmdParms">Parm����</param>
        /// <returns>����Ӱ������</returns>
        public  int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            int val = 0;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 3600;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return val;
            }
        }

       
        
        
        /// <summary>
        /// ����һ��SqlParameterʵ��
        /// </summary>
        /// <param name="ParamName">�ֶ���</param>
        /// <param name="stype">�ֶ�����</param>
        /// <param name="size">��Χ</param>
        /// <param name="Value">��ֵ</param>
        /// <returns>����һ��SqlParameterʵ��</returns>
        public  SqlParameter MakeParam(string ParamName, System.Data.SqlDbType stype, int size, Object Value)
        {
            SqlParameter para = new SqlParameter(ParamName, Value);
            para.SqlDbType = stype;
            para.Size = size;
            return para;
        }
       
        
        
        /// <summary>
        /// ���SqlParameterʵ��
        /// </summary>
        /// <param name="ParamName">�ֶ���</param>
        /// <param name="Value">��ֵ</param>
        /// <returns>����һ��SqlParameterʵ��</returns>
        public  SqlParameter MakeParam(string ParamName, string Value)
        {
            return new SqlParameter(ParamName, Value);
        }
       
        
        
        /// <summary>
        /// ���DateSetʵ��(��õ�ҳ��¼)
        /// </summary>
        /// <param name="int_PageSize">һҳ��ʾ�ļ�¼��</param>
        /// <param name="int_CurrentPageIndex">��ǰҳ��</param>
        /// <param name="cmdType">Sql�������</param>
        /// <param name="cmdText">Sql���</param>
        /// <param name="cmdParms">Parm����</param>
        /// <returns></returns>
        public  DataSet ExecuteDataSet(int int_PageSize, int int_CurrentPageIndex, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
                da.SelectCommand.CommandType = cmdType;
                da.SelectCommand.CommandTimeout = 3600;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        da.SelectCommand.Parameters.Add(parm);
                }
                conn.Close();

                DataSet ds = new DataSet();
                if (int_PageSize == 0 && int_CurrentPageIndex == 0)
                {
                    da.Fill(ds, "12news1234567890");
                }
                else
                {
                    int int_Page = int_PageSize * (int_CurrentPageIndex - 1);
                    if (int_Page < 0)
                    {
                        int_Page = 0;
                    }
                    da.Fill(ds, int_Page, int_PageSize, "12news1234567890");
                }
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        
        
        
        /// <summary>
        /// ���DateSetʵ��(���ȫ����¼)
        /// </summary>
        /// <param name="cmdType">Sql�������</param>
        /// <param name="cmdText">Sql���</param>
        /// <param name="cmdParms">Parm����</param>
        /// <returns></returns>
        public  DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
                da.SelectCommand.CommandType = cmdType;
                da.SelectCommand.CommandTimeout = 3600;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                    {
                        da.SelectCommand.Parameters.Add(parm);
                    }
                }
                conn.Close();

                DataSet ds = new DataSet();
                da.Fill(ds, "12news1234567890");
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }



        /// <summary>
        /// ����һ��datatable
        /// </summary>
        /// <param name="cmdType">sql�������</param>
        /// <param name="cmdText">sql���</param>
        /// <param name="cmdParms">parm����</param>
        /// <returns></returns>
        public  DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
                da.SelectCommand.CommandType = cmdType;
                da.SelectCommand.CommandTimeout = 3600;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        da.SelectCommand.Parameters.Add(parm);
                }
                conn.Close();

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }



        /// <summary>
        /// ִ��ExecuteScalar
        /// </summary>
        /// <param name="cmdType">Sql�������</param>
        /// <param name="cmdText">Sql���</param>
        /// <param name="cmdParms">Parm����</param>
        /// <returns>���ص�һ�е�һ�м�¼ֵ</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 3600;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
                if (val != null)
                    return val;
                else
                    return null;
            }
        }






        /// <summary>
        /// ִ��Transaction
        /// </summary>
        /// <param name="cmdType">Sql�������</param>
        /// <param name="cmdText">Sql�������</param>
        /// <returns></returns>
        public  bool ExecuteTransaction(CommandType cmdType, string[] cmdText)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.CommandTimeout = 3600;
                cmd.Transaction = transaction;

                try
                {
                    for (int i = 0; i < cmdText.Length; i++)
                    {
                        cmd.CommandText = cmdText[i];
                        cmd.ExecuteNonQuery();
                    }
                    
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    try
                    {
                        transaction.Rollback();
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        #region �������������ɵ�ģ����������ݷ���


        #region  ִ�м�SQL���

        /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public  int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }



        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">����SQL���</param>		
        public  bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }



        /// <summary>
        /// ִ�д�һ���洢���̲����ĵ�SQL��䡣
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <param name="content">��������,����һ���ֶ��Ǹ�ʽ���ӵ����£���������ţ�����ͨ�������ʽ���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public  int ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                SqlParameter myParameter = new SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }



        /// <summary>
        /// �����ݿ������ͼ���ʽ���ֶ�(������������Ƶ���һ��ʵ��)
        /// </summary>
        /// <param name="strSQL">SQL���</param>
        /// <param name="fs">ͼ���ֽ�,���ݿ���ֶ�����Ϊimage�����</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public  int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }




        /// <summary>
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
        public  object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }



        /// <summary>
        /// ִ�в�ѯ��䣬����SqlDataReader
        /// </summary>
        /// <param name="strSQL">��ѯ���</param>
        /// <returns>SqlDataReader</returns>
        public  SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }

        }



        /// <summary>
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        #endregion

        #region ִ�д�������SQL���

        /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public  int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����SqlParameter[]��</param>
        public  void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //ѭ��
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }





        /// <summary>
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
        public  object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        



        /// <summary>
        /// ִ�в�ѯ��䣬����SqlDataReader
        /// </summary>
        /// <param name="strSQL">��ѯ���</param>
        /// <returns>SqlDataReader</returns>
        public  SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }

        }



        /// <summary>
        /// ִ�в�ѯ��䣬����SqlDataReader
        /// </summary>
        /// <param name="cmdType">sql�������</param>
        /// <param name="SQLString">sql���</param>
        /// <param name="cmdParms">Parm����</param>
        /// <returns>SqlDataReader</returns>
        public  SqlDataReader ExecuteReader(CommandType cmdType,string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(connection, cmd, null, cmdType, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }

        }
        
        
        
        
        /// <summary>
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }




        private  void PrepareCommand(SqlConnection sqlconn, SqlCommand sqlcmd, SqlTransaction sqltran, CommandType cmdtype, string cmdtext, params SqlParameter[] parameters)
        {
            try
            {
                if (sqlconn.State == ConnectionState.Closed)

                    sqlconn.Open();

                sqlcmd.Connection = sqlconn;
                sqlcmd.CommandText = cmdtext;
                sqlcmd.CommandType = cmdtype;

                if (sqltran != null)
                    sqlcmd.Transaction = sqltran;


                if (parameters != null)
                    foreach (SqlParameter parameter in parameters)
                    {
                        sqlcmd.Parameters.Add(parameter);
                    }
            }
            catch (Exception ex)
            {
                sqlconn.Close();
                //throw;
            }
        }



        private  void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region �洢���̲���

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlDataReader</returns>
        public  SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="tableName">DataSet����еı���</param>
        /// <returns>DataSet</returns>
        public  DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                DataSet dataSet = new DataSet();
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(conn, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                conn.Close();
                return dataSet;
            }
        }




        /// <summary>
        /// ���� SqlCommand ����(��������һ���������������һ������ֵ)
        /// </summary>
        /// <param name="connection">���ݿ�����</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlCommand</returns>
        private  SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }





        /// <summary>
        /// ִ�д洢���̣�����Ӱ�������		
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <param name="rowsAffected">Ӱ�������</param>
        /// <returns></returns>
        public  int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }





        /// <summary>
        /// ���� SqlCommand ����ʵ��(��������һ������ֵ)	
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlCommand ����ʵ��</returns>
        private  SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion
        #endregion

    }
}

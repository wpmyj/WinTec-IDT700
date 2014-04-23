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
    /// 数据库相关操作
    /// </summary>
    public class SqlHelper
    {
        private string conStr;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConStr
        {
            get { return conStr; }
            set { conStr = value; }
        }




        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverAdd">数据库地址</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="uid">登陆名</param>
        /// <param name="pwd">登陆密码</param>
        public SqlHelper(string connStr)
        {
            this.ConStr = connStr;
        }



        /// <summary>
        /// 如果数据库连接没有打开，则打开连接
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
        /// 填充dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strSql"></param>
        /// <param name="strTableName"></param>
        public void Fill(DataSet ds, string strSql, string strTableName)
        {
            SqlDataAdapter sda = new SqlDataAdapter(strSql, ConStr);
            sda.Fill(ds, strTableName);
        }




        //　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝==  
        //　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝数据库底层操作＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        //　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝==  
        
        
        
        /// <summary>
        /// 执行ExecuteNonQuery
        /// </summary>
        /// <param name="cmdType">Sql语句类型</param>
        /// <param name="cmdText">Sql语句</param>
        /// <param name="cmdParms">Parm数组</param>
        /// <returns>返回影响行数</returns>
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
        /// 返回一个SqlParameter实例
        /// </summary>
        /// <param name="ParamName">字段名</param>
        /// <param name="stype">字段类型</param>
        /// <param name="size">范围</param>
        /// <param name="Value">赋值</param>
        /// <returns>返回一个SqlParameter实例</returns>
        public  SqlParameter MakeParam(string ParamName, System.Data.SqlDbType stype, int size, Object Value)
        {
            SqlParameter para = new SqlParameter(ParamName, Value);
            para.SqlDbType = stype;
            para.Size = size;
            return para;
        }
       
        
        
        /// <summary>
        /// 获得SqlParameter实例
        /// </summary>
        /// <param name="ParamName">字段名</param>
        /// <param name="Value">赋值</param>
        /// <returns>返回一个SqlParameter实例</returns>
        public  SqlParameter MakeParam(string ParamName, string Value)
        {
            return new SqlParameter(ParamName, Value);
        }
       
        
        
        /// <summary>
        /// 获得DateSet实例(获得单页记录)
        /// </summary>
        /// <param name="int_PageSize">一页显示的记录数</param>
        /// <param name="int_CurrentPageIndex">当前页码</param>
        /// <param name="cmdType">Sql语句类型</param>
        /// <param name="cmdText">Sql语句</param>
        /// <param name="cmdParms">Parm数组</param>
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
        /// 获得DateSet实例(获得全部记录)
        /// </summary>
        /// <param name="cmdType">Sql语句类型</param>
        /// <param name="cmdText">Sql语句</param>
        /// <param name="cmdParms">Parm数组</param>
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
        /// 返回一个datatable
        /// </summary>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">parm数组</param>
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
        /// 执行ExecuteScalar
        /// </summary>
        /// <param name="cmdType">Sql语句类型</param>
        /// <param name="cmdText">Sql语句</param>
        /// <param name="cmdParms">Parm数组</param>
        /// <returns>返回第一行第一列记录值</returns>
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
        /// 执行Transaction
        /// </summary>
        /// <param name="cmdType">Sql语句类型</param>
        /// <param name="cmdText">Sql语句数组</param>
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


        #region 代码生成器生成的模块所需的数据访问


        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
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
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
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
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
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
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
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
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
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
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
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
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
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

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
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
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
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
                        //循环
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
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
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
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
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
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="cmdType">sql语句类型</param>
        /// <param name="SQLString">sql语句</param>
        /// <param name="cmdParms">Parm数组</param>
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
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
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

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
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
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
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
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
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
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
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
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
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

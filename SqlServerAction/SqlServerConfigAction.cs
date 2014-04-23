using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Action.SqlServer
{
    /// <summary>
    /// Config 相关数据库操作
    /// </summary>
    public class SqlServerConfigAction:SqlServerBaseAction
    {
        public SqlServerConfigAction(string connStr)
            : base(connStr)
        {
        }

        public bool ChkRight(string posNo, string sNo)
        {
            bool ok = false;
            #region 组装sql
            string sqlStr = "select PosNo from tCzkPos where posno='"+posNo+"' and posid='"+sNo+"'";
            #endregion
            #region 执行sql
            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                if (reader.Read())
                {
                    ok = true;
                }
                reader.Close();
            }
            catch
            {
                ok = false;
                throw;
            }
            #endregion

            return ok;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace Action.SqlServer
{
    public class SqlServerDept : SqlServerBaseAction
    {
        public SqlServerDept(string constr):base(constr)
        {

        }
        /// <summary>
        /// 获取部门编号
        /// </summary>
        /// <param name="posNo">收款机编号</param>
        /// <returns></returns>
        public string getDept(string posNo)
        {
            string sqlStr = "select grpno from pos where posno='" + posNo + "'";
            
            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                if (reader.Read())
                {
                    return reader["grpno"].ToString();
                }
                else
                {
                    return null;
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}

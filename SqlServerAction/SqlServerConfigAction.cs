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

        /// <summary>
        /// 校验站点有效性
        /// </summary>
        /// <param name="posNo"></param>
        /// <param name="sNo"></param>
        /// <returns></returns>
        public bool ChkRight(string posNo, string sNo)
        {
            bool ok = false;
            #region 组装sql
            string sqlStr = "select top 1 * from Pos where PosNo='"+posNo+"' and PosID='"+sNo+"'";
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

        /// <summary>
        /// 查询站点参数
        /// </summary>
        /// <param name="posNo"></param>
        /// <returns></returns>
        public Model.Config syncCfg(string posNo)
        {
            Model.Config cfg = new Model.Config();



            try
            {
                SqlDataReader reader ;
                string sqlstr = "select PosNo,grpno,CsValue,DeptName from XtDept a,XtSysCfg b,POS c where a.code=c.grpno and c.PosNo='"+posNo+"' and b.CsName='dwmc'";
                reader= SqlEngine.ExecuteReader(sqlstr);
                if (reader.Read())
                {
                    cfg.Grpno = reader["grpno"].ToString();
                    cfg.PosNo = reader["PosNo"].ToString();
                    cfg.CompanyName = reader["CsValue"].ToString();
                    cfg.DeptName = reader["DeptName"].ToString();
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            return cfg;

        }
    }
}

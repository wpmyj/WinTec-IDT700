using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Action.SqlServer
{
    public class SqlServerUserAction:SqlServerBaseAction
    {
        public SqlServerUserAction(string connStr)
            : base(connStr)
        {

        }


        /// <summary>
        /// 获取后台用户
        /// </summary>
        /// <returns></returns>
        public List<Model.User> GetUserList()
        {
            List<Model.User> list = new List<Model.User>();

            string sqlStr =
                "select UserCode,UserName,mpassword,mclass,rightCode from XtMuser";
            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    Model.User user = new Model.User();
                    user.UserCode = reader["UserCode"].ToString();
                    user.UserName = reader["UserName"].ToString();
                    user.Mpassword = reader["mpassword"].ToString();
                    user.Mclass = reader["mclass"].ToString();
                    //user.Mdept = reader["mdept"].ToString();
                    user.RightCode = reader["RightCode"].ToString();
                    list.Add(user);
                }
                reader.Close();
            }
            catch
            {
                list.Clear();
                throw;
            }
            return list;
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public Model.User UserLogon(string userCode, string pwd,string posno)
        {
            Model.User user= new Model.User();

            pwd = new Des().EncryStrHex(pwd, "0125" + userCode);

            string sqlStr =
                "select a.UserCode,a.UserName,mpassword,mclass,rightCode,mdept from tCzkUser a ,tCzkPos b"
                +" where a.mdept=b.fdCode "
                +" and a.UserCode='"+userCode+"'"
                +" and mpassword='"+pwd+"' "
                +" and posno='"+posno+"' "
               ;
            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    user.UserCode = reader["UserCode"].ToString();
                    user.UserName = reader["UserName"].ToString();
                    user.Mpassword = reader["mpassword"].ToString();
                    user.Mclass = reader["mclass"].ToString();
                    user.Mdept = reader["mdept"].ToString();
                    user.RightCode = reader["RightCode"].ToString();
                }
                reader.Close();
                if (user.UserCode != "" || user.UserCode != null)
                {
                    #region 更新pos机使用状态
                    sqlStr = "update tczkPos set onuse='1' ,UserCode='" + user.UserCode + "',UserName='" + user.UserName + "' "
                        + " where posno='" + posno + "'";
                    #endregion

                    SqlEngine.ExecuteSql(sqlStr);
                }
            }
            catch
            {
                throw;
            }
            return user;
        }


        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="posNo"></param>
        public bool UserLogoff( string posNo)
        {
            bool ok = false;
            string sqlstr = "update tCzkPos set onuse='0' ,UserCode='',UserName='' "
                +" where posno='"+posNo+"'";
            try
            {
                int i = SqlEngine.ExecuteSql(sqlstr);
                if (i == 1)
                {
                    ok = true;
                }
            }
            catch
            {
                throw;
            }
            return ok;
        }
    }
}

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
        public Model.User UserLogon(string userCode, string pwd)
        {
            Model.User user= new Model.User();
            
            string sqlStr =
                "select UserCode,UserName,mpassword,mclass,rightCode from XtMuser "
                +"where UserCode='"+userCode+"'"
                +" and mpassword='"+pwd+"'";
            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    user.UserCode = reader["UserCode"].ToString();
                    user.UserName = reader["UserName"].ToString();
                    user.Mpassword = reader["mpassword"].ToString();
                    user.Mclass = reader["mclass"].ToString();
                    //user.Mdept = reader["mdept"].ToString();
                    user.RightCode = reader["RightCode"].ToString();
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            return user;
        }
    }
}

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
                "select UserCode,UserName,mpassword,mclass,mdept,rightCode from XtMuser where mclass in('"
                + ((int)Model.UserMClass.管理员).ToString() + "','"
                + ((int)Model.UserMClass.收款员).ToString() + "')";
               // +Model.UserMClass.售货员+"')";

            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    Model.User user = new Model.User();
                    user.UserCode = reader["UserCode"].ToString();
                    user.UserName = reader["UserName"].ToString();
                    user.Mpassword = reader["mpassword"].ToString();
                    user.Mclass = (Model.UserMClass)int.Parse(reader["mclass"].ToString());
                    user.Mdept = reader["mdept"].ToString();
                    user.RightCode = reader["RightCode"].ToString();
                    list.Add(user);
                }
            }
            catch(Exception ex) 
            {
                list.Clear();
                throw;
            }
            return list;
        }
    }
}

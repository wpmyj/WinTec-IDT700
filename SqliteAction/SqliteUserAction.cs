using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Collections;
namespace Action.Sqlite
{
    /// <summary>
    /// 用户相关操作类
    /// </summary>
    public class SqliteUserAction:SqliteBaseAction
    {
        public SqliteUserAction(string connStr)
            : base(connStr)
        {
        }



        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public Model.User userLogon(Model.User user)
        {
            #region 组装sql语句
            string sqlStr = "select * from xtmuser where userCode='" + user.UserCode + "' and mpassword='" + user.Mpassword + "'";
            #endregion
            try
            {
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);

                #region 组装usermodel
                if (reader.Read())
                {
                    user.UserCode = reader["userCode"].ToString();
                    user.UserName = reader["userName"].ToString();
                    user.Mpassword = reader["mpassword"].ToString();
                    //user.Mclass = (Model.UserMClass)int.Parse(reader["mclass"].ToString());
                    user.Mdept = reader["mdept"].ToString();
                    user.RightCode = reader["rightCode"].ToString();
                }
                else
                {
                    user = new Model.User();
                }
                #endregion
            }
            catch
            {
                throw;
            }
            return user;
        }


        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userList">用户列表</param>
        /// <returns></returns>
        public bool saveUser(List<Model.User> userList)
        {
            bool ok = false;
            ArrayList sqlStrArrayList=new ArrayList();

            #region 组装sql语句数组

            #region 清空用户列表   
            sqlStrArrayList.Add("delete from xtmuser");
            #endregion

            #region 保存用户
            foreach (Model.User user in userList)
            {
                string sqlStr = "insert into xtmuser (userCode,userName,mpassword,mclass,mdept,rightCode) "+
                    "values('" + user.UserCode + "','" + user.UserName + "','" + user.Mpassword + "','" + ((int)user.Mclass).ToString() + "','" + user.Mdept + "','" + user.RightCode + "')";
                sqlStrArrayList.Add(sqlStr);
                //SqliteEngine.ExecuteNonQuery(sqlStr);
            }
            #endregion

            #endregion
            try
            {            
                #region 执行语句
                ok = SqliteEngine.ExecuteNonQueryWithTran(sqlStrArrayList);
                #endregion
                          
                ok = true;
            }
            catch
            {
                ok = false;
                throw;
            }
            return ok;
        }
    }
}

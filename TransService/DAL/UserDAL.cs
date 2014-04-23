using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransService.Model;
using System.Data.SqlClient;
namespace TransService.DAL
{
    public class UserDAL : SQLBaseDAL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param Name="userCode"></param>
        /// <param Name="password"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        public static bool GetUser(string userCode, out MUser user, out string msg)
        {
            SqlDataReader rd=null;
            try
            {
                if (!DBTool.Select("UserCode=@UserCode", new MUser() { UserCode=userCode}, string.Empty
                    , out rd, out msg))
                {
                    user = null;
                    return false;
                }
                else
                {
                    ICollection<MUser> list = ObjTool.BuildObject<MUser>(rd);
                    rd.Close();
                    if (list.Count == 0)
                    {
                        msg = "登陆失败";
                        user = null;
                        return false;
                    }
                    else
                    {
                        user = list.First();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                user = null;
                return false;
            }
        }

        /// <summary>
        /// 检测用户权限
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="Password"></param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Check(string userCode, string password, out string msg)
        {
            SqlDataReader rd=null;
            try
            {
                if (!DBTool.Select("UserCode=@UserCode And mPassword=@mpassword And ValidFlag='Y'", new MUser() { UserCode=userCode,Password=password}, string.Empty
                    , out rd, out msg))
                {
                    return false;
                }
                else
                {
                    ICollection<MUser> list = ObjTool.BuildObject<MUser>(rd);
                    rd.Close();
                    if (list.Count == 0)
                    {
                        msg = "用户验证失败";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                return false;
            }
        }
    }
}

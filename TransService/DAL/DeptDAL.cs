using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransService.Model;
using System.Data.SqlClient;

namespace TransService.DAL
{
    public class DeptDAL:SQLBaseDAL
    {
        /// <summary>
        /// 读取部门信息
        /// </summary>
        /// <param Name="dept"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        public static bool GetDept(string DeptCode, out MDept dept, out string msg)
        {
            SqlDataReader rd=null;
            try
            {
                if (!DBTool.Select("Code=@Code", new MDept() { DeptCode = DeptCode }, string.Empty, out rd, out msg))
                {
                    dept = null;
                    return false;
                }
                else
                {
                    ICollection<MDept> list = ObjTool.BuildObject<MDept>(rd);
                    rd.Close();
                    if (list.Count == 0)
                    {
                        msg = "未找到该部门";
                        dept = null;
                        return false;
                    }
                    else
                    {
                        dept = list.First();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                dept = null;
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                return false;
            }
        }
    }
}

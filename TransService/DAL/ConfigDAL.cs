using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransService.Model;
using System.Data.SqlClient;
using System.Configuration;

namespace TransService.DAL
{
    public class ConfigDAL : SQLBaseDAL
    {
        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param Name="PosNo"></param>
        /// <param Name="config"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        public static bool GetConfig(string posNo, out ICollection<MConfig> config, out string msg)
        {
            SqlDataReader rd=null;
            try
            {
                if (!DBTool.Select("", new MConfig() { PosNo = posNo }, string.Empty, out rd, out msg))
                {
                    config = null;
                    return false;
                }
                else
                {
                    config= ObjTool.BuildObject<MConfig>(rd);
                    rd.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                config = null;
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                return false;
            }
        }
    }
}

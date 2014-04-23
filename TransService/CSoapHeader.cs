using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransService.Model;
using TransService.DAL;
using System.Data;
using TransService.COMM;

namespace TransService
{
    public class CSoapHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string UserCode;

        public string Password;

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptNO;

        /// <summary>
        /// 收款机号
        /// </summary>
        public string PosNO;

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <returns></returns>
        public bool IsValid(out string msg)
        {
            Password = Des.DecryStrHex(Password, "0125" + UserCode);//解密
            if (SQLBaseDAL.State == ConnectionState.Broken || SQLBaseDAL.State == ConnectionState.Closed)
            {
                //连接Broken、Closed
                if (!SQLBaseDAL.Open(out msg))//开启连接
                {
                    return false;
                }
            }
            return UserDAL.Check(UserCode, Password, out msg);
        }
    }
}

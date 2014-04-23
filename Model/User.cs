using System;

using System.Collections.Generic;
using System.Text;
using ComClass;
namespace Model
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class User
    {
        private string userCode;
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }



        private string userName;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }



        private string mpassword;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Mpassword
        {
            //set { mpassword = new Des().EncryStrHex(value, "0125" + userCode); }
            get
            {
                return mpassword;
            }
            set
            {
                mpassword = value;
            }
        }

        /// <summary>
        /// 输入明文密码
        /// </summary>
        public string EncryPassword
        {
            set
            {
                mpassword = Des.EncryStrHex(value, "0125" + userCode);
            }
        }

        public  string mclass;
        /// <summary>
        /// 用户类型
        /// 1-管理员 2-收款员 3-售货员 0-超级用户
        /// </summary>
        public UserMClass Mclass
        {
            get
            {
                return (UserMClass)int.Parse(mclass);
            }
            set
            {
                mclass = ((int)value).ToString();
            }
        }


        private string mdept;
        /// <summary>
        /// 部门
        /// </summary>
        public string Mdept
        {
            get { return mdept; }
            set { mdept = value; }
        }



        private string rightCode;
        /// <summary>
        /// 权限
        /// </summary>
        public string RightCode
        {
            get { return rightCode; }
            set { rightCode = value; }
        }
    }
}

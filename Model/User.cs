using System;

using System.Collections.Generic;
using System.Text;

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
            get { return mpassword; }
            set { mpassword = value; }
        }



        private string mclass;
        /// <summary>
        /// 用户类型
        /// 1-管理员  2-售货员 3-收款员
        /// </summary>
        public string Mclass
        {
            get { return mclass; }
            set { mclass = value; }
        }


        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool isGLY
        {
            get
            {
                if (mclass.Substring(0, 1) == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        /// <summary>
        /// 是否为营业员
        /// </summary>
        public bool isYYY
        {
            get
            {
                if (mclass.Substring(1, 1) == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }


        /// <summary>
        /// 是否为收款员
        /// </summary>
        public bool isSKY
        {
            get
            {
                if (mclass.Substring(2, 1) == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

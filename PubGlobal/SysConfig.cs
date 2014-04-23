using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Model;
namespace PubGlobal
{
    /// <summary>
    /// 操作系统参数
    /// </summary>
    public struct SysConfig
    {
        /// <summary>
        /// webservice网址
        /// </summary>
        public static string WebServiceUrl
        {
            get
            {
                return @"http://" + Server + ":" + Port + @"/TransService.asmx";
            }
        }

        /// <summary>
        /// 收款机号
        /// </summary>
        public static string PosNO;

        /// <summary>
        /// 服务器
        /// </summary>
        public static string Server;

        /// <summary>
        /// 端口号
        /// </summary>
        public static string Port;

        /// <summary>
        /// 是否根据部门获取商品列表
        /// true-部门  false-品类
        /// </summary>
        public static bool GetGoodsByDept;

        public static string PrnHeader1;

        public static string PrnHeader2;

        public static string PrnHeader3;

        public static string PrnFooter1;

        public static string PrnFooter2;

        public static string PrnFooter3;

        /// <summary>
        /// 储值卡密码
        /// </summary>
        public static string CzkPassword;

        /// <summary>
        /// 储值卡长度
        /// </summary>
        public static int CzkLen;
        /// <summary>
        /// 用户
        /// </summary>
        public static MUser User;

        /// <summary>
        /// 部门
        /// </summary>
        public static string DeptCode;

        public static string Stype;

        /// <summary>
        /// 打印联数
        /// </summary>
        public static int PrintCount;
    }
}
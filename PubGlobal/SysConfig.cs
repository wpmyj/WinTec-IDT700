using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Model;
namespace PubGlobal
{
    /// <summary>
    /// ����ϵͳ����
    /// </summary>
    public struct SysConfig
    {
        /// <summary>
        /// webservice��ַ
        /// </summary>
        public static string WebServiceUrl
        {
            get
            {
                return @"http://" + Server + ":" + Port + @"/TransService.asmx";
            }
        }

        /// <summary>
        /// �տ����
        /// </summary>
        public static string PosNO;

        /// <summary>
        /// ������
        /// </summary>
        public static string Server;

        /// <summary>
        /// �˿ں�
        /// </summary>
        public static string Port;

        /// <summary>
        /// �Ƿ���ݲ��Ż�ȡ��Ʒ�б�
        /// true-����  false-Ʒ��
        /// </summary>
        public static bool GetGoodsByDept;

        public static string PrnHeader1;

        public static string PrnHeader2;

        public static string PrnHeader3;

        public static string PrnFooter1;

        public static string PrnFooter2;

        public static string PrnFooter3;

        /// <summary>
        /// ��ֵ������
        /// </summary>
        public static string CzkPassword;

        /// <summary>
        /// ��ֵ������
        /// </summary>
        public static int CzkLen;
        /// <summary>
        /// �û�
        /// </summary>
        public static MUser User;

        /// <summary>
        /// ����
        /// </summary>
        public static string DeptCode;

        public static string Stype;

        /// <summary>
        /// ��ӡ����
        /// </summary>
        public static int PrintCount;
    }
}
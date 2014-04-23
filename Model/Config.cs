using System;

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Model
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class Config
    {
        private string companyName;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }



        private string customerName;
        /// <summary>
        /// 档口名称
        /// </summary>
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }



        private string posNo;
        /// <summary>
        /// 收款机号
        /// </summary>
        public string PosNo
        {
            get { return posNo; }
            set { posNo = value; }
        }




        private string serverAdd;
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAdd
        {
            get
            {
                if (serverAdd == string.Empty)
                {
                    return "http://192.168.0.1/SumPosWebService.asmx";
                }
                else { return serverAdd; }
            }
            set { serverAdd = value; }
        }


        /// <summary>
        /// 服务Url
        /// </summary>
        public string WebServerUrl
        {
            get { return serverAdd+"/SumPosWebService.asmx"; }
        }


        private CzCardType czkType;
        /// <summary>
        /// 卡类型
        /// </summary>
        public CzCardType CzkType
        {
            get { return czkType; }
            set { czkType = value; }
        }


        public string printBill;
        /// <summary>
        /// 打印小票
        /// </summary>
        public Model.PrintBillFlag PrintBill
        {
            get
            {
                if (printBill == "1")
                {
                    return PrintBillFlag.打印;
                }
                else
                {
                    return PrintBillFlag.不打印;
                }
            }
            set
            {
                if (value == PrintBillFlag.打印)
                {
                    printBill = "1";
                }
                else
                {
                    printBill = "0";
                }
            }
        }


        private string uid="sa";
        /// <summary>
        /// 数据库登陆名
        /// 默认sa
        /// </summary>
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }



        private string pwd="";
        /// <summary>
        /// 数据库登陆密码
        /// 默认空
        /// </summary>
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }


        /// <summary>
        /// sqlite数据库连接字符串
        /// </summary>
        public string SqliteConnStr
        {
            get
            {
                return "data source=" + CurrentPath + @"\data.xml";
            }
        }






        /// <summary>
        /// 程序完整路径
        /// </summary>
        public  string AppPath
        {
            get
            {
                return CurrentPath + @"\SumPos.exe";
            }
        }


        /// <summary>
        /// 程序所在路径
        /// </summary>
        public  string CurrentPath
        {
            get
            {
                string m_CurrentPath = "";

                if (Platform.Equals("WinCE"))
                {
                    m_CurrentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                }
                else if (Platform.Equals("Win32NT"))
                {
                    m_CurrentPath = Directory.GetCurrentDirectory();
                }

                return m_CurrentPath;
            }
        }



        /// <summary>
        /// 引导程序完整路径
        /// </summary>
        public  string UpgradeAppPath
        {
            get
            {
                return CurrentPath + @"\Upgrade.exe";
            }
        }



        /// <summary>
        /// 获取系统平台名称
        /// </summary>
        private  string Platform
        {
            get
            {
                return Environment.OSVersion.Platform.ToString();
            }
        }

        public string readCardMode;
        /// <summary>
        /// 读卡方式
        /// </summary>
        public Model.ReadCardByHand ReadCardMode
        {
            get {
                if(readCardMode=="1")
                {
                    return ReadCardByHand.允许手动;
                }
                else {
                    return ReadCardByHand.禁止手动;
                };
            }
            set {
                if (value == ReadCardByHand.允许手动)
                {
                    readCardMode = "1";
                }
                else
                {
                    readCardMode = "0";
                }
            }
        }


        private string netMac;
        /// <summary>
        /// 本机mac地址
        /// </summary>
        public string NetMac
        {
            get { return netMac; }
            set { netMac = value; }
        }


        private string str1;
        /// <summary>
        /// 票尾1
        /// </summary>
        public string Str1
        {
            get { return str1; }
            set { str1 = value; }
        }

        private string str2;
        /// <summary>
        /// 票尾2
        /// </summary>
        public string Str2
        {
            get { return str2; }
            set { str2 = value; }
        }

        private string str3;
        /// <summary>
        /// 票尾3
        /// </summary>
        public string Str3
        {
            get { return str3; }
            set { str3 = value; }
        }
    }
}

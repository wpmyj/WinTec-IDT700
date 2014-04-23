using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Upgrade
{
    public class Config
    {
        public Config()
        {
        }

        private static Upgrade.Model.ConfigModel configInfo = null;
        /// <summary>
        /// 配置信息
        /// </summary>
        public static Upgrade.Model.ConfigModel ConfigInfo
        {
            get
            {
                if (configInfo == null)
                {
                    configInfo = DAL.ConfigDAL.GetConfigInfo();
                }
                return configInfo;
            }
            set
            {
                configInfo = value;
            }
        }

        private static string Platform
        {
            get
            {
                return Environment.OSVersion.Platform.ToString();
            }
        }

        /// <summary>
        /// 程序所在路径
        /// </summary>
        public static string CurrentPath
        {
            get
            {
                string m_CurrentPath="";

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
        /// sqlite数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return "data source=" + CurrentPath + @"\data.xml";
            }
        }

        public static string ProcessAppPath
        {
            get
            {
                return CurrentPath + @"\SumPos.exe";
            }
        }

        public static string DataBaseAlterFilePath
        {
            get
            {
                return CurrentPath + @"\DataBaseAlter.sql";
            }
        }
    }
}

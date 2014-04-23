using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PubGlobal
{
    public class Envionment
    {
        public const string MAIN_EXE_FILE_NAME = @"\POS.exe";
        public const string UPDATE_EXE_FILE_NAME = @"\Update.exe";
        public const string CONFIG_FILE_NAME = @"\Config.ini";
        public const string VERSION_FILE_NAME = @"\Version.inf";
        /// <summary>
        /// 平台名称
        /// </summary>
        public static string Platform
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
        /// 程序完整路径
        /// </summary>
        public static string AppPath
        {
            get
            {
                return CurrentPath + MAIN_EXE_FILE_NAME;
            }
        }

        /// <summary>
        /// 引导程序完整路径
        /// </summary>
        public static string UpdateAppPath
        {
            get
            {
                return CurrentPath + UPDATE_EXE_FILE_NAME;
            }
        }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigFilePath
        {
            get
            {
                return CurrentPath + CONFIG_FILE_NAME;
            }
        }

        /// <summary>
        /// 版本文件路径
        /// </summary>
        public static string VersionFilePath
        {
            get
            {
                return CurrentPath + VERSION_FILE_NAME;
            }
        }

        /// <summary>
        /// 更新目录
        /// </summary>
        public static string UpdateFolderPath
        {
            get
            {
                return CurrentPath + @"\Updates";
            }
        }

        /// <summary>
        /// 程序版本
        /// </summary>
        public static string APPVersion
        {
            get;
            set;
        }
    }
}

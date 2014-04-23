using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
namespace ComClass
{
    public class ReadReg
    {
        public  enum HKEY { HKEY_LOCAL_MACHINE = 0, HKEY_CLASSES_ROOT = 1, HKEY_CURRENT_USER = 2, HKEY_USERS = 3 };
        private static RegistryKey[] reg = new RegistryKey[4]{Registry.LocalMachine,Registry.ClassesRoot,Registry.CurrentUser,Registry.Users};

        private static string ReadValue(HKEY Root, string SubKey, string ValueName)
        {
            RegistryKey subKey = reg[(int)Root];
            if (ValueName.Length == 0) return "[ERROR]";
            try
            {
                if (SubKey.Length > 0)
                {
                    string[] strSubKey = SubKey.Split('\\');
                    foreach (string strKeyName in strSubKey)
                    {
                        subKey = subKey.OpenSubKey(strKeyName);
                    }
                }
                string[] s = subKey.GetValueNames();
                byte[] strKeyb = (byte[])subKey.GetValue(ValueName);
                string strKey = "";
                foreach (byte b in strKeyb)
                {
                    string k = Convert.ToInt16(b).ToString("X");
                    if (k.Length < 2)
                    {
                        k = "0" + k;
                    }
                    strKey = strKey + k;
                }
                subKey.Close();
                return strKey;
            }
            catch
            {
                return "[ERROR]";
            }
        }


        /// <summary>
        /// 读取mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddr()
        {

            string mac = ReadValue(HKEY.HKEY_LOCAL_MACHINE, @"Comm\DM9CE1\Parms", "MACAddress");

            return mac;
        }
    }
}

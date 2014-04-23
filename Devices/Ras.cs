using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Devices
{
    public class RAS
    {
        #region api函数
        [DllImport("coredll.dll", CharSet = CharSet.Unicode)]
        private static extern int RasDial(int dialExtensions,
        int phoneBookPath,
        IntPtr rasDialParam,
        int NotifierType,
        int notifier,
        ref int hRasConn);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode)]
        private static extern int RasHangUp(int hRasConn);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode)]
        private static extern int RasGetConnectStatus(int hRasConn,
        ref RASCONNSTATUS lpRasConnStatus);
        #endregion

        //拨号参数
        private struct rasDialParams
        {
            public int dwSize;
        }

        /// <summary>
        /// 连接状态枚举
        /// </summary>
        public enum RASCONNSTATE
        {
            RASCS_OpenPort = 0,
            RASCS_PortOpened,
            RASCS_ConnectDevice,
            RASCS_DeviceConnected,
            RASCS_AllDevicesConnected,
            RASCS_Authenticate,
            RASCS_AuthNotify,
            RASCS_AuthRetry,
            RASCS_AuthCallback,
            RASCS_AuthChangePassword,
            RASCS_AuthProject,
            RASCS_AuthLinkSpeed,
            RASCS_AuthAck,
            RASCS_ReAuthenticate,
            RASCS_Authenticated,
            RASCS_PrepareForCallback,
            RASCS_WaitForModemReset,
            RASCS_WaitForCallback,
            RASCS_Projected,
            //#if   (WINVER   >=   0x400)   
            RASCS_StartAuthentication,   //   Windows   95   only   
            RASCS_CallbackComplete,   //   Windows   95   only   
            RASCS_LogonNetwork,   //   Windows   95   only   
            //#endif   
            RASCS_SubEntryConnected,
            RASCS_SubEntryDisconnected,
            RASCS_Interactive = 0x1000,   //   RASCS_PAUSED,   
            RASCS_RetryAuthentication,
            RASCS_CallbackSetByCaller,
            RASCS_PasswordExpired,
            //#if   (WINVER   >=   0x500)   
            RASCS_InvokeEapUI,
            //#endif   
            RASCS_Connected = 0x2000,   //RASCS_DONE,   
            RASCS_Disconnected
        }

        static int hRasConnection = 0;
        const int RAS_MaxDeviceType = 16;
        const int RAS_MaxDeviceName = 128;
        /// <summary>
        /// 连接状态结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct RASCONNSTATUS
        {
            public int dwSize;
            public RASCONNSTATE rasconnstate;
            public int dwError;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string szDeviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string szDeviceName;
        }

        public static bool Dial()
        {
            return Dial("我的连接", "16900", "16900");
            //return Dial("我的连接", "", "");
        }

        /// <summary>
        /// 连接gprs
        /// </summary>
        /// <param name="szRasConnection">gprs连接名称</param>
        /// <param name="szUsername">gprs连接用户名</param>
        /// <param name="szPassword">gprs连接密码</param>
        /// <param name="hRasConnection">连接成功后返回的gprs连接句柄</param>
        /// <returns>true:成功；false:失败</returns>
        public static bool Dial(string szRasConnection, string szUsername, string szPassword)
        {
            //先判断gprs是否已连接，如果已连接直接返回
            if (GetConnectStatus() == RASCONNSTATE.RASCS_Connected) return true;

            rasDialParams p = new rasDialParams();
            int dwSize;
            IntPtr strPointer;
            char[] bRasConnection = new char[szRasConnection.Length];
            //char[szRasConnection.Length] bRasConnection;
            IntPtr offset;
            int hrasconn = 0;

            char[] bUsername = new char[szUsername.Length];
            char[] bPassword = new char[szPassword.Length];
            int ret;

            dwSize = Marshal.SizeOf(p);
            dwSize += Marshal.SystemDefaultCharSize * 21;
            dwSize += Marshal.SystemDefaultCharSize * 129;
            dwSize += Marshal.SystemDefaultCharSize * 49;
            dwSize += Marshal.SystemDefaultCharSize * 257;
            dwSize += Marshal.SystemDefaultCharSize * 257;
            dwSize += Marshal.SystemDefaultCharSize * 16;

            bRasConnection = szRasConnection.ToCharArray();
            bUsername = szUsername.ToCharArray();
            bPassword = szPassword.ToCharArray();
            strPointer = GPRSConnect.AllocHLocal(dwSize);
            offset = new IntPtr(strPointer.ToInt32() + Marshal.SizeOf(p));
            Marshal.Copy(bRasConnection, 0, offset, szRasConnection.Length);
            offset = new IntPtr(offset.ToInt32() +
            Marshal.SystemDefaultCharSize * 21);
            Marshal.Copy("".ToCharArray(), 0, offset, "".Length);
            offset = new IntPtr(offset.ToInt32() +
            Marshal.SystemDefaultCharSize * 129);
            Marshal.Copy("".ToCharArray(), 0, offset, "".Length);
            offset = new IntPtr(offset.ToInt32() +
            Marshal.SystemDefaultCharSize * 49);
            Marshal.Copy(bUsername, 0, offset, szUsername.Length);
            offset = new IntPtr(offset.ToInt32() +
            Marshal.SystemDefaultCharSize * 257);
            Marshal.Copy(bPassword, 0, offset, szPassword.Length);
            offset = new IntPtr(offset.ToInt32() +
            Marshal.SystemDefaultCharSize * 257);
            Marshal.Copy("".ToCharArray(), 0, offset, "".Length);

            try
            {
                ret = RasDial(0, 0, strPointer, 0, 0, ref hrasconn);

                if (ret == 0)
                {
                    hRasConnection = hrasconn;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取gprs连接状态
        /// </summary>
        /// <returns></returns>
        public static RASCONNSTATE GetConnectStatus()
        {
            RASCONNSTATUS rasConnStatus = new RAS.RASCONNSTATUS();
            RasGetConnectStatus(hRasConnection, ref rasConnStatus);
            return rasConnStatus.rasconnstate;
        }

        /// <summary>
        /// 断开gprs连接
        /// </summary>
        /// <param name="hrasconn"></param>
        /// <returns></returns>
        public static bool HangUp()
        {
            int ret;
            try
            {
                ret = RasHangUp(hRasConnection);
                if (ret == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
        }
    }

    class GPRSConnect
    {

        [DllImport("coredll.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LocalAlloc(int uFlags,
        int uBytes);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("coredll.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LocalReAlloc(int hMem,
        int uBytes, int fuFlags);

        private const int LMEM_FIXED = 0;
        private const int LMEM_MOVEABLE = 2;
        private const int LMEM_ZEROINIT = 64;
        private const int LPTR = LMEM_ZEROINIT; //(LMEM_FIXED ||LMEM_ZEROINIT);


        // Allocates a block of memory using LocalAlloc
        public static IntPtr AllocHLocal(int cb)
        {
            return LocalAlloc(LPTR, cb);
        }

        // Frees memory allocated by AllocHLocal
        public static void FreeHLocal(IntPtr hlocal)
        {
            if (!hlocal.Equals(IntPtr.Zero))
            {
                if (!IntPtr.Zero.Equals(LocalFree(hlocal)))
                {
                    throw new Exception("win32 error");
                }
                hlocal = IntPtr.Zero;
            }
        }

        // Resizes a block of memory previously allocated with AllocHLocal
        public static IntPtr ReAllocHLocal(int pv, int cb)
        {
            IntPtr newMem = LocalReAlloc(pv, cb, LMEM_MOVEABLE);
            if (newMem.Equals(IntPtr.Zero))
            {
                throw new Exception("out of memory");
            }

            return newMem;
        }

        // Copies the contents of a managed string to unmanaged memory
        public static IntPtr StringToHLocalUni(string s)
        {
            if (s == null)
            {
                return IntPtr.Zero;
            }
            else
            {
                int nc = s.Length;
                int len = 2 * (1 + nc);

                IntPtr hLocal = AllocHLocal(len);

                if (hLocal.Equals(IntPtr.Zero))
                {
                    throw new Exception("out of memory");
                }
                else
                {
                    System.Runtime.InteropServices.Marshal.Copy(s.ToCharArray(), 0,
                    hLocal, s.Length);
                    return hLocal;
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace COMM
{
    public class Win32
    {
        public const uint POWER_FORCE = 0x00001000u;
        public const uint POWER_STATE_RESET = 0x00800000u;        // reset state

        [DllImport("coredll.dll")]
        public static extern uint SetSystemPowerState([MarshalAs(UnmanagedType.LPWStr)]string psState, uint StateFlags, uint Options);

        [DllImport("coredll.dll", EntryPoint = "FindWindow")]

        public static extern int FindWindow(string lpWindowName, string lpClassName);
        [DllImport("coredll.dll", EntryPoint = "ShowWindow")]

        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("coredll.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int uAction, int uParam, ref Rectangle lpvParam, int fuWinIni);

        public const int SPI_SETWORKAREA = 47;
        public const int SPI_GETWORKAREA = 48;

        public const int SW_HIDE = 0x00;
        public const int SW_SHOW = 0x0001;
        public const int SPIF_UPDATEINIFILE = 0x01;

    }
}
